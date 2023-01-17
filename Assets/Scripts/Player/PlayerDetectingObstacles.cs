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
    private void Start()
    {
        playerMov = gameObject.GetComponent<PlayerMovement>();
        playerHP = playerObject.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        var damage = gameSets.GetDamge();
        if (Physics.Raycast(transform.position + new Vector3(0, 0.8f, 0), Vector3.forward, out RaycastHit hitInfo,1f))
        {
            if (hitInfo.collider.CompareTag("Obstacle"))
            {
                playerHP.TakeDamage(damage);
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
        if (vecX > 0.9) { playerHP.TakeDamage(damage); playerMov.MoveLeft(); }
        if (vecX < -0.9) { playerHP.TakeDamage(damage); playerMov.MoveRight(); }
    }
}
