using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class OptionsMainMenu : MonoBehaviour
{
    public GameObject gameController;
    private GameSettings gameSets;

    public GameObject diffObj;
    private TMP_Dropdown diffDropDown;
    
    public GameObject dayTimeObj;
    private TMP_Dropdown dayTimeDropDown;

    public GameObject hintsObj;
    private Toggle hintsToggle;

    public GameObject fullscreenObj;
    private Toggle fullscreenToggle;

    public GameObject musicObj;  //to do
    private Slider musicSlider;

    public GameObject soundObj; //to do
    private Slider soundSlider;

    public GameObject ressObj;
    private TMP_Dropdown ressDropDown;

    public GameObject sun;
    private DayTimeScript sunScript;

    public GameObject uiManagerObject;
    private UIManager managerUI;

    public GameObject lastScore;
    private TMP_Text lastText;
    
    public GameObject highScore;
    private TMP_Text highText;
    
    public GameObject totalScore;
    private TMP_Text totalText;

    public GameObject objectNumber;
    private TMP_Text objectText;

    Resolution[] resolutions;

    public GameObject backButtonObject;
    private Button backButton;

    public GameObject applyButtonObject;
    private Button applyButton;

    public GameObject optionsTriggerObject;
    private OptionsTrigger optionsTrigger;

    GameObject gameOBJ;
    public bool showOBJ;

    public GameObject arrowObject;
    public List<GameObject> mainActivableObjects;
    private Image arrowPanel;

    private GameObject currentActiveObject;
    private GameObject lastActiveObject;
    void Start()
    {
        gameSets = gameController.GetComponent<GameSettings>();
        diffDropDown = diffObj.GetComponent<TMP_Dropdown>();
        hintsToggle = hintsObj.GetComponent<Toggle>();
        fullscreenToggle = fullscreenObj.GetComponent<Toggle>();
        musicSlider = musicObj.GetComponent<Slider>();
        soundSlider = soundObj.GetComponent<Slider>();
        ressDropDown = ressObj.GetComponent<TMP_Dropdown>();
        dayTimeDropDown = dayTimeObj.GetComponent<TMP_Dropdown>();
        sunScript = sun.GetComponent<DayTimeScript>();
        managerUI = uiManagerObject.GetComponent<UIManager>();

        lastText = lastScore.GetComponent<TMP_Text>();
        highText = highScore.GetComponent<TMP_Text>();
        totalText = totalScore.GetComponent<TMP_Text>();
        objectText = objectNumber.GetComponent<TMP_Text>();

        GetHints();
        GetFullscreen();
        GetDifficulty();
        GetGameResolution();
        GetMusic();
        GetSound();
        GetDayTime();
        SetScores();

        optionsTrigger = optionsTriggerObject.GetComponent<OptionsTrigger>();
        SetButtonActive();

        currentActiveObject = EventSystem.current.currentSelectedGameObject;
        arrowPanel = arrowObject.GetComponentInChildren<Image>();


        //arrowPanel.DOFade(0f, 0.5f).SetLoops(-1, LoopType.Yoyo);

        Sequence mySequence = DOTween.Sequence();
            mySequence.Append(arrowPanel.DOFade(0f, 1f).SetEase(Ease.InQuart).SetLoops(2,LoopType.Yoyo)).SetDelay(0.6f);        
        
        mySequence.SetLoops(-1, LoopType.Restart);
    }

    public void SetButtonActive()
    {
        backButton = backButtonObject.GetComponent<Button>();
        backButton.Select();
    }

    public void BackToMenu()
    {
        gameObject.SetActive(false);
        optionsTrigger.SetButtonActive();
    }
    void Update()
    {
        if (showOBJ)
        {
            gameOBJ = EventSystem.current.currentSelectedGameObject;
            
            //GameObject objPARENT = gameOBJ.transform.parent
            Debug.Log(gameOBJ);
        }
        CheckActiveObject();
    }


    public void CheckActiveObject()
    {
        currentActiveObject = EventSystem.current.currentSelectedGameObject;
        if (lastActiveObject != currentActiveObject)
        {
            foreach(GameObject activableObject in mainActivableObjects)
            {
                if (currentActiveObject == activableObject)
                {
                    lastActiveObject = currentActiveObject;
                    if (currentActiveObject == applyButtonObject || currentActiveObject == backButtonObject)
                    {
                        arrowObject.transform.DOScaleX(0.3f, 0.2f);
                        arrowObject.transform.DOLocalMoveY(currentActiveObject.transform.localPosition.y, 0.2f);
                        arrowObject.transform.DOLocalMoveX(currentActiveObject.transform.localPosition.x, 0.2f);
                    }
                    else
                    {
                        arrowObject.transform.DOLocalMoveY(currentActiveObject.transform.localPosition.y, 0.2f);
                        arrowObject.transform.DOLocalMoveX(354f, 0.2f);
                        arrowObject.transform.DOScale(1f, 0.2f);
                    }
                    break;
                }
            }    
        }
    }

    public void Scroll()
    {
        var siema = ressDropDown.transform.GetChild(3).gameObject;
        var elo = siema.GetComponent<ScrollRect>();

        showOBJ = true;
        //siema.verticalScrollbar.transform.position = new Vector2(siema.verticalScrollbar.transform.position.x, 2);

        //Scrollbar scrollbar = gameObject.GetComponentInChildren<ScrollRect>()?.verticalScrollbar;

        //if (options.Count > 1 && scrollbar != null)
        //{
        //    if (scrollbar.direction == Scrollbar.Direction.TopToBottom)
        //    {
        //        scrollbar.value = Mathf.Max(0.001f, (float)(value) / (float)(options.Count - 1));
        //    }
        //    else
        //    {
        //        scrollbar.value = Mathf.Max(0.001f, 1.0f - (float)(value) / (float)(options.Count - 1));
        //    }      
        //
        //}
    }


        public void OnApplyButtonClick()
    {
        var actualHintsON = hintsToggle.isOn;
        var actualFullscreenON = fullscreenToggle.isOn;
        var actualDiffLevel = diffDropDown.value;
        var actualMusicVolume = musicSlider.value;
        var actualSoundVolume = soundSlider.value;
        var actualResolution = ressDropDown.value;
        var actualDayTime = dayTimeDropDown.value;

        SetHints(actualHintsON);
        SetFullscreen(actualFullscreenON);
        SetDifficulty(actualDiffLevel);
        SetGameResolution(actualResolution);
        SetMusic(actualMusicVolume);
        SetSound(actualSoundVolume);
        SetDayTime(actualDayTime);
    }

    public void GetHints()
    {
        int turnHintsON = PlayerPrefs.GetInt("hintsON");

        if (turnHintsON == 1)
        {
            hintsToggle.isOn = true;
        }
        else
        {
            hintsToggle.isOn = false;
        }
    }

    void SetHints(bool hintsON)
    {
        if (hintsON)
        {
            PlayerPrefs.SetInt("hintsON", 1);
            gameSets.SetHints(1);
            managerUI.ChangeHintsStatus();
        }
        else
        {
            PlayerPrefs.SetInt("hintsON", 0);
            gameSets.SetHints(0);
            managerUI.ChangeHintsStatus();
        }
    }

    public void GetFullscreen()
    {
        int goFullscreen = PlayerPrefs.GetInt("fullscreen");

        if (goFullscreen == 1)
        {
            fullscreenToggle.isOn = true;
        }
        else
        {
            fullscreenToggle.isOn = false;
        }
    }

    public void SetFullscreen(bool fullscreenON)
    {
        if (fullscreenON)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
            gameSets.SetFullscreen(1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);
            gameSets.SetFullscreen(0);
        }
    }

    public void GetDifficulty() { diffDropDown.value = PlayerPrefs.GetInt("difficulty"); }

    void SetDifficulty(int diffLevel)
    {
        PlayerPrefs.SetInt("difficulty", diffLevel);
        gameSets.SetDifficulty(diffLevel);
    }

    public void GetGameResolution()
    {
        resolutions = Screen.resolutions;
        ressDropDown.ClearOptions();
        List<string> options = new List<string>();

        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + "@" + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResIndex = i;
            }
        }

        ressDropDown.AddOptions(options);
        ressDropDown.value = currentResIndex;
        ressDropDown.RefreshShownValue();
    }

    public void SetGameResolution(int resIndex) { gameSets.SetApplyResolution(resolutions[resIndex].height, resolutions[resIndex].width, resolutions[resIndex].refreshRate); }

    public void GetMusic() { musicSlider.value = PlayerPrefs.GetFloat("musicVolume"); }

    public void SetMusic(float musicVolume)
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
    }
    public void GetDayTime() { dayTimeDropDown.value = PlayerPrefs.GetInt("dayTime"); }

    public void SetDayTime(int dayTime)
    {
        PlayerPrefs.SetInt("dayTime", dayTime);
        if (dayTime == 2 || dayTime == 3)
        {
            PlayerPrefs.SetInt("lightsON", 1);
        }
        else
        {
            PlayerPrefs.SetInt("lightsON", 0);
        }

        gameSets.SetDayTime(dayTime);
        sunScript.SetSunPosition();
    }

    public void GetSound()
    {
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }

    public void SetSound(float soundVolume)
    {
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
        //gameSets.SetSoundVolume(soundVolume*0.05f); <----------to do
    }

    public void SetScores()
    {
        objectText.text = "Test Object No. " + (PlayerPrefs.GetInt("numberObject") + 1).ToString();
        lastText.text = "No. " + PlayerPrefs.GetInt("numberObject").ToString() +"\n" + PlayerPrefs.GetInt("distanceTraveled").ToString() +"m";
        highText.text = "No. " + PlayerPrefs.GetInt("highscoreObject").ToString() + "\n" + PlayerPrefs.GetInt("highscore").ToString() + "m";
        totalText.text = PlayerPrefs.GetInt("numberObject").ToString() + " Objects\n" + PlayerPrefs.GetInt("totalscore").ToString() + "m";   
    }
}
