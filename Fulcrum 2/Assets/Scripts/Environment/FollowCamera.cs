using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;

    private Vector3 originalOffset;

	// Use this for initialization
	void Start ()
	{
	    originalOffset = target.position - transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UpdateCamera();
	}

    void UpdateCamera()
    {
        Vector3 finalPos = target.position - originalOffset;

        transform.position = Vector3.Lerp(transform.position, finalPos, Time.fixedDeltaTime * followSpeed);
    }
}
