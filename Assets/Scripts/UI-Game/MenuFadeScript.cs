using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class MenuFadeScript : MonoBehaviour
{
    public GameObject blackScreenObject;
    public float fadingTime;

    private Image blackImage;
    private Color blackOpaque = Color.black;
    private Color blackTransparent = Color.black;


    void Start()
    {
        blackImage = blackScreenObject.GetComponent<Image>();
        blackTransparent.a = 0f;
        Invoke(nameof(FadeOutBlackScreen), 1f);
    }

    public void FadeOutBlackScreen() //zanikanie
    {
        blackScreenObject.SetActive(true);
        blackImage.DOColor(blackTransparent, fadingTime);
        Invoke(nameof(BlackScreenChangeStatus),fadingTime);
    }

    public void FadeInBlackScreen() //pojawianie
    {
        blackScreenObject.SetActive(true);
        blackImage.DOColor(blackOpaque, fadingTime);
        Invoke(nameof(LoadMainScene), fadingTime);
    }

    public void LoadMainScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

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

    public void BlackScreenChangeStatus()
    {
        blackScreenObject.SetActive(!blackScreenObject.activeSelf);
    }
}
