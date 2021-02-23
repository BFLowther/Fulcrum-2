using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum AiState
{
    wandering,
    potroling,
    idle,
    hunting
}

public class AI : MonoBehaviour
{
    public Transform playersTransform;

    Vector3 lastKnownPlayerLocation;

    //Transform lastKnownPlayerLocation;

    public float seeHearRange = 10f;
    private bool canSeeHear = false;
    private AiState aiState = AiState.idle;

    public float shootFromRange = 5f;
    private bool inRange = false;

    private NavMeshAgent meshAgent;

    private void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
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

    }

    void Wandering()
    {
        
    }

    void Potroling()
    {

    }

    void Hunting()
    {
        if (Vector3.Distance(transform.position, playersTransform.position) < shootFromRange)
            inRange = true;
        else
            inRange = false;

        if (inRange)
            meshAgent.destination = transform.position;
        else
        {
            meshAgent.destination = lastKnownPlayerLocation;
            if (Vector3.Distance(transform.position, lastKnownPlayerLocation) < 1f)
                aiState = AiState.idle;
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, seeHearRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootFromRange);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, meshAgent.destination);
    }
}
