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
    private bool shoot;
    private bool reloading = false;
    public float reloadSpeed = 5f;
    private float reloadTime;
    public int magizineSize = 6;
    private int magizineCounter;
    public float shootDelay = 1f;
    private float shootDelayCounter = 0f;

    Gun myGun;
    public GameObject bulletPrefab;
    GameObject bullet;


    private Animator anim;
    private bool playIdle;

    private SpriteRenderer _spriteRenderer;


    void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        idleCount = 5.0f;
        anim.SetFloat("idleTimer", idleCount);
        reloadTime = reloadSpeed;
        magizineCounter = magizineSize;
        myGun = gameObject.GetComponentInChildren<Gun>();
    }

    void Update()
    {
        UpdateInputs();
        TakeAShot();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void UpdateInputs()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        shoot = Input.GetButton("Fire1");
        //run = Input.GetButton("Run"); (not used in this build)

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

    void TakeAShot()
    {
        if (shoot && !reloading)
        {
            
            if (magizineCounter > 0)
            {
                if (shootDelayCounter <= 0)
                {
                    
                    if (GetDirection() != new Vector3(0, 0, 0))
                    {
                        Debug.Log("shot");
                        bullet = Instantiate(bulletPrefab);
                        bullet.transform.position = gameObject.transform.position;
                        bullet.transform.rotation = Quaternion.LookRotation(GetDirection(), Vector3.up);
                        bullet.GetComponent<ParticleSystem>().Play();
                        magizineCounter--;
                        shootDelayCounter = shootDelay;
                    }
                    
                }
                else
                    shootDelayCounter -= Time.deltaTime;
            }
            else
                reloading = true;
        }
        if (reloading)
        {
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0f)
            {
                reloading = false;
                magizineCounter = magizineSize;
                reloadTime = reloadSpeed;
            }
        }
    }

    private Vector3 GetDirection()
    {
        return myGun.ClosestEnemy(gameObject.transform.position) - gameObject.transform.position;
    }
}