using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScene : MonoBehaviour
{
    public GameObject mainCamera;
    public Transform startCameraPos;
    public Transform endCameraPos;

    public GameObject doorsTrigger;

    public GameObject leftDoor;
    public GameObject rightDoor;

    public Transform leftDoorPosClose;
    public Transform rightDoorPosClose;

    public GameObject blackScreenObject;
    public GameObject playerObject;

    public float fadingTime;
    public float speed;

    private Image blackImage;
    private Color blackOpaque = Color.black;
    private Color blackTransparent = Color.black;

    public TMP_Text distanceText;
    public TMP_Text numberObjectText;
    public TMP_Text biomeText;

    public GameObject statusMenu;

    private GameObject playButtonObject;
    private GameObject endButtonObject;

    private Button playButton;
    private Button endButton;

    void Start()
    {
        blackImage = blackScreenObject.GetComponent<Image>();
        blackTransparent.a = 0f;
        Invoke(nameof(FadeOutBlackScreen), 1f);
        Invoke(nameof(GameOver),1.2f);


        mainCamera.transform.SetPositionAndRotation(startCameraPos.position, startCameraPos.rotation);
        PlayerPrefs.SetInt("playerDead", 1);
        PlayerPrefs.SetInt("runON", 0);
    }


    public void GameOver()
    {
        Sequence gameOverSeq = DOTween.Sequence()
            .Append(playerObject.transform.DOMove(new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, 10f), 1).SetEase(Ease.Linear))
            .Append(leftDoor.transform.DORotateQuaternion(leftDoorPosClose.rotation, 1f))
            .Join(rightDoor.transform.DORotateQuaternion(rightDoorPosClose.rotation, 0.6f))
            .Join(mainCamera.transform.DOMove(endCameraPos.position, 1f).SetEase(Ease.InQuart))
            .Join(mainCamera.transform.DORotateQuaternion(endCameraPos.rotation, 0.5f));

        gameOverSeq.OnComplete(() => {
            ActiveStatusMenu();
        });
    }

    public void CheckSetScores()
    {
        int lastScore = PlayerPrefs.GetInt("distanceTraveled");
        int highScore = PlayerPrefs.GetInt("highscore");
        int totalScore = PlayerPrefs.GetInt("totalscore");

        if (lastScore != 0)
        {
            if (lastScore > highScore)
            {
                PlayerPrefs.SetInt("highscore", lastScore);
                PlayerPrefs.SetInt("highscoreObject", PlayerPrefs.GetInt("numberObject"));
            }
            totalScore += lastScore;
        }

        PlayerPrefs.SetInt("totalscore", totalScore);
    }

    public void MainMenuButton()
    {
        FadeInBlackScreen();
        Invoke(nameof(LoadMainScene), fadingTime);
        PlayerPrefs.SetInt("playAgain", 0);
    }
    
    public void PlayAgainButton()
    {
        FadeInBlackScreen();
        Invoke(nameof(LoadMainScene), fadingTime);
        PlayerPrefs.SetInt("playAgain", 1);
    }
    public void FadeOutBlackScreen() //zanikanie
    {

        blackScreenObject.SetActive(true);
        blackImage.DOColor(blackTransparent, fadingTime);
        Invoke(nameof(BlackScreenChangeStatus), fadingTime);
    }

    public void ActiveStatusMenu()
    {
        statusMenu.SetActive(true);
        distanceText.text = PlayerPrefs.GetInt("distanceTraveled").ToString() + " m";
        numberObjectText.text = PlayerPrefs.GetInt("numberObject").ToString();
        biomeText.text = PlayerPrefs.GetString("currentBiome");

        endButtonObject = GameObject.Find("MainMenuButton");
        endButton = endButtonObject.GetComponent<Button>();
        playButtonObject = GameObject.Find("PlayAgainButton");
        playButton = playButtonObject.GetComponent<Button>();

        
        playButton.Select();
    }

    public void FadeInBlackScreen() //pojawianie
    {
        CheckSetScores();
        blackScreenObject.SetActive(true);
        blackImage.DOColor(blackOpaque, fadingTime);
    }

    public void BlackScreenChangeStatus()
    {
        blackScreenObject.SetActive(!blackScreenObject.activeSelf);
    }

    public void LoadMainScene() { StartCoroutine(LoadSceneAsync()); }

    IEnumerator LoadSceneAsync()
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

}
