using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool canSeeHear = false;
    AiState aiState = AiState.idle;
    

    void Update()
    {
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

    void SeeHear()
    {

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

    }
}
