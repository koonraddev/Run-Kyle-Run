using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSettings : MonoBehaviour
{
    private int difficulty;
    private int hintsON; //1 - on; 0 - off
    private int fullscreenON; //1 - on; 0 - off

    //private float musicVolume;
    //private float soundVolume;
    //private int resWidth;
    //private int resHeight;

    private int dayTime;


    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        difficulty = PlayerPrefs.GetInt("difficulty");
        hintsON = PlayerPrefs.GetInt("hintsON");
        fullscreenON = PlayerPrefs.GetInt("fullscreen");
        //musicVolume = PlayerPrefs.GetFloat("musicVolume");
        //soundVolume = PlayerPrefs.GetFloat("soundVolume");
        //resWidth = PlayerPrefs.GetInt("resolutionWidth");
        //resHeight = PlayerPrefs.GetInt("resolutionHeight");
        dayTime = PlayerPrefs.GetInt("dayTime");
    }

    public void SetDifficulty(int difficultyValue)
    {
        difficulty = difficultyValue;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }
    public int GetBaseSpeed()
    {
        return difficulty switch
        {
            0 => 15,
            1 => 25,
            2 => 30,
            _ => 20,
        };
    }
    public int GetRunSpeed()
    {
        return difficulty switch
        {
            0 => 20,
            1 => 30,
            2 => 35,
            _ => 25,
        };
    }

    public float GetAnimBaseSpeed()
    {
        return difficulty switch
        {
            0 => 0.9f,
            1 => 1.25f,
            2 => 1.5f,
            _ => 1f,
        };
    }
    public float GetAnimRunSpeed()
    {
        return difficulty switch
        {
            0 => 1.2f,
            1 => 1.5f,
            2 => 2f,
            _ => 1f,
        };
    }

    public int GetDamge()
    {
        if (difficulty == 2)
        {
            return 50;
        }
        else
        {
            return 20;
        }
    }

    public void SetHints(int hintsINT)
    {
        hintsON = hintsINT;
    }

    public int GetHints()
    {
        return hintsON;
    }

    public void SetFullscreen(int fullON)
    {
        fullscreenON = fullON;
    }
    public void SetApplyResolution(int resH, int resW, int fps)
    {
        //resHeight = resH;
        //resWidth = resW;
        if (fullscreenON == 1)
        {
            Screen.SetResolution(resW, resH, true); ;
            Application.targetFrameRate = fps;
        }
        else
        {
            Screen.SetResolution(resW, resH, false);
            Application.targetFrameRate = fps;
        }
    }
    
    public void SetDayTime(int dayTimeValue)
    {
        dayTime = dayTimeValue;
    }
    public int GetDayTime()
    {
        return dayTime;
    }
}
