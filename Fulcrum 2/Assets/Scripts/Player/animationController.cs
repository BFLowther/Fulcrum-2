using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class animationController : MonoBehaviour
{
   
    public Animator animator;

    void Start()
    {

        animator=this.GetComponent<Animator>();
        
    }

    public void Update()
    {
        float move = Input.GetAxis("Vertical");
        animator.SetFloat("ForwardSpeed", move);

        float sideMove = Input.GetAxis("Horizontal");
        animator.SetFloat("SideSpeed", sideMove);
    }

    
}