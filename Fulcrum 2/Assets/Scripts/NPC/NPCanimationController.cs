using UnityEngine;
using UnityEngine.AI;

public class NPCanimationController : MonoBehaviour
{
    public Animator animator;
    NavMeshAgent agent;
    private Rigidbody myRigidBody;

    void start()
    {
        animator = this.GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody>();
    }

    void update()
    {
        

       

        
    }

    


}