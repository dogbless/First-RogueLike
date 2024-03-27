using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject brokenPiece1;
    public GameObject[] Pieces;
    public GameObject EnemyPrefab;
    public int maxPieces = 5;
    public float enemySpawnChance = 0.05f;
    public GameObject[] ItemsToDrop;
    public float HealthSpawnChance = 0.10f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Update code if needed
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerController.instance.dashCounter > 0)
            {
                Destroy(gameObject);
                SoundEffects.instance.boxbroken();

                int piecesToDrop = Random.Range(0, maxPieces);
                for (int i = 0; i < piecesToDrop; i++)
                {
                    int randomPiece = Random.Range(0, Pieces.Length);
                    Instantiate(Pieces[randomPiece], gameObject.transform.position, transform.rotation);
                }

                if (Random.value <= enemySpawnChance)
                {
                    Instantiate(EnemyPrefab, gameObject.transform.position, transform.rotation);
                }

                if (Random.value <= HealthSpawnChance)
                {
                    int randomHealthIndex = Random.Range(0, ItemsToDrop.Length);
                    Instantiate(ItemsToDrop[randomHealthIndex], gameObject.transform.position, transform.rotation);
                }
            }
        }

        if (other.tag == "PlayerBullet")
        {
            Destroy(gameObject);
            SoundEffects.instance.boxbroken();
            int piecesToDrop = Random.Range(0, maxPieces);
            for (int i = 0; i < piecesToDrop; i++)
            {
                int randomPiece = Random.Range(0, Pieces.Length);
                Instantiate(Pieces[randomPiece], gameObject.transform.position, transform.rotation);
            }

            if (Random.value <= enemySpawnChance)
            {
                Instantiate(EnemyPrefab, gameObject.transform.position, transform.rotation);
            }

            if (Random.value <= HealthSpawnChance)
            {
                int randomHealthIndex = Random.Range(0, ItemsToDrop.Length);
                Instantiate(ItemsToDrop[randomHealthIndex], gameObject.transform.position, transform.rotation);
            }
        }
    }
}
