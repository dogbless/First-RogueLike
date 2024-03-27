using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
public class DamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player_Health.Instance.damagePlayer();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player_Health.Instance.damagePlayer();
        }
    }


    private void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player_Health.Instance.damagePlayer();
        }
    }

    private void OnCollisionStay2D(UnityEngine.Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player_Health.Instance.damagePlayer();
        }
    }

}
