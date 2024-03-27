using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public static Player_Health Instance;
    public int currenthealth;
    public int maxHealth;
    public float damageInvinceLength = 1f;
    private float invinceCount;
    public float cooldown = 0f;

    private bool isFirstBuy = true;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        maxHealth = CharacterTracker.Instance.maxHealth;
        currenthealth = CharacterTracker.Instance.currentHealth;
        //currenthealth = maxHealth;

        UI_Controller.Instance.healthslider.maxValue = maxHealth;
        UI_Controller.Instance.healthslider.value = currenthealth;
        UI_Controller.Instance.healthtext.text = currenthealth.ToString() + " / " + maxHealth.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        UI_Controller.Instance.healthtext.text = $"{currenthealth} / {maxHealth}";
        if (Input.GetKeyDown(KeyCode.Space) && cooldown == 0)
        {
            invinceCount += 1;
            cooldown = 1;
           
        }
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            cooldown = Mathf.Max(0, cooldown);
        }
        UI_Controller.Instance.healthslider.value = currenthealth;
        if (invinceCount > 0)
        {
            invinceCount -= Time.deltaTime;
            if(invinceCount <= 0)
            {
                PlayerController.instance.bodysr.color = new Color(PlayerController.instance.bodysr.color.r, PlayerController.instance.bodysr.color.g, PlayerController.instance.bodysr.color.b, 1f);
            }
        }

    }

    public void damagePlayer()
    {// -- is the same as -= 1
        
        if (invinceCount <= 0)
        {
            currenthealth -= 1;
            invinceCount = damageInvinceLength;
            PlayerController.instance.bodysr.color = new Color(PlayerController.instance.bodysr.color.r, PlayerController.instance.bodysr.color.g, PlayerController.instance.bodysr.color.b, .5f);
            if (currenthealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UI_Controller.Instance.deathScreen.SetActive(true);
                SoundEffects.instance.Playerdies();
                AudioManger.instance.PlayGameOver();

            }
            UI_Controller.Instance.healthslider.value = currenthealth;
            UI_Controller.Instance.healthtext.text = currenthealth.ToString() + " / " + maxHealth.ToString();
        }
    }

    public void HealPlayer(int healAmount)
    {
        currenthealth += healAmount;
        SoundEffects.instance.Playerheals();
        if (currenthealth > maxHealth)
        {
            currenthealth = maxHealth;
        }

        UI_Controller.Instance.healthslider.value = currenthealth;
        UI_Controller.Instance.healthtext.text = currenthealth.ToString() + " / " + maxHealth.ToString();
    }
    // Inside Player_Health script
    public void increaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currenthealth = maxHealth;

        // Update healthslider properties
        UI_Controller.Instance.healthslider.maxValue = maxHealth;
        UI_Controller.Instance.healthslider.value = currenthealth;

        // Update healthtext immediately
        UI_Controller.Instance.healthtext.text = $"{currenthealth} / {maxHealth}";

        if (isFirstBuy)
        {
            // Activate the function a second time for the first buy
            isFirstBuy = false;
            increaseMaxHealth(amount);
        }
    }


}
