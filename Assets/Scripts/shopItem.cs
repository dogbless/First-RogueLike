using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shopItem : MonoBehaviour
{
    public GameObject buyMessage;
    private bool inBuyZone;
    public bool isHealthRestore, isHealthUpgrade, isWeapon;
    public Gun[] potentialGuns;
    private Gun theGun;
    public SpriteRenderer gunSprite;
    public int itemCost;
    public int upgradeHealthAmount;
    public Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        if (isWeapon)
        {
            int selectedGun = Random.Range(0, potentialGuns.Length);
            theGun = potentialGuns[selectedGun];

            gunSprite.sprite = theGun.gunShopSprite;
            infoText.text = theGun.weaponName + " - " + theGun.itemCost + "Gold - ";
            itemCost = theGun.itemCost;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (LevelManager.instance.currentCoins >= itemCost)
                {
                    LevelManager.instance.SpendCoins(itemCost);

                    if (isHealthRestore)
                    {
                        Player_Health.Instance.HealPlayer(Player_Health.Instance.maxHealth);
                    }

                    if (isHealthUpgrade)
                    {
                        Player_Health.Instance.increaseMaxHealth(upgradeHealthAmount);
                    }
                    if (isWeapon)
                    {
                        gunSprite.sprite = theGun.gunShopSprite;
                        Gun gunClone = Instantiate(theGun);
                        gunClone.transform.parent = PlayerController.instance.gunArm;
                        gunClone.transform.position = PlayerController.instance.gunArm.position;
                        gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        gunClone.transform.localScale = Vector3.one;

                        PlayerController.instance.availableGun.Add(gunClone);
                        PlayerController.instance.currentGun = PlayerController.instance.availableGun.Count - 1;
                        PlayerController.instance.SwitchGun();
                    }
                    
                    gameObject.SetActive(false);
                    inBuyZone = false;
                    SoundEffects.instance.shop();
                }
                else
                {
                    SoundEffects.instance.notEnough();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(true);

            inBuyZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(false);

            inBuyZone = false;
        }
    }
}
