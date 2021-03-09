using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private List<GameObject> enemys = new List<GameObject>();
    private bool inList = false;
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
}
