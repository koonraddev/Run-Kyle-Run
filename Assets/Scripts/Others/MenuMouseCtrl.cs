using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMouseCtrl : MonoBehaviour
{
    void Start()
    {
        string loadedSceneName = SceneManager.GetActiveScene().name;
        CursorStatus(loadedSceneName);
    }

    public void CursorStatus(string sceneName)
    {
        switch (sceneName)
        {
            case "StartScene":
            case "GameOverScene":
            case "MainScene":
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            default:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
}
