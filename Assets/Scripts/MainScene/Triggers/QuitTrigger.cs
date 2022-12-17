using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class QuitTrigger : MonoBehaviour
{
    public GameObject cam;
    private CameraStartMenu camCtr;
    public GameObject quitMenu;
    public Transform quitCameraPlace;


    public GameObject playTextObject;
    private TMP_Text quitText;

    public GameObject lightQuit;
    public bool lightMustBeON;
    private bool lightsON;

    [Header("Colors")]
    public Color basicColor;
    public Color mouseOnColor;
    public float changeDuration;
    void Start()
    {
        camCtr = cam.GetComponent<CameraStartMenu>();
        quitText = playTextObject.GetComponent<TMP_Text>();
        quitText.color = basicColor;
        lightMustBeON = false;
    }

    void Update()
    {
        var dayTime = PlayerPrefs.GetInt("dayTime");
        var lights = PlayerPrefs.GetInt("lightsON");
        if ((dayTime == 3 || dayTime == 4) && lights == 1) { lightsON = true; }

        bool setActive = cam.transform.position == quitCameraPlace.position;
        quitMenu.SetActive(setActive);
    }

    public void OnMouseDown()
    {
        camCtr.QuitCameraPlace();
        if (lightsON) { lightQuit.SetActive(true); }
        lightMustBeON = true;
    }

    private void OnMouseEnter()
    {
        quitText.DOColor(mouseOnColor, changeDuration);
        if (lightsON) { lightQuit.SetActive(true); }
    }

    private void OnMouseExit()
    {
        quitText.DOColor(basicColor, changeDuration);
        if (!lightMustBeON) { lightQuit.SetActive(false); }
    }

    public void LightOff() { lightMustBeON = false; }
}
