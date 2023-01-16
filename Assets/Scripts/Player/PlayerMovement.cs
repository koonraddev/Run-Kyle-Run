using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public CharacterController ctr;

    private readonly Transform[] lanes = new Transform[5];
    private int activeLane;
    public Transform lane0;
    public Transform lane1;
    public Transform lane2;
    public Transform lane3;
    public Transform lane4;

    public bool backCamera;

    private float movementSpeed;
    public float movementSpeedOnGround;
    public float movementSpeedInAir;
    public float jumpForce;
    public float gravity;
    public float verticalVelocity;
    private Vector3 newPosition = Vector3.zero;
    private Vector3 moveVector = Vector3.zero;
    public bool runON;

    public Camera mainCamera;
    private CameraMovement camMov;

    void Start()
    {
        activeLane = 2;
        lanes[0] = lane0;
        lanes[1] = lane1;
        lanes[2] = lane2;
        lanes[3] = lane3;
        lanes[4] = lane4;
        runON = false;
        camMov = mainCamera.GetComponent<CameraMovement>();
        ctr = gameObject.GetComponent<CharacterController>();
        backCamera = camMov.backCamera;
    }

    void Update()
    {
        moveVector = Vector3.zero;
        backCamera = camMov.backCamera;
        if (runON)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                camMov.MultiplierActiveLane();
                if (backCamera) { MoveRight(); } else { MoveLeft(); }       
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                camMov.MultiplierActiveLane();
                if (backCamera) { MoveLeft(); } else { MoveRight(); }           
            }
            activeLane = Mathf.Clamp(activeLane, 0, 4);
        }

        if (ctr.isGrounded)
        {
            movementSpeed = movementSpeedOnGround;
            verticalVelocity = -0.1f;
            if (Input.GetKeyDown(KeyCode.UpArrow) && runON) verticalVelocity = jumpForce;
        }
        else
        {
            movementSpeed = movementSpeedInAir;
            verticalVelocity -= (gravity * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.DownArrow)) verticalVelocity = -jumpForce;
        }       
        newPosition.x = lanes[activeLane].transform.position.x;
        newPosition.z = lanes[activeLane].transform.position.z;

        moveVector.x = (newPosition - transform.position).normalized.x * movementSpeed;
        moveVector.y = verticalVelocity;

        moveVector.z = transform.position.z != lanes[activeLane].transform.position.z ? (newPosition - transform.position).normalized.z * movementSpeed : 0f;

        ctr.Move(moveVector * Time.deltaTime); 
    }

    public int GetActiveLane() { return activeLane; }

    public Vector3 GetActiveLanePosition() { return lanes[activeLane].transform.position; }

    public float GetMovementSpeed() { return movementSpeed; }
    public void RunChangeStatus() { runON = !runON; }

    public void MoveLeft() { if (runON) activeLane -= 1; }

    public void MoveRight() { if (runON) activeLane += 1; }
}