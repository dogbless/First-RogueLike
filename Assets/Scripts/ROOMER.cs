using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROOMER : MonoBehaviour
{
    // Start is called before the first frame update
    public bool closeWhenEntered;
   // public bool openWhenEnemiesAreCleared;

    public GameObject[] doors;
    // Start is called before the first frame update
    //public List<GameObject> enemies = new List<GameObject>();
    [HideInInspector]
    public bool roomActive;
    public GameObject mapHider;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (enemies.Count > 0 && roomActive && openWhenEnemiesAreCleared)
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
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);

                    closeWhenEntered = false;
                }
            }
        }*/
    }


    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);

            closeWhenEntered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);


            if (closeWhenEntered)
            {
                mapHider.SetActive(false);
            
                StartCoroutine(CloseDoorsDelayed(0.4f)); // Adjust the delay time as needed
            }

            roomActive = true;

            mapHider.SetActive(false);
        }
    }
    private IEnumerator CloseDoorsDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            roomActive = false;
            OpenDoors();
        }
    }
}
