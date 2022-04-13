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

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _centreOfMass;
    [SerializeField] private WheelAxle[] _wheelAxle;
    [SerializeField] private float _maxMotorTorque; // крутящий момент колеса
    [SerializeField] private float _maxSteeringAngle;
    [SerializeField]  private float _motor;
    private float _speedMultiplier = 0.5f;
    private float _steering;


    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        //GetComponent<Rigidbody>().centerOfMass = _centreOfMass.position;
        SpeedBoost(0.5f);
    }

    public void SpeedBoost(float multiplier)
    {
        _speedMultiplier = multiplier;
        print(_speedMultiplier);
    }

    private void Update()
    {
        //_motor = _maxMotorTorque * Input.GetAxis("Vertical");


        _motor = _maxMotorTorque * _speedMultiplier;
        _steering = Mathf.Clamp(Input.acceleration.x * _maxSteeringAngle, -_maxSteeringAngle, _maxSteeringAngle);


#if UNITY_EDITOR
        _steering = Input.GetAxis("Horizontal") * _maxSteeringAngle;
#endif

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
            _wheelAxle[i].Update(_steering, _motor);
        }
    }

    private void OnGUI()
    {
        GUI.Button(new Rect(new Vector2(Screen.width / 2, 50), new Vector2(200, 200)), "Velocity: " + _rigidbody.velocity.magnitude);
    }
}
