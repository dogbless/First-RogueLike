using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public int npcHealth;
    public GameObject splatter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (npcHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(splatter);
            SoundEffects.instance.DeadEnemy();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBuller")
        {
            npcHealth = 0;
        }
    }
}
