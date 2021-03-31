using UnityEngine;

public class TumbleWeedSpawner : MonoBehaviour
{
    public float timeBetweenSpawn;
    public float tumbleweedSpeed;
    public float distanceBeforeDisapear;
    public GameObject stoppingPoint;
    private float timer = 0;
    [HideInInspector]
    public bool playerNear = false;
    Tumbleweed tumbleweedScript;

    private void Start()
    {
        stoppingPoint.transform.position = transform.position + transform.forward * distanceBeforeDisapear;
    }

    void Update()
    {
        if (playerNear)
        {
            if (timer <= 0)
            {
                GameObject tumbleWeed = ObjectPooler.SharedInstance.GetPooledObject();
                if (tumbleWeed != null)
                {
                    tumbleweedScript = tumbleWeed.GetComponent<Tumbleweed>();
                    tumbleweedScript.speed = tumbleweedSpeed;
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 3f);
        Gizmos.DrawWireSphere(transform.position + transform.forward * distanceBeforeDisapear, 3f);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distanceBeforeDisapear);
    }
}
