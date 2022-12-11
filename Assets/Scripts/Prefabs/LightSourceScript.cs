using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceScript : MonoBehaviour
{
    private int lightsON;
    public float lightIntensity;
    void Update()
    {
        lightsON = PlayerPrefs.GetInt("lightsON");
        if (lightsON == 1)
        {
            gameObject.GetComponent<Light>().intensity = lightIntensity;
        }
        else
        {
            gameObject.GetComponent<Light>().intensity = 0;
        }
    }
}
