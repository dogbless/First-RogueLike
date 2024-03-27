using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithNPC : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject notification;


    private void Start()
    {

    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            notification.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            notification.SetActive(true);
        }
    }

}
