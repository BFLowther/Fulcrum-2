using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitPoint : MonoBehaviour
{
    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            playerController.TakeAHit();
            Destroy(other.gameObject);
        }
    }
}
