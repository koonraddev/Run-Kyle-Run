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
        if (vecX > 0.9) { playerHP.TakeDamage(damage); playerMov.MoveLeft(); }
        if (vecX < -0.9) { playerHP.TakeDamage(damage); playerMov.MoveRight(); }

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
    }
}
