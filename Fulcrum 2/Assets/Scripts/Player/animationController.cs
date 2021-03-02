using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewAnimController : MonoBehaviour
{


    public Slider sliderScrubber;
    public Animator animator;
    bool isWalkingForward = false;

    void Start()
    {

        gameObject.GetComponent<Animator>().Play("maleIdle.anim");
        
    }

    public void Update()
    {
        if (Input.GetKey("w"))
        {
            isWalkingForward = true;
        }
        else
        {
            isWalkingForward = false;
        }
    }

    
}