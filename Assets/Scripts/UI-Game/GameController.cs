using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject gamePausedMenu;

    public GameObject blackScreen;

    public GameObject sun;
    private Animation anim;

    public int distanceTraveled;
    public bool gameOver;
    public bool gamePaused;

    public GameObject playerObject;
    private PlayerMovement playerMov;
    private PlayerHealth playerHP;

    public int maxHealth;

    public GameObject mainCam;
    private CameraMovement camMov;

    public GameObject blackScreenObject;
    private Image blackImage;
    private Color blackOpaque = Color.black;
    public float fadingTime;
    private bool gameON;

    public Resolution elo;

    public int powerRunConsume;
    public int powerCharge;

    private int power;
    private int maxPower;
    private float powerDownCounter;
    private float powerUpCounter;
    private bool isPower;
    void Start()
    {
        maxPower = 100;
        power = maxPower;
        gameOver = false;
        gamePaused = false;
        playerMov = playerObject.GetComponent<PlayerMovement>();
        camMov = mainCam.GetComponent<CameraMovement>();
        playerHP = playerObject.GetComponent<PlayerHealth>();
        blackImage = blackScreenObject.GetComponent<Image>();
        PlayerPrefs.SetInt("playerDead", 0);
        anim = sun.GetComponent<Animation>();
        powerDownCounter = 0;
        powerUpCounter = 0;
    }

    void Update()
    {
        gameON = playerMov.runON;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (power >= powerRunConsume)
            {
                isPower = true;
                PowerDown(powerRunConsume);
            }
            else
            {
                isPower = false;
            }
        }
        else
        {
            if(power < maxPower)
            {
                PowerUP(powerCharge);
            }
        }

        if (playerHP.dead == true)
        {
            GameOver();
        }
    }



    private void PowerUP(int powerAdd)
    {
        powerUpCounter += Time.deltaTime;
        if (powerUpCounter >= 1)
        {
            power += powerAdd;
            powerUpCounter = 0;
        }
    }

    private void PowerDown(int powerConsume)
    {
        powerDownCounter += Time.deltaTime;
        if (powerDownCounter >= 1)
        {
            power -= powerConsume;
            powerDownCounter = 0;
        }

    }
    public int GetPowerValue() { return power; }

    public int GetMaxPowerValue() { return maxPower; }

    public bool GetPowerStatus() { return isPower; }


    public int GetMaxHealthValue() { return maxHealth; }

    public void StopMovingObjects()
    {
        Ground_Script[] groundObjects = FindObjectsOfType<Ground_Script>();
        foreach (Ground_Script obj in groundObjects)
        {
            obj.FreezeObject();
        }
    }

    public void ResumeMovingObjects()
    {
        Ground_Script[] groundObjects = FindObjectsOfType<Ground_Script>();
        foreach (Ground_Script obj in groundObjects)
        {
            obj.MoveObject();
        }
    }

    /*
    public void PauseGame()
    {
        Debug.Log("paused");
        gamePaused = true;
        gamePausedMenu.SetActive(true);
        StopMovingObjects();
        playerMov.RunChangeStatus();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("resumed");
        gamePaused = false;
        gamePausedMenu.SetActive(false);
        ResumeMovingObjects();
        playerMov.RunChangeStatus();
        Time.timeScale = 1f;
    }
    */

    public void GameOver()
    {
        PlayerPrefs.SetInt("distanceTraveled", distanceTraveled);

        if (PlayerPrefs.GetInt("dayTime") == 4)
        {
            anim["SunDayNightCycle"].speed = 0f;
            PlayerPrefs.SetFloat("sunAnimTime", anim["SunDayNightCycle"].time);
        }
        playerMov.RunChangeStatus();
        camMov.RunChangeStatus();
        StopMovingObjects();
        blackScreen.SetActive(true);
        blackScreen.GetComponent<Image>().color = Color.black;
        StartCoroutine(LoadSceneAsync());
        Time.timeScale = 1f;
    }
    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);

        while (!operation.isDone)
        {
            yield return new WaitForSecondsRealtime(2);
        }
    }

    IEnumerator LoadMainScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (blackImage.color == blackOpaque)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void FadeInBlackScreen() //pojawianie
    {
        blackScreenObject.SetActive(true);
        blackImage.DOColor(blackOpaque, fadingTime);
        Invoke(nameof(LoadMainMenu), fadingTime);
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadMainScene());
    }
}