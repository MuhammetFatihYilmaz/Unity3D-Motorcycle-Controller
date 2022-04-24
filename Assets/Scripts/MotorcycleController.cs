using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MotorcycleController : MonoBehaviour
{
    //
    Rigidbody motorcycleRigidbody;
    [SerializeField] WheelCollider frontWheelCollider;
    [SerializeField] Transform[] SteeringPiecesTransforms;
    //
    [SerializeField] float movePower;
    [SerializeField] float brakePower;
    [SerializeField] float inNeutralBrakePower;
    [SerializeField] float maxSteerRotateAngle;
    //
    float currentSteerRotateAngle;
    bool isBraking;
    float currentBrakePower;
    //
    [SerializeField] TMP_Text speedText;
    int speed = 0;
    //
    bool isMovingSoundPlaying = false;
    bool isInNeutralSoundPlaying = false;
    bool isbrakingSoundPlaying = false;

    void Start()
    {
        motorcycleRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        DisplayMotorcycleSpeedText();
    }

    void FixedUpdate()
    {
        VerticalMove();
        HorizontalMove();
        FollowAllSteeringPieceToWheelRotation();
    }

    void VerticalMove()
    {
        float verticalInput = Input.GetAxis("Vertical");
        frontWheelCollider.motorTorque = verticalInput * movePower;
        if(verticalInput > 0)
        {
            SetMotorcycleMovingSound();
            CheckPutBrake();
            isInNeutralSoundPlaying = false;
        }
        //
        else if (verticalInput == 0)
        {
            SetMotorcycleInNeutralSound();
            frontWheelCollider.brakeTorque = inNeutralBrakePower;
            isMovingSoundPlaying = false;
        }
        else
        {
            SetMotorcycleInNeutralSound();
            frontWheelCollider.brakeTorque = 0;
            isMovingSoundPlaying = false;
        }

    }

    void HorizontalMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        currentSteerRotateAngle = maxSteerRotateAngle * horizontalInput;
        frontWheelCollider.steerAngle = currentSteerRotateAngle;
    }

    void CheckPutBrake()
    {
        isBraking = Input.GetKey(KeyCode.Space);
        currentBrakePower = isBraking ? brakePower : 0f;
        frontWheelCollider.brakeTorque = currentBrakePower;
        if (isBraking)
            SetMotorcycleBrakingSound();
        else
            isbrakingSoundPlaying = false;
    }

    void FollowAllSteeringPieceToWheelRotation()
    {
        foreach (Transform piece in SteeringPiecesTransforms)
        {
            piece.localEulerAngles = new Vector3(piece.localEulerAngles.x, frontWheelCollider.steerAngle, piece.localEulerAngles.z);
        }
    }

    void DisplayMotorcycleSpeedText()
    {
        speed = Mathf.RoundToInt(motorcycleRigidbody.velocity.magnitude * 3.6f);
        speedText.text = speed.ToString();
    }

    void SetMotorcycleMovingSound()
    {
        if (!isMovingSoundPlaying)
        {
            AudioManager.Instance.StopMotorcycleEngineSound();
            AudioManager.Instance.PlayMotorcycleSpeedUpSound();
            isMovingSoundPlaying = true;
        }
    }

    void SetMotorcycleInNeutralSound()
    {
        if (!isInNeutralSoundPlaying)
        {
            AudioManager.Instance.StopMotorcycleSpeedUpSound();
            AudioManager.Instance.PlayMotorcycleEngineSound();
            isInNeutralSoundPlaying = true;
        }
    }

    void SetMotorcycleBrakingSound()
    {
        if (!isbrakingSoundPlaying)
        {
            AudioManager.Instance.StopMotorcycleEngineSound();
            AudioManager.Instance.StopMotorcycleSpeedUpSound();
            AudioManager.Instance.PlayMotorcycleBrakingSound();
            isbrakingSoundPlaying = true;
            isMovingSoundPlaying = false;
        }
    }

}
