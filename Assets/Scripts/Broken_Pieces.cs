using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken_Pieces : MonoBehaviour
{

    public float moveSpeed = 3f;
    private Vector3 moveDirection;
    // Start is called before the first frame update

    public float deceleration = 5;
    public float lifeTime = 3f;
    public SpriteRenderer SR;
    public float fadeOutSpeed = 2.5f;

    void Start()
    {
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;

        moveDirection = Vector3.Lerp(moveDirection,Vector3.zero,deceleration*Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {

            SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, Mathf.MoveTowards(SR.color.a, 0f, fadeOutSpeed * Time.deltaTime));



            if(SR.color.a == 0f)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
