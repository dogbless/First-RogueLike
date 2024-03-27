using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects instance;
    public AudioSource BoxBreaking;
    public AudioSource enemyDeath;
    public AudioSource enemyHurt;
    public AudioSource Shoot1;
    public AudioSource PlayerDeath;
    public AudioSource playerHeal;
    public AudioSource playerHurt;
    public AudioSource playerDash;
    public AudioSource coinSound;
    public AudioSource shopBuySound;
    public AudioSource shopBadSound;


    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void boxbroken()
    {
        

        BoxBreaking.Play();
    }
    public void DeadEnemy()
    {
     

        enemyDeath.Play();
    }
    public void HurtEnemy()
    {
     

        enemyHurt.Play();
    }
    public void Shootpistol()
    {

        Shoot1.Play();
    }
    public void Playerdies()
    {
     

        PlayerDeath.Play();
    }
    public void Playerheals()
    {
       

        playerHeal.Play();
    }
    public void Playerhurt()
    {
      

        playerHurt.Play();
    }
    public void PlayerDashes()
    {
       

        playerDash.Play();
    }
    public void CoinPickedup()
    {
        coinSound.Play();
    }
    public void shop()
    {
        shopBuySound.Play();
    }
    public void notEnough()
    {
        shopBadSound.Play();
    }


}
