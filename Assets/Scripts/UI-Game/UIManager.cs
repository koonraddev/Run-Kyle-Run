using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public float speed;

    public bool runON;
    public bool pauseON;

    public GameObject distanceTriggerObject;
    private DistanceTrigger distTr;
    public Vector2 hpON;
    public Vector2 hpOFF;

    public Vector2 powerON;
    public Vector2 powerOFF;

    public Vector2 distON;
    public Vector2 distOFF;

    public Vector2 biomeON;
    public Vector2 biomeOFF;

    public Vector2 hintsON;
    public Vector2 hintsOFF;

    public Vector2 blackStripeON;
    public Vector2 blackStripeOFF;
 
    //[Header("HPSiderPositionX")]
    private float hpONPosX;
    private float hpOFFPosX;

    //[Header("HPSiderPositionY")]
    private float hpONPosY;
    private float hpOFFPosY;
    
    //[Header("powerSiderPositionX")]
    private float powerONPosX;
    private float powerOFFPosX;

    //[Header("powerSiderPositionY")]
    private float powerONPosY;
    private float powerOFFPosY;

    //[Header("DistancePositionX")]
    private float distONPosX;
    private float distOFFPosX;

    //[Header("DistancePositionY")]
    private float distONPosY;
    private float distOFFPosY;

    //[Header("BiomePositionX")]
    private float biomeONPosX;
    private float biomeOFFPosX;

    //[Header("BiomePositionY")]
    private float biomeONPosY;
    private float biomeOFFPosY;

    //[Header("HintsPositionX")]
    private float hintsONPosX;
    private float hintsOFFPosX;

    //[Header("HintsPositionY")]
    private float hintsONPosY;
    private float hintsOFFPosY;

    //[Header("BlackStripesPositionX")]
    private float stripesONPosX;
    private float stripesOFFPosX;

    //[Header("HintsPositionY")]
    private float stripesONPosY;
    private float stripesOFFPosY;

    public GameObject HPBar;
    private Slider HPSlider;
    public GameObject HPBarBackground;
    private Slider HPSliderBackground;

    public GameObject powerBar;
    private Slider powerSlider;
    public GameObject powerBarBackground;
    private Slider powerSliderBackground;

    public GameObject distance;
    private TMP_Text distanceText;

    public GameObject biomeObject;
    private TMP_Text biomeText;

    public GameObject playerObject;
    private PlayerHealth playerHP;

  
    public GameObject gameControllerObject;
    private GameController gameCtr;

    public GameObject hintsObject;
    public int showHints;

    public GameObject stripeUPObject;

    public GameObject stripeDownObject;
    public bool showStripes;

    void Start()
    {
        SetCords();

        runON = false;
        pauseON = false;

        distTr = distanceTriggerObject.GetComponent<DistanceTrigger>();
        distanceText = distance.GetComponent<TMP_Text>();

        playerHP = playerObject.GetComponent<PlayerHealth>();

        HPSlider = HPBar.GetComponent<Slider>();
        HPSliderBackground = HPBarBackground.GetComponent<Slider>();

        powerSlider = powerBar.GetComponent<Slider>();
        powerSliderBackground = powerBarBackground.GetComponent<Slider>();

        gameCtr = gameControllerObject.GetComponent<GameController>();

        biomeText = biomeObject.GetComponent<TMP_Text>();

        showHints = PlayerPrefs.GetInt("hintsON");

        HPSlider.maxValue = gameCtr.GetMaxHealthValue();
        HPSliderBackground.value = gameCtr.GetMaxHealthValue();
        powerSlider.maxValue = gameCtr.GetMaxPowerValue();
        powerSliderBackground.maxValue = gameCtr.GetMaxPowerValue();


        SetStartPositions();

    }

    void Update()
    {
        var step = speed * Time.deltaTime;
        distanceText.text = "Distance " + distTr.distanceScaled.ToString() + "m";
        HPSlider.value = playerHP.GetCurrentHP();
        powerSlider.value = gameCtr.GetPowerValue();

        if (playerHP.dead)
        {
            gameCtr.gameOver = true;
            gameCtr.distanceTraveled = distTr.distanceScaled;
        }
    }


    public IEnumerator ShowBiome()
    {
        Sequence seqShow = DOTween.Sequence()
            .Append(stripeUPObject.transform.DOLocalMove(new Vector2(stripesONPosX, stripesONPosY), 0.5f).SetEase(Ease.Linear))
            .Join(stripeDownObject.transform.DOLocalMove(new Vector2(-stripesONPosX, -stripesONPosY), 0.5f).SetEase(Ease.Linear))
            .Append(biomeObject.transform.DOLocalMove(new Vector2(biomeONPosX, biomeONPosY), 0.5f).SetDelay(1f))
            .AppendInterval(2f);


        yield return seqShow.WaitForCompletion();

        Sequence seqHide = DOTween.Sequence()
            .Append(biomeObject.transform.DOLocalMove(new Vector2(biomeOFFPosX, biomeOFFPosY), 0.5f))
            .Append(stripeUPObject.transform.DOLocalMove(new Vector2(stripesOFFPosX, stripesOFFPosY), 0.5f).SetEase(Ease.Linear))
            .Join(stripeDownObject.transform.DOLocalMove(new Vector2(-stripesOFFPosX, -stripesOFFPosY), 0.5f).SetEase(Ease.Linear));

    }

    public void ShowRunUI()
    {
        HPBar.transform.DOLocalMove(new Vector2(hpONPosX, hpONPosY), 0.5f).SetEase(Ease.Linear);
        HPBarBackground.transform.DOLocalMove(new Vector2(hpONPosX, hpONPosY), 0.5f).SetEase(Ease.Linear);
        powerBar.transform.DOLocalMove(new Vector2(powerONPosX, powerONPosY), 0.5f).SetEase(Ease.Linear);
        powerBarBackground.transform.DOLocalMove(new Vector2(powerONPosX, powerONPosY), 0.5f).SetEase(Ease.Linear);
        distance.transform.DOLocalMove(new Vector2(distONPosX, distONPosY), 0.5f).SetEase(Ease.Linear);
        if (showHints == 1)
        {
            hintsObject.transform.DOLocalMove(new Vector2(hintsONPosX, hintsONPosY), 0.5f).SetEase(Ease.Linear);
        }
    }

    public void SetStartPositions()
    {
        stripeUPObject.transform.localPosition = new Vector2(stripesOFFPosX, stripesOFFPosY);
        stripeDownObject.transform.localPosition = new Vector2(-stripesOFFPosX, -stripesOFFPosY);
        biomeObject.transform.localPosition = new Vector2(biomeOFFPosX, biomeOFFPosY);
        HPBar.transform.localPosition = new Vector2(hpOFFPosX, hpOFFPosY);
        HPBarBackground.transform.localPosition = new Vector2(hpOFFPosX, hpOFFPosY);
        powerBar.transform.localPosition = new Vector2(powerOFFPosX, powerOFFPosY);
        powerBarBackground.transform.localPosition = new Vector2(powerOFFPosX, powerOFFPosY);
        distance.transform.localPosition = new Vector2(distOFFPosX, distOFFPosY);
    }
    private void SetCords()
    {
        hpONPosX = hpON.x;
        hpONPosY = hpON.y;

        hpOFFPosX = hpOFF.x;
        hpOFFPosY = hpOFF.y;

        powerONPosX = powerON.x;
        powerONPosY = powerON.y;

        powerOFFPosX = powerOFF.x;
        powerOFFPosY = powerOFF.y;

        distONPosX = distON.x;
        distONPosY = distON.y;

        distOFFPosX = distOFF.x;
        distOFFPosY = distOFF.y;

        biomeONPosX = biomeON.x;
        biomeONPosY = biomeON.y;

        biomeOFFPosX = biomeOFF.x;
        biomeOFFPosY = biomeOFF.y;

        hintsONPosX = hintsON.x;
        hintsONPosY = hintsON.y;

        hintsOFFPosX = hintsOFF.x;
        hintsOFFPosY = hintsOFF.y;

        stripesONPosX = blackStripeON.x;
        stripesONPosY = blackStripeON.y;

        stripesOFFPosX = blackStripeOFF.x;
        stripesOFFPosY = blackStripeOFF.y;
    }

    public void RunChangeStatus() 
    {
        runON = !runON;
        ShowRunUI();
    }

    public void ChangeHPBackground()
    {
        var HPBackgroundNewValue = HPSlider.value;
        if (HPSlider.value > HPSliderBackground.value)
        {
            HPSliderBackground.value = HPSlider.value;
        }
        else
        {
            HPSliderBackground.DOValue(HPBackgroundNewValue, 1f);
        }
    }

    public void ChangePowerBackground()
    {
        var powerBackgroundNewValue = powerSlider.value;
        if (powerSlider.value > powerSliderBackground.value)
        {
            powerSliderBackground.value = powerSlider.value;
        }
        else
        {
            powerSliderBackground.DOValue(powerBackgroundNewValue, 1f);
        }
    }

    public void ChangeBiomeText() { biomeText.text = "Biome: " + PlayerPrefs.GetString("currentBiome"); }

    public void ChangeHintsStatus() { showHints = PlayerPrefs.GetInt("hintsON"); }
}