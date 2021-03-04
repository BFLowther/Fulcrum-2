using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AiState
{
    idle,
    wandering,
    potroling,
    hunting
}

public class AI : MonoBehaviour
{
    [Header("Player Prefab")]
    public Transform playersTransform;

    [Header("AI State Settings")]
    public AiState defaultAiState = AiState.idle;

    [Header("Potrol/Waiting Settings")]
    public List<Vector3> movementPoints;
    public float potrolWaitTime = 5f;
    public float potrolSeeHearRangePenalty = .66f;
    public float wanderingSeeHearRangePenalty = .75f;

    [Header("AI Ranges")]
    public float seeHearRange = 10f;
    public float shootFromRange = 5f;

    private bool canSeeHear = false;
    private bool inRange = false;
    private AiState aiState = AiState.idle;
    private AiState lastAiState = AiState.idle;
    private bool still = false;
    private int currentMovementPoint = 0;
    private float potrolWaitTimeCounter;
    private float seeHearRangeBackup;

    private NavMeshAgent meshAgent;

    Vector3 lastKnownPlayerLocation;

    private void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        potrolWaitTimeCounter = potrolWaitTime;
        seeHearRangeBackup = seeHearRange;
    }

    void Update()
    {
        CheckSenses();

        if (canSeeHear)
            SeeHear();

        if (aiState == AiState.idle)
            Idle();

        if (aiState == AiState.wandering)
            Wandering();

        if (aiState == AiState.potroling)
            Potroling();

        if (aiState == AiState.hunting)
            Hunting();
    }

    void CheckSenses()
    {
        if (Vector3.Distance(transform.position, playersTransform.position) < seeHearRange)
        {
            canSeeHear = true;
        }
        else
            canSeeHear = false;
    }

    void SeeHear()
    {
        lastKnownPlayerLocation = playersTransform.position;
        aiState = AiState.hunting;
        canSeeHear = false;
    }

    void Idle()
    {
        aiState = defaultAiState;
    }

    void Wandering()
    {
        if (seeHearRange == seeHearRangeBackup)
            seeHearRange = seeHearRange * .75f;

        meshAgent.autoBraking = false;
        if (!meshAgent.pathPending && meshAgent.remainingDistance < .5f)
                GoToNextPoint();

        lastAiState = AiState.wandering;
    }

    void Potroling()
    {
        if (seeHearRange == seeHearRangeBackup && !(still))
            seeHearRange = seeHearRange * .66f;

        meshAgent.autoBraking = true;
        if(!meshAgent.pathPending && meshAgent.remainingDistance < .5f)
        {
            seeHearRange = seeHearRangeBackup;
            still = true;

            potrolWaitTimeCounter -= Time.deltaTime;
            if (potrolWaitTimeCounter <= 0f)
            {
                GoToNextPoint();
                still = false;
            }
        }

        lastAiState = AiState.potroling;
    }

    void Hunting()
    {
        seeHearRange = seeHearRangeBackup;
        if (Vector3.Distance(transform.position, playersTransform.position) < shootFromRange)
            inRange = true;
        else
            inRange = false;

        if (inRange)
            meshAgent.destination = transform.position;
        else
        {
            meshAgent.destination = lastKnownPlayerLocation;
            if (meshAgent.remainingDistance < .5f)
                aiState = AiState.idle;
        }

        lastAiState = AiState.hunting;
    }

    void GoToNextPoint()
    {
        if (movementPoints.Count == 0)
            return;

        meshAgent.destination = movementPoints[currentMovementPoint];

        currentMovementPoint = (currentMovementPoint + 1) % movementPoints.Count;

        potrolWaitTimeCounter = potrolWaitTime;
    }

    private void OnDrawGizmos()
    {
        AiGizmos();

        MovementPointGizmos();
    }

    void AiGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, seeHearRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootFromRange);
        Gizmos.color = Color.white;
        if(meshAgent != null)
            Gizmos.DrawLine(transform.position, meshAgent.destination);
    }

    void MovementPointGizmos()
    {
        Gizmos.color = Color.magenta;
        if (movementPoints.Count > 0)
        {
            for (int i = movementPoints.Count; i > 0; i--)
            {
                Gizmos.DrawSphere(movementPoints[i - 1], .5f);
                
                if (movementPoints.Count > 2 && i == movementPoints.Count)
                {
                    Gizmos.DrawLine(movementPoints[i - 1], movementPoints[0]);
                    Gizmos.DrawLine(movementPoints[i - 1], movementPoints[i - 2]);
                }
                else if (i != 1)
                {
                    Gizmos.DrawLine(movementPoints[i - 1], movementPoints[i - 2]);
                }
                
            }
        }
        
    }
}
