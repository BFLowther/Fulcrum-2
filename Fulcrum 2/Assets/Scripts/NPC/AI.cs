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
    public float closeEnoughRange = 5f;

    [Header("AI Health Settings")]
    public float health = 10f;
    public float dmgTakenPerHit = 2f;

    [Header("AI Shooting Settings")]
    public GameObject bulletPrefab;
    public int magazineSize = 6;
    public float shootDelay = 1f;
    public float reloadSpeed = 5f;
    public float bulletSpeed = 10f;


    private bool canSeeHear = false;
    private bool inRange = false;
    private bool inRangeToShoot = false;
    private AiState aiState = AiState.idle;
    private AiState lastAiState = AiState.idle;
    private bool still = false;
    private int currentMovementPoint = 0;
    private float potrolWaitTimeCounter;
    private float seeHearRangeBackup;

    private bool reloading = false;
    private float shootDelayCounter = 0f;
    private float reloadTime;
    private int magazineCounter = 6;
    GameObject bullet;
    private Vector3 bulletPosition;


    private NavMeshAgent meshAgent;

    Vector3 lastKnownPlayerLocation;

    private void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        potrolWaitTimeCounter = potrolWaitTime;
        seeHearRangeBackup = seeHearRange;

        reloadTime = reloadSpeed;
        magazineCounter = magazineSize;
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
            inRangeToShoot = true;
        }
        else
        {
            canSeeHear = false;
            inRangeToShoot = false;
        }
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
        if (Vector3.Distance(transform.position, playersTransform.position) < closeEnoughRange)
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
        TakeAShot();
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

    public void TakeAHit()
    {
        health -= dmgTakenPerHit;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void TakeAShot()
    {
        if (inRangeToShoot && !reloading)
        {

            if (magazineCounter > 0)
            {
                if (shootDelayCounter <= 0)
                {

                    if (inRangeToShoot)
                    {
                        bulletPosition = gameObject.transform.position;
                        bulletPosition.y = lastKnownPlayerLocation.y;
                        bullet = Instantiate(bulletPrefab);
                        bullet.transform.position = bulletPosition;
                        bullet.transform.rotation = Quaternion.LookRotation(GetDirection(), Vector3.up);
                        bullet.GetComponent<ParticleSystem>().Play();
                        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
                        magazineCounter--;
                        shootDelayCounter = shootDelay;
                    }

                }
                else
                    shootDelayCounter -= Time.deltaTime;
            }
            else
                reloading = true;
        }
        if (reloading)
        {
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0f)
            {
                magazineCounter = magazineSize;
                reloadTime = reloadSpeed;
                reloading = false;
            }
        }
    }

    private Vector3 GetDirection()
    {
        return lastKnownPlayerLocation - bulletPosition;
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
        Gizmos.DrawWireSphere(transform.position, closeEnoughRange);
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
