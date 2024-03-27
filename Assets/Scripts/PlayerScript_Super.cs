using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform gunArm;

    //private Camera theCam;

    public Animator anim;

    /*public GameObject bulletToFire;

    public Transform firePoint;

    public float timeBetweenShots;

    private float shotCounter;*/

    public SpriteRenderer bodysr;
    public float rotationSpeed = 5f;

    private float ActiveMoveSpeed;
    public float dashSpeed = 8f, dashLenght = 0.5f,dashcooldown = 1f, dashInvincibility = 0.5f;
    [HideInInspector]
    public float dashCounter;
    private float  dashCoolCounter;
    private float minTimeBetweenShots = 0.1f;
    [HideInInspector]
    public bool canMove = true;
    public List<Gun> availableGun = new List<Gun>();
    [HideInInspector]
    public int currentGun;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
       // theCam = Camera.main;

        ActiveMoveSpeed = moveSpeed;

        UI_Controller.Instance.currentGun.sprite = availableGun[currentGun].gunUI;
        UI_Controller.Instance.gunText.text = availableGun[currentGun].weaponName;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !LevelManager.instance.isPaused)
        {


            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();

            theRB.velocity = moveInput * ActiveMoveSpeed;

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = CameraController.instance.mainCamera.WorldToScreenPoint(transform.localPosition);

            // Smoothly rotate the character
            float targetScaleX = (mousePos.x < screenPoint.x) ? -1f : 1f;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScaleX, 1f, 1f), Time.deltaTime * rotationSpeed);

            // Smoothly rotate and scale the gun arm
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

            // Ensure the gun arm is correctly oriented based on the character's scale
            float gunArmScaleX = Mathf.Abs(transform.localScale.x); // Use absolute value for scaling
            gunArm.localScale = new Vector3(gunArmScaleX, 1f, 1f);

            if (targetScaleX < 0)
            {
                angle += 180f; // Adjust the angle for correct rotation when facing left
            }

            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            gunArm.rotation = Quaternion.Lerp(gunArm.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            /* if (Input.GetMouseButtonDown(0))
             {
                 // Calculate bullet direction separately
                 Vector2 bulletDirection = (mousePos - screenPoint).normalized;
                 float bulletAngle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
                 Quaternion bulletRotation = Quaternion.Euler(0, 0, bulletAngle);

                 FireBullet(bulletRotation);
             }*/

            /* if (Input.GetMouseButton(0))
             {
                 shotCounter -= Time.deltaTime;

                 if (shotCounter <= 0)
                 {
                     // Calculate bullet direction separately
                     Vector2 bulletDirection = (mousePos - screenPoint).normalized;
                     float bulletAngle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
                     Quaternion bulletRotation = Quaternion.Euler(0, 0, bulletAngle);

                     FireBullet(bulletRotation);
                 }
             }
            */

            if (Input.GetKeyDown(KeyCode.Tab))
            {
              
                if (availableGun.Count > 0)
                {
                    currentGun++;
                    if (currentGun >= availableGun.Count)
                    {
                        currentGun = 0;
                    }
                  
                    SwitchGun();
                }
                else
                {
                    
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && dashCoolCounter <= 0)
            {
                ActiveMoveSpeed = dashSpeed;
                dashCounter = dashLenght;
                dashCoolCounter = dashcooldown;

                anim.SetTrigger("Dash");
                SoundEffects.instance.PlayerDashes();
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;

                if (dashCounter <= 0)
                {
                    ActiveMoveSpeed = moveSpeed;
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }




            if (moveInput != Vector2.zero)
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

    }
   /* void FireBullet(Quaternion bulletRotation)
    {
        Instantiate(bulletToFire, firePoint.position, bulletRotation);
        SoundEffects.instance.Shootpistol();
        shotCounter = timeBetweenShots;
    }

   public void FireRatePickup()
    {
        timeBetweenShots = minTimeBetweenShots;

    }

    public void DefaultFireRate()
    {
       
        timeBetweenShots = 0.2f;
    }*/

    public void SwitchGun()
    {
        foreach (Gun gun in availableGun)
        {
          
            gun.gameObject.SetActive(false);

           
        }
        availableGun[currentGun].gameObject.SetActive(true);
        UI_Controller.Instance.currentGun.sprite = availableGun[currentGun].gunUI;
        UI_Controller.Instance.gunText.text = availableGun[currentGun].weaponName;
    }
}