using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    private SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();

        SR.sortingOrder = Mathf.RoundToInt(transform.position.y * -10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
