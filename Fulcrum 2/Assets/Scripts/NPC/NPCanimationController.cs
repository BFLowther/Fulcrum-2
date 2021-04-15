using UnityEngine;
using UnityEngine.AI;

public class NPCanimationController : MonoBehaviour
{
    public Animator animator;
    NavMeshAgent agent;
    private Rigidbody myRigidBody;
    public Transform player;

    void start()
    {
        animator = this.GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody>();
    }

    void update()
    {
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;

            if(direction.magnitude > 1)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        
    }

    


}