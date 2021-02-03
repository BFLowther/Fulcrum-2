using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float runMultiplier = 1.5f;

    public float idleCount;
    //public float timeForIdle = 5.0f;

    // Input Data
    private float vertical;
    private float horizontal;
    private bool run;

    private Animator anim;
    private bool playIdle;

    private SpriteRenderer _spriteRenderer;


    void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        idleCount = 5.0f;
        anim.SetFloat("idleTimer", idleCount);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void UpdateInputs()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        run = Input.GetButton("Run");
    }

    void MovePlayer()
    {
        Vector3 moveVector = new Vector3(horizontal, 0, vertical);
        moveVector = moveVector.normalized * speed * (run ? runMultiplier : 1);

        anim.SetFloat("vertical", moveVector.z);
        anim.SetFloat("horizontal", moveVector.x);

        Vector3 targetPos = transform.position + moveVector;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);

        if (horizontal > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
}