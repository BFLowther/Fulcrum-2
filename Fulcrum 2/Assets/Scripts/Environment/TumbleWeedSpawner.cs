using UnityEngine;

public class TumbleWeedSpawner : MonoBehaviour
{
    public float timeBetweenSpawn;
    public float tumbleweedSpeed;
    public float distanceBeforeDisapear;
    private float timer = 0;
    Tumbleweed tumbleweedScript;

    void Update()
    {
        if (timer <= 0)
        {
            GameObject tumbleWeed = ObjectPooler.SharedInstance.GetPooledObject();
            if (tumbleWeed != null)
            {
                tumbleweedScript = tumbleWeed.GetComponent<Tumbleweed>();
                tumbleweedScript.speed = tumbleweedSpeed;
                tumbleweedScript.distanceBeforeDisapear = distanceBeforeDisapear;
                tumbleWeed.transform.position = gameObject.transform.position;
                tumbleWeed.transform.rotation = gameObject.transform.rotation;
                tumbleWeed.SetActive(true);
            }

            timer = timeBetweenSpawn;
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, .5f);
        Gizmos.DrawWireSphere(transform.position + transform.forward * distanceBeforeDisapear, .5f);

    }
}
