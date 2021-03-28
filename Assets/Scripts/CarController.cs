using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] GameObject wheelColliderGroupObject;
    private List<WheelCollider> wheelColliders = new List<WheelCollider>();
    [Header("Wheel Meshes")]
    [SerializeField] Transform frontLeftWheelTransform;
    [SerializeField] Transform frontRightWheelTransform;
    [SerializeField] Transform rearLeftWheelTransform;
    [SerializeField] Transform rearRightWheelTransform;

    [SerializeField] float motorForce = 12000f;
    [SerializeField] float maxSteerAngle = 30f;
    [SerializeField] float brakeForce = 5000f;
    [SerializeField] float topRPM = 1000f;
    [SerializeField] float engineSoundPitch = 0.3f;

    private float verticalInput;
    private float horizontalInput;
    private float currentSteerAngle;
    private float brakeInput;
    private void Start() {
       foreach (Transform child in wheelColliderGroupObject.transform) {
            wheelColliders.Add(child.GetComponent<WheelCollider>());
        }
    }

    private void FixedUpdate() {
        HandleInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        HandleBraking();
        //print("current Speed: " + gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude);
    }

    private void HandleInput() {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        brakeInput = Input.GetAxis("Brake");
        //print("vertical input: " + verticalInput);
    }
    private void HandleMotor() {
        foreach (WheelCollider wC in wheelColliders) {
            //print("current rpm: " + wC.rpm);
            if (wC.rpm > topRPM) {
                wC.motorTorque = 0;
            } else {
                //if (wC.gameObject.name.Contains("Front")) {
                    wC.motorTorque = verticalInput * motorForce;
                    HandleSound(verticalInput);
                //}
            }
        }
    }
    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        foreach (WheelCollider wC in wheelColliders) {
            if (wC.gameObject.name.Contains("Front")) {
                wC.steerAngle = currentSteerAngle;
            }
        }
    }
    private void HandleBraking() {
        foreach (WheelCollider wC in wheelColliders) {
            wC.brakeTorque = brakeInput * brakeForce;
        }
    }
    private void UpdateWheels() {
        foreach (WheelCollider wC in wheelColliders) {
            if (wC.gameObject.name.Contains("Front")) {
                if (wC.gameObject.name.Contains("Left")) {
                    UpdateWheel(wC, frontLeftWheelTransform);
                } else {
                    UpdateWheel(wC, frontRightWheelTransform);
                }
            } else {
                if (wC.gameObject.name.Contains("Left")) {
                    UpdateWheel(wC, rearLeftWheelTransform);
                } else {
                    UpdateWheel(wC, rearRightWheelTransform);
                }
            }
        }
    }
    private void UpdateWheel(WheelCollider wC, Transform wT) {
        Vector3 pos;
        Quaternion rot;
        wC.GetWorldPose(out pos, out rot);
        wT.position = pos;
        wT.rotation = rot;
    }
    private void HandleSound(float input) {
        GameObject.Find("WindShield L").GetComponent<AudioSource>().pitch = 1 + (input * engineSoundPitch);
    }
}
