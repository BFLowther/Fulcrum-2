using UnityEngine;
using UnityEngine.AI;

public class NPCanimationController : MonoBehaviour
{
    public Animator animator;
    Vector3 lastPosition;
    Transform myTransform;
    bool isMoving;

    void start()
    {
        animator = this.GetComponent<Animator>();
        myTransform = transform;
        isMoving = false;
    }

    void update()
    {
        if (myTransform.position != lastPosition)
            isMoving = true;
        else
            isMoving = false;

        lastPosition = myTransform.position;

        animator.SetBool("isMoving", isMoving);
        
    }

    


}