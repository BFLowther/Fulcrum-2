using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class animationController : MonoBehaviour
{

    public Animator animator;
    //private bool isMale;
    public GameManagerNew GameManagerNew;

    void Start()
    {

        animator = this.GetComponent<Animator>();
       
        animator.SetBool("isMale", GameManagerNew.IsMale());
    }

    public void Update()
    {
        float move = Input.GetAxis("Vertical");//sets move to Verticals value
        animator.SetFloat("ForwardSpeed", move); //sets ForwardSpeed to move's value

        float sideMove = Input.GetAxis("Horizontal");
        animator.SetFloat("SideSpeed", sideMove);
    }

    
    
}