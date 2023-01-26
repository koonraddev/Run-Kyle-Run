using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectingObstacles : MonoBehaviour
{
    public CameraMovement camMov;
    public GameSettings gameSets;
    public GameObject boomEffect;

    private PlayerHealth playerHP;
    private PlayerMovement playerMov;
    private void Start()
    {
        playerMov = gameObject.GetComponent<PlayerMovement>();
        playerHP = gameObject.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        FrontCollisionDetection();      
    }

    private void FrontCollisionDetection()
    {
        var damage = gameSets.GetDamge();
        if (Physics.Raycast(transform.position + new Vector3(0, 0.8f, 0), Vector3.forward, out RaycastHit hitInfo, 0.65f))
        {
            if (hitInfo.collider.CompareTag("Obstacle"))
            {
                StartCoroutine(camMov.CameraShake()); ;
                playerHP.TakeDamage(damage);
                Instantiate(boomEffect, hitInfo.point, new Quaternion(0f, 0f, 0f, 0f));
                Destroy(hitInfo.collider.gameObject);
            }

            if (hitInfo.collider.CompareTag("StrongObstacle"))
            {
                playerHP.TakeDamage(9999);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var damage = gameSets.GetDamge();
        float vecX = hit.moveDirection.x;
        if (vecX > 0.9) 
        {
            playerMov.MoveLeft();
            StartCoroutine(camMov.CameraShake());
            playerHP.TakeDamage(damage); 
            Instantiate(boomEffect, hit.point, new Quaternion(0f, 0f ,0f ,0f)); 
        }
        if (vecX < -0.9) 
        {
            playerMov.MoveRight();
            StartCoroutine(camMov.CameraShake()); ;
            playerHP.TakeDamage(damage);
            Instantiate(boomEffect, hit.point, new Quaternion(0f, 0f, 0f, 0f)); 
        }
    }
}
