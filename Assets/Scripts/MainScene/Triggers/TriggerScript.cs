using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    private const bool V = true;
    public List<GameObject> industryPrefabsList;
    public List<GameObject> poolPrefabsList;
    public List<GameObject> currentPrefabsList;

    private int newPrefabNumber;
    private int lastPrefabNumber;

    public int biome;

    private GameSettings gameSets;
    private GameController gameCtrl;

    private float speed;
    private float baseSpeed;
    private float runSpeed;

    private float addSpeed;
    private bool runON;


    void Start()
    {
        SetRunStatus(false);
        gameSets = FindObjectOfType<GameSettings>();
        gameCtrl = FindObjectOfType<GameController>();

        baseSpeed = gameSets.GetBaseSpeed();
        runSpeed = gameSets.GetRunSpeed();
        currentPrefabsList = CheckBiomeIndex();
    }

    private void Update()
    {
        var isPower = gameCtrl.GetPowerStatus();
        if (runON)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (isPower) { RunSpeed(); }
                else { BaseSpeed(); }
            }
            else { BaseSpeed(); }
        }
        else { SetSpeedToZero(); }
    }


    public float GetGroundSpeed() { return speed; }
    public void RunSpeed()
    {      
        if (speed < runSpeed)
        {
            addSpeed += Time.deltaTime / 5;
            speed += addSpeed;
        }
        else
        {
            speed = runSpeed;
            addSpeed = 0;
        }
    }



    public void SetSpeedToZero() { speed = 0; }

    public void SetRunStatus(bool status) { runON = status; }

    public void BaseSpeed() { speed = baseSpeed; addSpeed = 0;}

    private void OnTriggerExit(Collider exitInfo)
    {
        currentPrefabsList = CheckBiomeIndex();
        Ground_Script ground = exitInfo.GetComponent<Ground_Script>();
        if (ground != null)
        {
            try
            {
                string prefabName = exitInfo.gameObject.name;
                string endingName = prefabName.Substring(prefabName.Length - 7, 7);
                if (prefabName.Length > 7)
                {
                    if (endingName == "(Clone)") { lastPrefabNumber = System.Int32.Parse(prefabName.Substring(prefabName.Length - 8, 1)); }
                    else { lastPrefabNumber = System.Int32.Parse(prefabName.Substring(prefabName.Length - 1, 1)); }
                }
                newPrefabNumber = RandomIntExcept(lastPrefabNumber);
            }
            catch (System.FormatException)
            {
                lastPrefabNumber = 0;
                newPrefabNumber = 0;
            }
            finally
            {
                RenderGround(newPrefabNumber);
            }
        }
    }

    private List<GameObject> CheckBiomeIndex()
    {
        return biome switch
        {
            1 => industryPrefabsList,
            2 => poolPrefabsList,
            _ => industryPrefabsList,
        };
    }

    public int RandomIntExcept(int except)
    {
        int result = Random.Range(0, currentPrefabsList.Count - 1);
        if (result >= except) result += 1;

        return result;
    }

    private void RenderGround(int prefabNumber)
    {
        Vector3 pos1 = gameObject.transform.position + (new Vector3(0f, 0f, (currentPrefabsList[prefabNumber].transform.localScale.z - gameObject.transform.localScale.z - 1f)));
        Instantiate(currentPrefabsList[prefabNumber], pos1, Quaternion.identity);
    }
}
