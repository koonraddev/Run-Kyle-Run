using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectingObstacles : MonoBehaviour
{
    public GameObject playerObject;
    private PlayerHealth playerHP;

    public GameSettings gameSets;

    public float zpow;
    private PlayerMovement playerMov;
    private void Start()
    {
        playerMov = gameObject.GetComponent<PlayerMovement>();
        playerHP = playerObject.GetComponent<PlayerHealth>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var damage = gameSets.GetDamge();
        float vecX = hit.moveDirection.x;
        //float vecY = hit.moveDirection.y;
        //float vecZ = hit.moveDirection.z;
        if (vecX > 0.9)
        {
            playerMov.MoveLeft();
            playerHP.TakeDamage(damage);
        }
        if (vecX < -0.9)
        {
            playerMov.MoveRight();
            playerHP.TakeDamage(damage);
        }

        if (hit.collider.CompareTag("Obstacle"))
        {
            if(hit.moveDirection.z > 0.3)
            {
                Destroy(hit.collider.gameObject);
                playerHP.TakeDamage(damage);
            }
        }
        
        if (hit.collider.CompareTag("StrongObstacle"))
        {
            if (hit.moveDirection.z > 0.3)
            {
                playerHP.TakeDamage(9999);
            }
        }  
    }
}
