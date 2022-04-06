using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [System.Serializable]
    public class WheelAxle
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public Transform leftWheelModel;
        public Transform rightWheelModel;
        public bool isMotor; // is this wheel attached to motor?
        public bool isSteering; // does this wheel apply steer angle?

        public void Update(float steering, float motor)
        {
            if (isSteering)
            {
                leftWheel.steerAngle = steering;
                rightWheel.steerAngle = steering;
            }
            if (isMotor)
            {
                leftWheel.motorTorque = motor;
                rightWheel.motorTorque = motor;
            }

            leftWheel.GetWorldPose(out Vector3 pos, out Quaternion rot);
            leftWheelModel.position = pos;
            leftWheelModel.rotation = rot;

            rightWheel.GetWorldPose(out pos, out rot);
            rightWheelModel.position = pos;
            rightWheelModel.rotation = rot;
        }

        public void Break(float brakeTorque)
        {
            print("break");
            leftWheel.brakeTorque = brakeTorque;
            rightWheel.brakeTorque = brakeTorque;
        }
    }

    [SerializeField] private WheelAxle[] _wheelAxle;
    [SerializeField] private float _maxMotorTorque; // крутящий момент колеса
    [SerializeField] private float _maxSteeringAngle;
    private float _motor;
    private float _steering;


    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Update()
    {
        //_motor = _maxMotorTorque * Input.GetAxis("Vertical");

        _motor = _maxMotorTorque * 0.5f;
        _steering = Mathf.Clamp(Input.acceleration.x * _maxSteeringAngle, -_maxSteeringAngle, _maxSteeringAngle);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            _wheelAxle[0].Break(800);
            _wheelAxle[1].Break(800);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _wheelAxle[0].Break(0);
            _wheelAxle[1].Break(0);
        }

        for (int i = 0; i < _wheelAxle.Length; i++)
        {
            if (_wheelAxle[i].isSteering)
            {
                _wheelAxle[i].leftWheel.steerAngle = _steering;
                _wheelAxle[i].rightWheel.steerAngle = _steering;
            }
            if (_wheelAxle[i].isMotor)
            {
                _wheelAxle[i].leftWheel.motorTorque = _motor;
                _wheelAxle[i].rightWheel.motorTorque = _motor;
            }

            _wheelAxle[i].Update(_steering, _motor);
        }
    }
}
