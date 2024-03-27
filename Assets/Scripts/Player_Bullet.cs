using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRB;
    public GameObject ImpactEffect;
    public int DamageTogive = 50;
    
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed;
        
      
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
      

        Instantiate(ImpactEffect, transform.position, transform.rotation);
        SoundEffects.instance.Shootpistol();
        Destroy(gameObject);

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                
                enemy.DamageEnemy(DamageTogive);
            }
           

        }
        if (other.tag == "Boss")
        {

            BossController.instance.TakeDamage(DamageTogive);

            Instantiate(BossController.instance.hitEffect, transform.position, transform.rotation);
        }


    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);

    }
}
