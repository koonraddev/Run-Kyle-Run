using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerModeOneTrigger : MonoBehaviour
{
    public GameObject mainCamera;
    private CameraMovement camMov;

    public Transform BackCameraPlace;
    public float speed;
    void Start()
    {
        camMov = mainCamera.GetComponent<CameraMovement>();
        speed = 10;
    }

    void Update()
    {
        if (camMov.cameraMode == 1 && camMov.GetRunStatus())
        {
            camMov.BackCameraStartPlace = new Vector3(0, 8, -33);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCharacter"))
        {
            camMov.cameraMode = 1;
        }
    }
}
