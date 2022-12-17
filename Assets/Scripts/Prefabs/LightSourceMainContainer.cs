using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceMainContainer : MonoBehaviour
{
    public float totalSeconds;     // seconds the flash wil last
    public float maxIntensity;
    private Light myLight;

    private bool changeColor;

    private int colorIndex;
    public Color[] colorList;

    void Start()
    {
        myLight = gameObject.GetComponent<Light>();
        colorIndex = 0;
        changeColor = false;
        myLight.color = colorList[colorIndex];

    }
    void Update()
    {
        if (myLight.intensity == 0 && changeColor == true)
        {
            colorIndex = colorIndex == colorList.Length - 1 ? 0 : +1;

            changeColor = false;
        }
        myLight.color = colorList[colorIndex];

        if (myLight.intensity == 0 || myLight.intensity == maxIntensity)
        {
            StopCoroutine(FlashNow());
            StartCoroutine(FlashNow());
        }
    }

    public IEnumerator FlashNow()
    {
        changeColor = true;
        float waitTime = totalSeconds / 2;
        while (myLight.intensity < maxIntensity)
        {
            myLight.intensity += Time.deltaTime / waitTime;        // Increase intensity
            yield return null;
        }
        while (myLight.intensity > 0)
        {
            myLight.intensity -= Time.deltaTime / waitTime;         //Decrease intensity
            yield return null;
        }

        yield return null;
    }

    public int RandomIntExcept(int except)
    {
        int result = Random.Range(0, colorList.Length - 1);
        if (result >= except) result += 1;
        return result;
    }
}
