using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitToLoad = 4;

    public string nextLevel;
    public bool isPaused;
    public int currentCoins;
    public Transform startPoint;
    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.transform.position = startPoint.position;
        PlayerController.instance.canMove = true;
        currentCoins = CharacterTracker.Instance.currentCoins;
        Time.timeScale = 1f;
        UI_Controller.Instance.coinText.text = currentCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        AudioManger.instance.PlayLevelWin();

        PlayerController.instance.canMove = false;
        UI_Controller.Instance.StartFadeToBlack();
        yield return new WaitForSeconds(waitToLoad);
        CharacterTracker.Instance.currentCoins = currentCoins;
        CharacterTracker.Instance.currentHealth = Player_Health.Instance.currenthealth;
        CharacterTracker.Instance.maxHealth = Player_Health.Instance.maxHealth;

        SceneManager.LoadScene(nextLevel);


    }

    public void PauseUnpause()
    {
        if (!isPaused)
        {
            UI_Controller.Instance.pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            UI_Controller.Instance.pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void GetCoins(int amount)
    {
        currentCoins += amount;
        UI_Controller.Instance.coinText.text = currentCoins.ToString();
    }

    public void SpendCoins(int amount)
    {
        currentCoins -= amount;

        if (currentCoins < 0)
        {
            currentCoins = 0;
        }
        UI_Controller.Instance.coinText.text = currentCoins.ToString();
    }
}
