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

    Transform lastKnownPlayerLocation;

    public float seeHearRange = 10f;
    bool canSeeHear = false;
    AiState aiState = AiState.idle;

    public float shootFromRange = 5f;
    bool inRange = false;

    NavMeshAgent meshAgent;

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
        if(Vector3.Distance(gameObject.transform.position, playersTransform.position) < seeHearRange)
        {
            canSeeHear = true;
        }
    }

    void SeeHear()
    {
        lastKnownPlayerLocation = playersTransform;
        aiState = AiState.hunting;
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
        if (Vector3.Distance(gameObject.transform.position, playersTransform.position) < shootFromRange)
            inRange = true;
        else
            inRange = false;

        if (inRange)
            meshAgent.destination = gameObject.transform.position;
        else
        {
            meshAgent.destination = lastKnownPlayerLocation.position;
            if (Vector3.Distance(gameObject.transform.position, lastKnownPlayerLocation.position) < 1)
                aiState = AiState.idle;
        }

        
    }
}
