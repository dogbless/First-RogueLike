using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI_Controller : MonoBehaviour
{
    public static UI_Controller Instance;
    public Slider healthslider;
    public Text healthtext,coinText;
    public GameObject deathScreen;
    public Image fadeScreen;
    public float fadeSpeed;

    private bool fadeToBlack;
    private bool fadeOutBlack;

    public string newGameScene, MainMenuScene;

    public GameObject pauseMenu,mapDisplay, bigMapText;

    public Image currentGun;

    public Text gunText;

    public Slider bossHealthBar;
    // Start is called before the first frame update


    public void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        fadeOutBlack = true;
        fadeToBlack = false;
        currentGun.sprite = PlayerController.instance.availableGun[PlayerController.instance.currentGun].gunUI;
        gunText.text = PlayerController.instance.availableGun[PlayerController.instance.currentGun].weaponName;

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a,0f,fadeSpeed*Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }

        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a,1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }

        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false ;
    }

    public void newGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameScene);
        Destroy(PlayerController.instance.gameObject);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenuScene);
        Destroy(PlayerController.instance.gameObject);
    }

    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }
    public void LogHealthSliderText()
    {
        Debug.Log("HealthSlider Text: " + healthtext.text);
    }
}
