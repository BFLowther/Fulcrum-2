﻿using UnityEngine;
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
    public int magazineSize = 6;
    private int magazineCounter;
    public float shootDelay = 1f;
    private float shootDelayCounter = 0f;

    Gun myGun;
    public GameObject bulletPrefab;
    GameObject bullet;
    public float bulletSpeed = 10f;

    public float health = 10f;
    public float dmgTakenPerHit = 2f;
    //public ParticleSystem DeathParticle;
    [HideInInspector]
    public bool dead = false;

    private Animator anim;
    private bool playIdle;

    private SpriteRenderer _spriteRenderer;

    public static PlayerController SharedInstance;

    void Awake()
    {
        SharedInstance = this;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        idleCount = 5.0f;
        anim.SetFloat("idleTimer", idleCount);
        reloadTime = reloadSpeed;
        magazineCounter = magazineSize;
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

        Vector3 targetPos = transform.position + moveVector;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);

        float move = Input.GetAxis("Vertical");//sets move to Verticals value
        anim.SetFloat("ForwardSpeed", move); //sets ForwardSpeed to move's value

        float sideMove = Input.GetAxis("Horizontal");
        anim.SetFloat("SideSpeed", sideMove);
    }

    void TakeAShot()
    {
        if (shoot && !reloading)
        {
            
            if (magazineCounter > 0)
            {
                if (shootDelayCounter <= 0f)
                {
                    
                    if (GetDirection() != new Vector3(0, 0, 0))
                    {
                        bullet = Instantiate(bulletPrefab);
                        bullet.transform.position = gameObject.transform.position;
                        bullet.transform.rotation = Quaternion.LookRotation(GetDirection(), Vector3.up);
                        //bullet.GetComponent<ParticleSystem>().Play();
                        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
                        magazineCounter--;
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
                magazineCounter = magazineSize;
                reloadTime = reloadSpeed;
                reloading = false;
            }
        }
    }

    private Vector3 GetDirection()
    {
        return myGun.ClosestEnemy(gameObject.transform.position) - gameObject.transform.position;
    }

    public void TakeAHit()
    {
        health -= dmgTakenPerHit;
        if (health <= 0)
        {
            //Instantiate(DeathParticle);
            dead = true;
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    public float GetMagazineCount()
    {
        return magazineCounter;
    }

    public bool IsReloading()
    {
        return reloading;
    }

    public float GetReloadTime()
    {
        return reloadTime;
    }
}
