using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform myTransform;
    private Transform myCameraTransform;
	private GameObject camera;

	void Start()
	{
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		myCameraTransform = camera.transform;
	}
	void LateUpdate()
    {
        myTransform.forward = myCameraTransform.forward;
    }
}
