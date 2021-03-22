using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbleweed : MonoBehaviour
{
    public float speed = 0;
    public float distanceBeforeDisapear = 0;
    Vector3 direction;
    Vector3 disapearPoint;
    private void OnEnable()
    {
        direction = transform.forward;

        disapearPoint = transform.forward * distanceBeforeDisapear;

    }
    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * speed);
    }
}
