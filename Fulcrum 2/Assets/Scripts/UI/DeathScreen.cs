using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreenUI;

    private void Update()
    {
        if(PlayerController.SharedInstance.dead == true)
        {
            deathScreenUI.SetActive(true);
        }
        else
        {
            deathScreenUI.SetActive(false);
        }
    }
}
