using UnityEngine;

public class EnemyHitPoint : MonoBehaviour
{
    AI aiScrip;

    private void Start()
    {
        aiScrip = GetComponentInParent<AI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            aiScrip.TakeAHit();
            Destroy(other.gameObject);
        }
    }

    
}
