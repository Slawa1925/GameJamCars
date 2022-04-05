using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public Transform leftWheelModel;
        public Transform rightWheelModel;
        public bool motor; // is this wheel attached to motor?
        public bool steering; // does this wheel apply steer angle?

        public void update()
        {
            Vector3 pos;
            Quaternion rot;
            leftWheel.GetWorldPose(out pos, out rot);
            leftWheelModel.position = pos;
            leftWheelModel.rotation = rot;

            rightWheel.GetWorldPose(out pos, out rot);
            rightWheelModel.position = pos;
            rightWheelModel.rotation = rot;
        }

    }
    public List<AxleInfo> axleInfos; // the information about each individual axle
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _directionArrow;
    [SerializeField] private float _acceleration;

    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public float motor;


    private void Start()
    {
        
    }

    public void FixedUpdate()
    {
         motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            axleInfo.update();
        }
    }

    private void Update()
    {
        //var input = Input.acceleration;//new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));//Input.acceleration;
        //_rigidbody.AddForce(Vector3.forward * _acceleration * Time.deltaTime, ForceMode.Acceleration);

        //_rigidbody.ro

        /*var input = Quaternion.Euler(90, 0, 0) * Input.acceleration;



        _rigidbody.AddForce(input);
        Debug.DrawLine(transform.position, transform.position + input * 5);
        _directionArrow.localScale = Vector3.one * input.magnitude;

        _directionArrow.LookAt(transform.position + input);*/
    }
}
