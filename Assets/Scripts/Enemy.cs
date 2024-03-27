using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;

    public bool shouldRunAway;
    public float runAwayRange;
    public bool shouldWander;
    public float wanderLength, pauseLength;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;
    public bool shouldChase;
    public float rangeToChasePlayer;
    public Animator anim;
    private Vector3 MoveDirection;
    public int health = 150;
    public GameObject damageEffect;
    public GameObject[] deathsplatters;
    public bool shouldShoot;
    public GameObject Bullet;
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;
    public Transform firePoint_;
    public float fireRate;
    private float fireCounter; // Make it private to avoid external modification
    public SpriteRenderer Body;
    public float shootRange;
    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;

    // Start is called before the first frame update
    void Start()
    {
        fireCounter = fireRate; // Initialize fireCounter to fireRate to start shooting immediately
        if (shouldWander)
        {
            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Body.isVisible && PlayerController.instance != null && PlayerController.instance.gameObject.activeInHierarchy)
        {
            MoveDirection = Vector3.zero;
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && shouldChase)
            {
                MoveDirection = (PlayerController.instance.transform.position - transform.position).normalized;
            }
            else
            {
                if (shouldWander)
                {
                    if (wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;

                        //move enemy

                        MoveDirection = wanderDirection;

                        if (wanderCounter <= 0)
                        {
                            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
                        }
                    }
                    if (pauseCounter > 0)
                    {
                        pauseCounter -= Time.deltaTime;

                        if (pauseCounter <= 0)
                        {
                            wanderCounter = Random.Range(wanderLength * .75f, wanderLength * 1.75f);
                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);

                        };
                    }
                
                
                }
                if (shouldPatrol)
                {
                    MoveDirection = (patrolPoints[currentPatrolPoint].position - transform.position).normalized;
                   

                    if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) <= 0.2f)
                    {
                        currentPatrolPoint++;
                     

                        if (currentPatrolPoint >= patrolPoints.Length)
                        {
                            currentPatrolPoint = 0;
                        }
                    }
                }



            }
            if (shouldRunAway && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                MoveDirection = transform.position - PlayerController.instance.transform.position ;
            }


            MoveDirection.Normalize();

            if (shouldPatrol || shouldChase || shouldRunAway)
            {
                theRB.velocity = MoveDirection * moveSpeed;
            }
            else
            {
                theRB.velocity = Vector2.zero;
            }

            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
            {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(Bullet, firePoint_.position, firePoint_.rotation);
                    SoundEffects.instance.Shootpistol();
                }
            }
            else
            {
                // If not shooting, you might want to set velocity to zero here as well
                theRB.velocity = Vector2.zero;
            }

            

            if (moveSpeed != 0.0f)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }

        if (health <= 0)
        {
          
            Destroy(gameObject);
            SoundEffects.instance.DeadEnemy();

            int selectedSplatter = Random.Range(0, deathsplatters.Length);
            int rotation = Random.Range(0, 4);

            Instantiate(deathsplatters[selectedSplatter], transform.position, Quaternion.Euler(0, 0, rotation * 90));
        }
    }


    public void DamageEnemy(int damage)
    {
    

        health -= damage;
        SoundEffects.instance.HurtEnemy();
        Instantiate(damageEffect, transform.position, transform.rotation);

        if (health <= 0)
        {
           
            Destroy(gameObject);
            SoundEffects.instance.DeadEnemy();

            int selectedSplatter = Random.Range(0,deathsplatters.Length);
            int rotation = Random.Range(0, 4);


            Instantiate(deathsplatters[selectedSplatter],transform.position,Quaternion.Euler(0,0,rotation*90));
            if (shouldDropItem)
            {
                float dropchance = Random.Range(0f, 100f);

                if (dropchance < itemDropPercent)
                {
                    int randomItem = Random.Range(0, itemsToDrop.Length);
                    Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                }
            }
            //Instantiate(deathsplatter, transform.position, transform.rotation);
        }
       
    }



}
