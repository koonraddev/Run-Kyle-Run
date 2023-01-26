using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsTrigger : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Camera cam;
    private CameraStartMenu camCtr;
    public GameObject optionsMenuObject;
    private OptionsMainMenu optionsMenu;
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

    private Button optionsButton; 
   
    void Start()
    {
        camCtr = cam.GetComponent<CameraStartMenu>();
        optionsText = optionsTextObject.GetComponent<TMP_Text>();
        optionsText.color = basicColor;
        lightMustBeON = false;

        optionsButton = gameObject.GetComponent<Button>();
    }

    void Update()
    {
        var dayTime = PlayerPrefs.GetInt("dayTime");
        var lights = PlayerPrefs.GetInt("lightsON");
        if ((dayTime == 3 || dayTime == 4) && lights == 1) { lightsON = true; }
    }

    /*
    public void OnMouseDown()
    {
        ClickOptions();
    }

    private void OnMouseEnter()
    {
        SelectOptions();
    }

    private void OnMouseExit()
    {
        DeselectOptions();
    }
    */

    IEnumerator OptionsClicked()
    {
        camCtr.OptionsCameraPlace();

        while (cam.transform.position != optionsCameraPlace.position)
        {
            yield return null;
        }

        optionsMenuObject.SetActive(true);
        optionsMenu = optionsMenuObject.GetComponent<OptionsMainMenu>();
        optionsMenu.SetButtonActive();
    }

    public void ClickOptions()
    {
        StartCoroutine(OptionsClicked());
        if (lightsON) { lightOptions.SetActive(true); }
        lightMustBeON = true;
    }

    public void SelectOptions()
    {
        optionsText.DOColor(mouseOnColor, changeDuration);
        if (lightsON) { lightOptions.SetActive(true); }

        laptopON.SetActive(true);
        laptopOFF.SetActive(false);
    }

    public void DeselectOptions()
    {
        optionsText.DOColor(basicColor, changeDuration);
        if (!lightMustBeON) { lightOptions.SetActive(false); }

        laptopON.SetActive(false);
        laptopOFF.SetActive(true);
    }

    public void LightOff() { lightMustBeON = false; }

    public void OnSelect(BaseEventData eventData)
    {
        SelectOptions();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        DeselectOptions();
    }

    public void SetButtonActive()
    {
        optionsButton.Select();
    }
}
