using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class PlayTrigger : MonoBehaviour
{
    public GameObject playerObject;
    private Animator animator;

    public GameObject cam;
    private CameraStartMenu camCtr;

    public GameObject leftRenderingTrigger;
    private TriggerScript leftTrigger;

    public GameObject rightRenderingTrigger;
    private TriggerScript rightTrigger;

    public GameObject mainRenderingTrigger;
    private TriggerScript mainTrigger;

    public GameObject startDoorsTriggerObj;
    private StartDoorsTrigger startDTrigger;


    public GameObject playTextObject;
    private TMP_Text playText;

    public GameObject lightPlay;
    public bool lightMustBeON;
    private bool lightsON;

    [Header("Colors")]
    public Color basicColor;
    public Color mouseOnColor;
    public float changeDuration;

    void Start()
    {
        animator = playerObject.GetComponent<Animator>();
        camCtr = cam.GetComponent<CameraStartMenu>();

        leftTrigger = leftRenderingTrigger.GetComponent<TriggerScript>();
        rightTrigger = rightRenderingTrigger.GetComponent<TriggerScript>();
        mainTrigger = mainRenderingTrigger.GetComponent<TriggerScript>();

        playText = playTextObject.GetComponent<TMP_Text>();
        playText.color = basicColor;

        startDTrigger = startDoorsTriggerObj.GetComponent<StartDoorsTrigger>();
        lightMustBeON = false;
        Invoke(nameof(PlayAgainCheck), 1f);
    }

    void Update()
    {
        var dayTime = PlayerPrefs.GetInt("dayTime");
        var lights = PlayerPrefs.GetInt("lightsON");
        if ((dayTime == 3 || dayTime == 4) && lights == 1) { lightsON = true; }         
    }

    public void PlayAgainCheck()
    {
        if (PlayerPrefs.GetInt("playAgain") == 1)
        {
            OnMouseDown();
            PlayerPrefs.SetInt("playAgain", 0);
        }
    }

    public void OnMouseDown()
    {
        PlayerPrefs.SetInt("runON", 1);
        camCtr.PlayCameraPlace();
        leftTrigger.SetRunStatus(true);
        mainTrigger.SetRunStatus(true);
        rightTrigger.SetRunStatus(true);
        //Ground_Script[] groundObjects = FindObjectsOfType<Ground_Script>();
        lightMustBeON = true;
        int difficulty = PlayerPrefs.GetInt("difficulty");

        switch (difficulty)
        {
            case 0:
                animator.speed = 1f;
                break;
            case 1:
                animator.speed = 1.5f;
                break;
            case 2:
                animator.speed = 2f;
                break;
        }
        //foreach (Ground_Script obj in groundObjects)
        //{
        //    obj.MoveObject();
        //}
    }

    private void OnMouseEnter()
    {
        playText.DOColor(mouseOnColor, changeDuration);
        startDTrigger.ChangeDoorsStatus();
        if (lightsON) lightPlay.SetActive(true);
    }

    private void OnMouseExit()
    {
        playText.DOColor(basicColor, changeDuration);
        startDTrigger.ChangeDoorsStatus();
        if (!lightMustBeON) lightPlay.SetActive(false);
    }
}
