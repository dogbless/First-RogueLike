using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class roomCenter : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();

    public bool openWhenEnemiesCleared;
    public ROOMER theRoom;
    // Start is called before the first frame update
    void Start()
    {
        if (openWhenEnemiesCleared && !levelGenrator.instance.shopRoom | openWhenEnemiesCleared && !levelGenrator.instance.centerStart)
        {
            theRoom.closeWhenEntered = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && theRoom.roomActive && openWhenEnemiesCleared)
        {
            for (global::System.Int32 i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
            if (enemies.Count == 0)
            {
                theRoom.OpenDoors();
            }
        }
    }
}
