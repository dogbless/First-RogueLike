using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : MonoBehaviour
{
    public Gun_Pickup[] potentialGuns;

    public SpriteRenderer theSR;
    public Sprite chestOpen;
    public GameObject notification;
    public Transform spawnPoint;
    private bool canOpen,isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                int gunSelect = Random.Range(0, potentialGuns.Length);

                Instantiate(potentialGuns[gunSelect],spawnPoint.position,spawnPoint.rotation);

                theSR.sprite = chestOpen;

                notification.SetActive(false);
                isOpen = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canOpen = true;
            notification.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canOpen = false;
            notification.SetActive(false);

        }
    }
}
