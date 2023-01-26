using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitTrigger : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject cam;
    private CameraStartMenu camCtr;
    public GameObject quitMenuObject;
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

    private Button quitButton;

    private QuitMenu quitMenu;
    void Start()
    {
        quitButton = gameObject.GetComponent<Button>();
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
    }

    /*
    public void OnMouseDown()
    {
        ClickQuit();
    }

    private void OnMouseEnter()
    {
        SelectQuit();
    }

    private void OnMouseExit()
    {
        DeselectQuit();
    }
    */

    IEnumerator QuitClicked()
    {
        camCtr.QuitCameraPlace();

        while (cam.transform.position != quitCameraPlace.position)
        {
            yield return null;
        }

        quitMenuObject.SetActive(true);
        quitMenu = quitMenuObject.GetComponent<QuitMenu>();
        quitMenu.SetButtonActive();
    }

    public void ClickQuit()
    {
        StartCoroutine(QuitClicked());
        if (lightsON) { lightQuit.SetActive(true); }
        lightMustBeON = true;
    }

    public void SelectQuit()
    {
        quitText.DOColor(mouseOnColor, changeDuration);
        if (lightsON) { lightQuit.SetActive(true); }
    }

    public void DeselectQuit()
    {
        quitText.DOColor(basicColor, changeDuration);
        if (!lightMustBeON) { lightQuit.SetActive(false); }
    }

    public void LightOff() { lightMustBeON = false; }

    public void OnSelect(BaseEventData eventData)
    {
        SelectQuit();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        DeselectQuit();
    }

    public void SetButtonActive()
    {
        quitButton.Select();
    }
}
