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
    public Vector2 barPanelON;
    public Vector2 barPanelOFF;

    public Vector2 distPanelON;
    public Vector2 distPanelOFF;

    public Vector2 biomeON;
    public Vector2 biomeOFF;

    public Vector2 hintsON;
    public Vector2 hintsOFF;

    public Vector2 blackStripeON;
    public Vector2 blackStripeOFF;
 
    //[Header("HPSiderPositionX")]
    private float barPanelONPosX;
    private float barPanelOFFPosX;

    //[Header("HPSiderPositionY")]
    private float barPanelONPosY;
    private float barPanelOFFPosY;
    
    //[Header("DistancePositionX")]
    private float distPanelONPosX;
    private float distPanelOFFPosX;

    //[Header("DistancePanelPositionY")]
    private float distPanelONPosY;
    private float distPanelOFFPosY;

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

    public GameObject barPanel;
    public GameObject HPBar;
    private Slider HPSlider;
    public GameObject HPBarBackground;
    private Slider HPSliderBackground;

    public GameObject powerBar;
    private Slider powerSlider;
    public GameObject powerBarBackground;
    private Slider powerSliderBackground;

    public GameObject distancePanel;
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
        distanceText.text = distTr.distanceScaled.ToString() + "m";
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
        HideRunUI();
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

        seqHide.OnComplete( ()=> {
            seqShow.Kill();
            seqHide.Kill();
            ShowRunUI();
        });
        
    }

    public void ShowRunUI()
    {
        barPanel.transform.DOLocalMove(new Vector2(barPanelONPosX, barPanelONPosY), 0.5f).SetEase(Ease.Linear);
        distancePanel.transform.DOLocalMove(new Vector2(distPanelONPosX, distPanelONPosY), 0.5f).SetEase(Ease.Linear);
        if (showHints == 1)
        {
            hintsObject.transform.DOLocalMove(new Vector2(hintsONPosX, hintsONPosY), 0.5f).SetEase(Ease.Linear);
        }
    }

    public void HideRunUI()
    {
        barPanel.transform.DOLocalMove(new Vector2(barPanelOFFPosX, barPanelOFFPosY), 0.5f).SetEase(Ease.Linear);
        distancePanel.transform.DOLocalMove(new Vector2(distPanelOFFPosX, distPanelOFFPosY), 0.5f).SetEase(Ease.Linear);
        if (showHints == 1)
        {
            hintsObject.transform.DOLocalMove(new Vector2(hintsOFFPosX, hintsOFFPosY), 0.5f).SetEase(Ease.Linear);
        }
    }

    public void SetStartPositions()
    {
        stripeUPObject.transform.localPosition = new Vector2(stripesOFFPosX, stripesOFFPosY);
        stripeDownObject.transform.localPosition = new Vector2(-stripesOFFPosX, -stripesOFFPosY);
        biomeObject.transform.localPosition = new Vector2(biomeOFFPosX, biomeOFFPosY);
        barPanel.transform.localPosition = new Vector2(barPanelOFFPosX, barPanelOFFPosY);
        distancePanel.transform.localPosition = new Vector2(distPanelOFFPosX, distPanelOFFPosY);
    }
    private void SetCords()
    {
        barPanelONPosX = barPanelON.x;
        barPanelONPosY = barPanelON.y;

        barPanelOFFPosX = barPanelOFF.x;
        barPanelOFFPosY = barPanelOFF.y;

        distPanelONPosX = distPanelON.x;
        distPanelONPosY = distPanelON.y;

        distPanelOFFPosX = distPanelOFF.x;
        distPanelOFFPosY = distPanelOFF.y;

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
        //ShowRunUI();
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