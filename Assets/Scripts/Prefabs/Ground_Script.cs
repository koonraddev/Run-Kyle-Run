using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Script : MonoBehaviour
{
    private TriggerScript gameTrigger;
    public bool freeze;

    private float speed;
    private void Start()
    {
        gameTrigger = FindObjectOfType<TriggerScript>();
        speed = gameTrigger.GetGroundSpeed();
    }
    void Update()
    {
        speed = gameTrigger.GetGroundSpeed();
        transform.position += new Vector3(0f, 0f, -1f) * Time.deltaTime * speed;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void FreezeObject()
    {
        speed = 0;
    }
    public void MoveObject()
    {
        speed = gameTrigger.GetGroundSpeed();
    }

}
