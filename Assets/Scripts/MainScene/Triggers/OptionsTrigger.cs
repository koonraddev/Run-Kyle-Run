using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class OptionsTrigger : MonoBehaviour
{
    public Camera cam;
    private CameraStartMenu camCtr;
    public GameObject optionsMenu;
    public Transform optionsCameraPlace;
    public GameObject optionsTextObject;
    private TMP_Text optionsText;

    public GameObject laptopOFF;
    public GameObject laptopON;
    public GameObject lightOptions;

    public bool lightMustBeON;
    private bool lightsON;
    private int dayTime;

    public float speed;
    [Header("Colors")]
    public Color basicColor;
    public Color mouseOnColor;
    public float changeDuration;
   
    void Start()
    {
        camCtr = cam.GetComponent<CameraStartMenu>();
        optionsText = optionsTextObject.GetComponent<TMP_Text>();
        optionsText.color = basicColor;
        lightMustBeON = false;
    }

    void Update()
    {
        var dayTime = PlayerPrefs.GetInt("dayTime");
        var lights = PlayerPrefs.GetInt("lightsON");
        if ((dayTime == 3 || dayTime == 4) && lights == 1) { lightsON = true; }

        bool setActive = cam.transform.position == optionsCameraPlace.position;
        optionsMenu.SetActive(setActive);
    }

    public void OnMouseDown()
    {
        camCtr.OptionsCameraPlace();
        if (lightsON) { lightOptions.SetActive(true); }
        lightMustBeON = true;
    }

    private void OnMouseEnter()
    {
        optionsText.DOColor(mouseOnColor, changeDuration);
        if (lightsON) { lightOptions.SetActive(true); }
        
        laptopON.SetActive(true);
        laptopOFF.SetActive(false);
    }

    private void OnMouseExit()
    {
        optionsText.DOColor(basicColor, changeDuration);
        if (!lightMustBeON) { lightOptions.SetActive(false); }

        laptopON.SetActive(false);
        laptopOFF.SetActive(true);
    }

    public void LightOff() { lightMustBeON = false; }
}
