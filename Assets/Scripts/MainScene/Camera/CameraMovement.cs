using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    public Camera mainCamera;
    public bool backCamera;

    public PlayerMovement playerMov;

    public Transform FrontCameraPlace;
    public Transform BackCameraPlace;

    public Vector3 BackCameraStartPlace;
    public float speed;
    public float multiplier;

    [Header("Floating Effect")]
    public float lineScaleX;
    public float lineScaleY;
    private float frequency;
    public float runFrequency;
    public float baseFrequency;

    [Header("FOV Settings")]
    public float ShiftingBackCamFov;
    public float NOShiftingBackCamFov;

    private float shiftingBackCamRatio;
    [Space(10)]
    public float ShiftingFrontCam;
    public float NOShiftingFrontCam;
    [Space(10)]

    private bool runON;
    Vector3 BackCameraPosition;
    Vector3 FrontCameraPosition;
    public float offsetX;
    public float offsetY;
    public float backFrontMultiplier;
    public float activeLaneMultiplier;

    public int cameraMode; //0 = standard/top run  |  1 = bottom run

    private float addCameraFov;
    private float step;
    private float angle;
    private float scale;
    private float x;
    private float y;

    private GameObject cameraGhost;

    private GameController gameCtrl;

    private bool isPower;
    private Vector3 cameraPos;
    private Vector3 ghostCamPos; 
    private Vector3 oldPos;
    private Vector3 finalShakeVector;

    public bool shakeOFF;
    void Start()
    {
        shakeOFF = true;
        oldPos = gameObject.transform.position;
        gameCtrl = FindObjectOfType<GameController>();
        cameraGhost = new GameObject();
        cameraGhost.transform.position = gameObject.transform.position;
        shiftingBackCamRatio = ShiftingBackCamFov / NOShiftingBackCamFov;
        backCamera = true;
        runON = false;
        offsetX = 0;
        offsetY = 0;
        multiplier = backFrontMultiplier;
        cameraMode = 1;
        BackCameraStartPlace = BackCameraPosition;
        frequency = baseFrequency;
    }

    void Update()
    {


        isPower = gameCtrl.GetPowerStatus();
        BackCameraPosition = BackCameraStartPlace;
        FrontCameraPosition = FrontCameraPlace.position;
        if (cameraMode == 1)
        {
            BackCameraPosition.x += offsetX;
            FrontCameraPosition.x += offsetX;
            
            FrontCameraPosition.y += offsetY;
            BackCameraPosition.y += offsetY;
        }
        step = speed * Time.deltaTime;
        if (runON)
        {
            angle = Mathf.PI;
            scale = 2 / (3 - Mathf.Cos(2 * (angle + Time.time * frequency)));
            x = scale * Mathf.Cos(angle + Time.time * frequency) * lineScaleX;
            y = scale * Mathf.Sin(2 * (angle + Time.time * frequency)) / 2 * lineScaleY;


            if (shakeOFF == true) //camera shake effects off
            {
                finalShakeVector = Vector3.MoveTowards(finalShakeVector, Vector3.zero, step);
            }
            else
            {
                cameraPos = gameObject.transform.position;
                ghostCamPos = cameraGhost.transform.position;
                finalShakeVector = oldPos - ghostCamPos;

            }

            if (backCamera)
            {
                if (Input.GetKey(KeyCode.LeftShift) && isPower) { CameraShifting(); }
                else { CameraNoShifting(); }
            }
            else
            {
                //unfinished
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(FrontCameraPosition.x + x, FrontCameraPosition.y + y, FrontCameraPosition.z), step * multiplier);
                    mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, FrontCameraPlace.localRotation, step);
                    mainCamera.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, ShiftingFrontCam, speed * Time.deltaTime);
                }
                else
                {
                    mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, FrontCameraPosition, step * multiplier);
                    mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, FrontCameraPlace.localRotation, step);
                    mainCamera.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, NOShiftingBackCamFov, speed * Time.deltaTime);//
                }
            }
            /*
            if (Input.GetKeyDown(KeyCode.C))
            {
                backCamera = !backCamera;
                multiplier = backFrontMultiplier;
            }
            */
        } 

    }

    public IEnumerator CameraShake()
    {
        cameraGhost.transform.position = BackCameraPosition;
        oldPos = BackCameraPosition;

        var tweener = cameraGhost.transform.DOShakePosition(0.5f, new Vector3(0.5f, 0.5f, 0), 10, 90, false, true, ShakeRandomnessMode.Full);

        while (tweener.IsActive()) { shakeOFF = false;  yield return null; }
        shakeOFF = true;  
    }
        

    public void CameraShifting()
    {
        frequency = runFrequency;
        if (playerMov.ctr.isGrounded)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(BackCameraPosition.x + x, BackCameraPosition.y + y, BackCameraPosition.z) + finalShakeVector, step * multiplier);
        }
        else
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, BackCameraPosition + finalShakeVector, step * multiplier);
        }
        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, BackCameraPlace.rotation, step);
        //mainCamera.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, ShiftingFov, speed * Time.deltaTime);

        if (mainCamera.fieldOfView < ShiftingBackCamFov)
        {
            addCameraFov += Time.deltaTime; // * shiftingBackCamRatio;
            mainCamera.fieldOfView += addCameraFov;
        }
        else
        {
            mainCamera.fieldOfView = ShiftingBackCamFov;
            addCameraFov = 0;
        }


    }

    private void CameraNoShifting()
    {
        frequency = baseFrequency;
        if (playerMov.ctr.isGrounded) { mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(BackCameraPosition.x + x, BackCameraPosition.y + y, BackCameraPosition.z) + finalShakeVector, step * multiplier); }
        else { mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, BackCameraPosition + finalShakeVector, step * multiplier); }

        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, BackCameraPlace.rotation, step);
        mainCamera.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, NOShiftingBackCamFov, speed * Time.deltaTime);
    }

    public void RunChangeStatus() { runON = !runON; }

    public void MultiplierActiveLane() { multiplier = activeLaneMultiplier; }
   
    public bool GetRunStatus() { return runON; }
}