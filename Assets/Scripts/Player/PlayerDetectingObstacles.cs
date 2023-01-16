using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectingObstacles : MonoBehaviour
{
    public GameObject playerObject;
    private PlayerHealth playerHP;

    public GameSettings gameSets;
    public float counter = 0f;
    public float zpow;
    private PlayerMovement playerMov;
    private LineRenderer lineR;
    private GameObject lineBox;

    private void Start()
    {
        lineBox = new GameObject();
        lineBox.AddComponent<LineRenderer>();
        lineR = lineBox.GetComponent<LineRenderer>();
        lineR.endWidth = 0.2f;
        lineR.startWidth = 0.2f;
        lineBox.transform.position = gameObject.transform.position + new Vector3(0,2f,0);
        lineR.positionCount = 2;

        playerMov = gameObject.GetComponent<PlayerMovement>();
        playerHP = playerObject.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        var damage = gameSets.GetDamge();
        if (Physics.Raycast(transform.position + new Vector3(0, 2f, 0), Vector3.forward, out RaycastHit hitInfo))
        {
            //Debug.Log(hitInfo.collider.name);

            //lineR.SetPosition(0, lineBox.transform.position);
            //lineR.SetPosition(1, hitInfo.collider.transform.position);

            if (hitInfo.distance < 1f && hitInfo.collider.CompareTag("Obstacle"))
            {
                playerHP.TakeDamage(damage);
                Destroy(hitInfo.collider.gameObject);
            }

            if (hitInfo.distance < 1f && hitInfo.collider.CompareTag("StrongObstacle"))
            {
                playerHP.TakeDamage(9999);
            }
        }

        
    }

    public void Draw()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var damage = gameSets.GetDamge();
        float vecX = hit.moveDirection.x;
        //float vecY = hit.moveDirection.y;
        //float vecZ = hit.moveDirection.z;
        //Debug.Log("cotnr -> " + hit.gameObject.name);
        //Debug.Log("cotnr -> " + hit.gameObject.tag);

        if (vecX > 0.9) { playerHP.TakeDamage(damage); playerMov.MoveLeft(); }
        if (vecX < -0.9) { playerHP.TakeDamage(damage); playerMov.MoveRight(); }


        /*
        if (hit.moveDirection.z > 0.3)
        {
            if (hit.collider.CompareTag("Obstacle")) 
            { 
                playerHP.TakeDamage(damage); 
                Destroy(hit.collider.gameObject); 
            }
            if (hit.collider.CompareTag("StrongObstacle"))
            {
                playerHP.TakeDamage(9999);
            }
        } 
        */

    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger -> " + other.gameObject.name);
        //Debug.Log("trigger -> " + other.gameObject.tag);
        var damage = gameSets.GetDamge();
        /*
        if (other.CompareTag("CUBE"))
        {
            playerHP.TakeDamage(damage);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Obstacle"))
        {
            playerHP.TakeDamage(9999);
        }
        */
    }
    
}
