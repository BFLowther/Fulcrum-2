using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Gun SharedInstance;
    private List<GameObject> enemys = new List<GameObject>();
    Vector3 playerPosition;
    private bool inList = false;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        playerPosition = GetComponentInParent<Transform>().position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
        //    for (int i = enemys.Count; i > 0; i--)
        //    {
        //        if (other.gameObject.name == enemys[i - 1].name)
        //        {
        //            inList = true;
        //        }
        //    }
        //    if (!(inList))
        //    {
        //        enemys.Add(other.gameObject);
        //        inList = false;
        //    }
            enemys.Add(other.gameObject);
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            for (int i = enemys.Count; i > 0; i--)
            {
                if (other.gameObject.name == enemys[i - 1].name)
                {
                    enemys.RemoveAt(i - 1);
                }
            }
    }

    public void removeEnemy(GameObject other)
    {
        if (other.tag == "Enemy")
            for (int i = enemys.Count; i > 0; i--)
            {
                if (other.gameObject.name == enemys[i - 1].name)
                {
                    enemys.RemoveAt(i - 1);
                }
            }
    }

    public Vector3 ClosestEnemy(Vector3 playerPosition)
    {
        if (enemys.Count > 0)
        {
            Vector3 closestEnemy = enemys[enemys.Count - 1].transform.position;

            for (int i = enemys.Count; i > 0; i--)
            {
                if (Vector3.Distance(enemys[i - 1].transform.position, playerPosition) < Vector3.Distance(closestEnemy, playerPosition))
                    closestEnemy = enemys[i - 1].transform.position;
                
            }
            return closestEnemy;
        }
        return playerPosition;
    }

    public bool ClosestEnemy (string other)
    {
        if (enemys.Count > 0)
        {
            Vector3 closestEnemy = enemys[enemys.Count - 1].transform.position;
            GameObject closestEnemyGO = enemys[enemys.Count - 1];

            for (int i = enemys.Count; i > 0; i--)
            {
                if (Vector3.Distance(enemys[i - 1].transform.position, playerPosition) < Vector3.Distance(closestEnemy, playerPosition))
                {
                    closestEnemy = enemys[i - 1].transform.position;
                    closestEnemyGO = enemys[i - 1];
                    
                }

            }
            if (other == closestEnemyGO.name)
                return true;
        }
        return false;
    }
}
