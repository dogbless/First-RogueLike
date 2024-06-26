using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public BossAction[] actions;
    private int currentAction;
    private float actionCounter;
    private float shotCounter;
    public Rigidbody2D theRB;
    private Vector2 moveDirection;
    public int currentHealth;
    public GameObject deathEffect,hitEffect;
    public GameObject levelExit;
    public BossSequence[] sequences;
    public int currentSequence;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        actions = sequences[currentSequence].actions;
        actionCounter = actions[currentAction].actionLength;
        UI_Controller.Instance.bossHealthBar.maxValue = currentHealth;
        UI_Controller.Instance.bossHealthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (actionCounter > 0)
        {
            actionCounter-=Time.deltaTime;

            //handle movement
            moveDirection = Vector2.zero;
            if (actions[currentAction].shouldMove)
            {
                if (actions[currentAction].shouldChasePlayer)
                {
                    moveDirection = PlayerController.instance.transform.position - transform.position;
                    moveDirection.Normalize();  
                }
                if (actions[currentAction].moveToPoints)
                {
                    moveDirection = actions[currentAction].pointToMove.position - transform.position;
                }
            }
            theRB.velocity = moveDirection * actions[currentAction].moveSpeed;
            //handle shooting
            if (actions[currentAction].shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = actions[currentAction].timeBetweenShots;

                    foreach (Transform t in actions[currentAction].shotPoints)
                    {
                        Instantiate(actions[currentAction].itemTOShoot,t.position,t.rotation);
                    }
                }
            }
        }
        else
        {
            currentAction++; 
            if (currentAction >= actions.Length)
            {
                currentAction = 0;
            }
            actionCounter = actions[currentAction].actionLength;

        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);

            Instantiate(deathEffect,transform.position, transform.rotation);

            

            if(Vector3.Distance(PlayerController.instance.transform.position,levelExit.transform.position) > 2f)
            {
                levelExit.transform.position += new Vector3(4f, 0f, 0f);
            }
            levelExit.SetActive(true);
            UI_Controller.Instance.bossHealthBar.gameObject.SetActive(false);


        }
        else
        {
            if (currentHealth <= sequences[currentSequence].endsequenceHealth && currentSequence < sequences.Length - 1) 
            {
                currentSequence++;
                actions = sequences[currentSequence].actions;
                currentAction = 0;
                actionCounter = actions[currentAction].actionLength;
            }
        }
        UI_Controller.Instance.bossHealthBar.value = currentHealth;
    }
}
[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;
    public bool shouldMove;
    public bool shouldChasePlayer;
    public bool moveToPoints;
    public float moveSpeed;
    public Transform pointToMove;


    public bool shouldShoot;
    public GameObject itemTOShoot;
    public float timeBetweenShots;
    public Transform[] shotPoints;


}

[System.Serializable]
public class BossSequence
{
    [Header("Sequcence")]
    public BossAction[] actions;

    public int endsequenceHealth;
}
