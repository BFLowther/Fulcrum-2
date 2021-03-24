using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    TumbleWeedSpawner tumbleweedSpawner;

    private void Start()
    {
        tumbleweedSpawner = GetComponentInParent<TumbleWeedSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player")
        {
            tumbleweedSpawner.playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            tumbleweedSpawner.playerNear = false;
        }
    }
}
