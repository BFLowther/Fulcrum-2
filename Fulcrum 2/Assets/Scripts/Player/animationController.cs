using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewAnimController : MonoBehaviour
{


   
    public Animator animator;

    void Start()
    {

        animator=this.GetComponent<Animator>();
        
    }

    public void Update()
    {
        float move = Input.GetAxis("Vertical");
        animator.SetFloat("Speed", move);
    }

    
}