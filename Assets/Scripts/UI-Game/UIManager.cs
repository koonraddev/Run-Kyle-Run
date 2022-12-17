using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float speed;

    public bool runON;
    public bool pauseON;

    public GameObject distanceTriggerObject;
    private DistanceTrigger distTr;
    public Vector3 hpON;
    public Vector3 hpOFF;

    public Vector3 powerON;
    public Vector3 powerOFF;

    public Vector3 distON;
    public Vector3 distOFF;

    public Vector3 biomeON;
    public Vector3 biomeOFF;

    public Vector3 hintsON;
    public Vector3 hintsOFF;

    public Vector3 blackStripeON;
    public Vector3 blackStripeOFF;
 
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
    private RectTransform sliderTransform;
    private Vector2 sliderPos;
    private Slider HPSlider;

    public GameObject distance;
    private RectTransform distanceTransform;
    private Vector2 distancePos;
    private TMP_Text distanceText;

    public GameObject powerBar;
    private RectTransform powerSliderTransform;
    private Vector2 powerSliderPos;
    private Slider powerSlider;

    public GameObject biomeObject;
    private RectTransform biomeTransform;
    private Vector2 biomePos;
    private TMP_Text biomeText;
    public bool showBiome;

    public GameObject playerObject;
    private PlayerHealth playerHP;

  
    public GameObject gameControllerObject;
    private GameController gameCtr;

    public GameObject hintsObject;
    private RectTransform hintsTransform;
    private Vector2 hintsPos;
    public int showHints;

    public GameObject stripeUPObject;
    private RectTransform stripeUPTransform;
    private Vector2 stripeUPPos;

    public GameObject stripeDownObject;
    private RectTransform stripeDownTransform;
    private Vector2 stripeDownPos;
    public bool showStripes;
    void Start()
    {
        SetCords();

        runON = false;
        pauseON = false;
        sliderTransform = HPBar.GetComponent<RectTransform>();
        sliderPos = sliderTransform.anchoredPosition;
        
        powerSliderTransform = powerBar.GetComponent<RectTransform>();
        powerSliderPos = powerSliderTransform.anchoredPosition;
        powerSlider = powerBar.GetComponent<Slider>();

        distanceTransform = distance.GetComponent<RectTransform>();
        distancePos = distanceTransform.anchoredPosition;

        distTr = distanceTriggerObject.GetComponent<DistanceTrigger>();
        distanceText = distance.GetComponent<TMP_Text>();

        playerHP = playerObject.GetComponent<PlayerHealth>();
        HPSlider = HPBar.GetComponent<Slider>();

        gameCtr = gameControllerObject.GetComponent<GameController>();

        biomeText = biomeObject.GetComponent<TMP_Text>();
        biomeTransform = biomeObject.GetComponent<RectTransform>();
        biomePos = biomeTransform.anchoredPosition;
        showBiome = false;

        showHints = PlayerPrefs.GetInt("hintsON");
        hintsTransform = hintsObject.GetComponent<RectTransform>();
        hintsPos = hintsTransform.anchoredPosition;
        
        stripeUPTransform = stripeUPObject.GetComponent<RectTransform>();
        stripeUPPos = stripeUPTransform.anchoredPosition;
        
        stripeDownTransform = stripeDownObject.GetComponent<RectTransform>();
        stripeDownPos = stripeDownTransform.anchoredPosition;
        showStripes = false;

        HPSlider.maxValue = playerHP.GetCurrentHP();
        powerSlider.maxValue = gameCtr.GetPowerValue();

    }

    void Update()
    {
        var step = speed * Time.deltaTime;
        distanceText.text = "Distance " + distTr.distanceScaled.ToString() + "m";
        HPSlider.value = playerHP.GetCurrentHP();
        powerSlider.value = gameCtr.GetPowerValue();

        //HP Bar; Power Bar; Distance
        if (runON && !pauseON) // SHOW HP Bar; Power Bar; Distance
        {      
            HPBar.transform.localPosition = Vector3.MoveTowards(HPBar.transform.localPosition, new Vector3(sliderPos.x + hpONPosX, sliderPos.y + hpONPosY), step * 100);
            powerBar.transform.localPosition = Vector3.MoveTowards(powerBar.transform.localPosition, new Vector3(powerSliderPos.x + powerONPosX, powerSliderPos.y + powerONPosY), step * 100);
            distance.transform.localPosition = Vector3.MoveTowards(distance.transform.localPosition, new Vector3(distancePos.x + distONPosX, distancePos.y + distONPosY), step * 100);
            
            //Hints
            if (showHints == 1) hintsObject.transform.localPosition = Vector3.MoveTowards(hintsObject.transform.localPosition, new Vector3(hintsPos.x + hintsONPosX, hintsPos.y + hintsONPosY), step * 100);
            else hintsObject.transform.localPosition = Vector3.MoveTowards(hintsObject.transform.localPosition, new Vector3(hintsPos.x + hintsOFFPosX, hintsPos.y + hintsOFFPosY), step * 100);
        }
        else
        {
            // HIDE HP Bar; Power Bar; Distance
            HPBar.transform.localPosition = Vector3.MoveTowards(HPBar.transform.localPosition, new Vector3(sliderPos.x + hpOFFPosX, sliderPos.y + hpOFFPosY), step * 100);
            powerBar.transform.localPosition = Vector3.MoveTowards(powerBar.transform.localPosition, new Vector3(powerSliderPos.x + powerOFFPosX, powerSliderPos.y + powerOFFPosY), step * 100);
            distance.transform.localPosition = Vector3.MoveTowards(distance.transform.localPosition, new Vector3(distancePos.x + distOFFPosX, distancePos.y + distOFFPosY), step * 100);
        }

        //Biome
        if (showBiome) //SHOW Biome
        {
            stripeUPObject.transform.localPosition = Vector3.MoveTowards(stripeUPObject.transform.localPosition, new Vector3(stripeUPPos.x + stripesONPosX, stripeUPPos.y + stripesONPosY), step * 30);
            stripeDownObject.transform.localPosition = Vector3.MoveTowards(stripeDownObject.transform.localPosition, new Vector3(stripeDownPos.x - stripesONPosX, stripeDownPos.y - stripesONPosY), step * 30);
            //Stripes
            if (showStripes) //Show Stripes
            {
                biomeObject.transform.localPosition = Vector3.MoveTowards(biomeObject.transform.localPosition, new Vector3(biomePos.x + biomeONPosX, biomePos.y + biomeONPosY), step * 30);
            }
            else //Hide Stripes
            {
                biomeObject.transform.localPosition = Vector3.MoveTowards(biomeObject.transform.localPosition, new Vector3(biomePos.x + biomeOFFPosX, biomePos.y + biomeOFFPosY), step * 30);
                Invoke(nameof(BlackStripesHide), 4f);

            }
            Invoke(nameof(BiomeTextHide), 5f);
        }
        else //HIDE Biome
        {
            stripeUPObject.transform.localPosition = Vector3.MoveTowards(stripeUPObject.transform.localPosition, new Vector3(stripeUPPos.x + stripesOFFPosX, stripeUPPos.y + stripesOFFPosY), step * 30);
            stripeDownObject.transform.localPosition = Vector3.MoveTowards(stripeDownObject.transform.localPosition, new Vector3(stripeDownPos.x - stripesOFFPosX, stripeDownPos.y - stripesOFFPosY), step * 30);
            biomeObject.transform.localPosition = Vector3.MoveTowards(biomeObject.transform.localPosition, new Vector3(biomePos.x + biomeOFFPosX, biomePos.y + biomeOFFPosY), step * 30);
        }

        if (playerHP.dead)
        {
            gameCtr.gameOver = true;
            gameCtr.distanceTraveled = distTr.distanceScaled;
        }
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

    public void RunChangeStatus() { runON = !runON; }

    public void ChangeBiomeText() { biomeText.text = "Biome: " + PlayerPrefs.GetString("currentBiome"); }

    public void BiomeTextShowUP() { showBiome = true; Invoke(nameof(BlackStripesShow), 1.5f); }

    public void BiomeTextHide() { showBiome = false; }

    public void BlackStripesShow() { showStripes = true; }

    public void BlackStripesHide() { showStripes = false; }

    public void ChangeHintsStatus() { showHints = PlayerPrefs.GetInt("hintsON"); }
}