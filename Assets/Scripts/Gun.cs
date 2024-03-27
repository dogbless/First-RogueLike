using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletToFire;
    public Transform firePoint;
    public float timeBetweenShots;
    public bool isShotgun; // Set this to true if you want to activate the shotgun behavior
    public bool isGumby; // Set this to true to activate Gumby behavior
    public string weaponName;
    public Sprite gunUI;
    private float shotCounter;
    public int itemCost;
    public Sprite gunShopSprite;

    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isShotgun)
                {
                    ShootShotgun();
                }
                else if (isGumby)
                {
                    ShootGumby();
                }
                else
                {
                    ShootBullet();
                }
            }

            if (Input.GetMouseButton(0))
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    if (isShotgun)
                    {
                        ShootShotgun();
                    }
                    else if (isGumby)
                    {
                        ShootGumby();
                    }
                    else
                    {
                        ShootBullet();
                    }
                    shotCounter = timeBetweenShots;
                }
            }
        }
    }

    void ShootBullet()
    {
        Instantiate(bulletToFire, firePoint.position, GetBulletRotation());
        SoundEffects.instance.Shootpistol();
    }

    void ShootShotgun()
    {
        // Instantiate 5 bullets in a spread
        for (int i = 0; i < 2; i++)
        {
            Quaternion spreadRotation = GetBulletRotation() * Quaternion.Euler(0f, 0f, Random.Range(-15f, 15f));
            Instantiate(bulletToFire, firePoint.position, spreadRotation);
        }
        SoundEffects.instance.Shootpistol();
    }

    void ShootGumby()
    {
        // Fire like a shotgun and a machine gun at the same time
        for (int i = 0; i < 5; i++)
        {
            Quaternion spreadRotation = GetBulletRotation() * Quaternion.Euler(0f, 0f, Random.Range(-15f, 15f));
            Instantiate(bulletToFire, firePoint.position, spreadRotation);
        }
        SoundEffects.instance.Shootpistol();
    }

    Quaternion GetBulletRotation()
    {
        float playerScaleX = PlayerController.instance.transform.localScale.x;

        if (playerScaleX > 0)
        {
            return firePoint.rotation;
        }
        else
        {
            Vector3 combinedDirection = new Vector3(0f, 180f, 0f) + firePoint.rotation.eulerAngles;
            return Quaternion.Euler(combinedDirection.x, combinedDirection.y, -combinedDirection.z);
        }
    }
}
