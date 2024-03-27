using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VicoryScreen : MonoBehaviour { 

    public float waitForAnyKey = 2f;
    public GameObject anyKeyTest;
    public string mainMenuScene;
    public AudioSource Music;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        playMusic(); 
        Destroy(PlayerController.instance.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(waitForAnyKey > 0)
        {
            waitForAnyKey -= Time.deltaTime;
            if (waitForAnyKey <= 0)
            {
                anyKeyTest.SetActive(true);
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(mainMenuScene);
            }
        }

    }

    public void playMusic()
    {
        Music.Play();
    }
}
