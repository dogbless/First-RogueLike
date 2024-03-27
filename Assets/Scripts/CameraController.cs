using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraController instance;
    public float moveSpeed;
    public Transform target;
    public Camera mainCamera,bigMapCamera;
    private bool bigMapActive;
    public bool isBossRoom;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (isBossRoom)
        {
            target = PlayerController.instance.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
          

            // Calculate the center of the room based on the collider bounds
            Vector3 roomCenter = target.GetComponent<Collider2D>().bounds.center;

            // Move towards the center of the room
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(roomCenter.x, roomCenter.y, transform.position.z), moveSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.M) && !isBossRoom)
        {
            if (!bigMapActive)
            {
                ActivateBigMap();
            }
            else
            {
                DeactivateBigMap();
            }
            
        }

    }

    public void ChangeTarget(Transform newTarget)
    {
        

        target = newTarget;

    }

    public void ActivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = true;

            bigMapCamera.enabled = true;
            mainCamera.enabled = false;

            PlayerController.instance.canMove = false;
            Time.timeScale = 0;
            UI_Controller.Instance.mapDisplay.SetActive(false);
            UI_Controller.Instance.bigMapText.SetActive(true);
        }
       
    }

    public void DeactivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = false;

            bigMapCamera.enabled = false;
            mainCamera.enabled = true;
            PlayerController.instance.canMove = true;
            Time.timeScale = 1;
            UI_Controller.Instance.mapDisplay.SetActive(true);
            UI_Controller.Instance.bigMapText.SetActive(false);
        }
    }
}
