using UnityEngine;

public class Tumbleweed : MonoBehaviour
{
    public float speed = 0;
    public float distanceBeforeDisapear = 0;
    Vector3 direction;
    private void OnEnable()
    {
        direction = transform.forward;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TumbleweedStopper")
        {
            gameObject.SetActive(false);
        }
    }
}
