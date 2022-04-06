using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothTime = 0.3F;
    [SerializeField] private float _rotationSpeed = 0.5f;
    [SerializeField] private Vector3 _cameraLocation = new Vector3(0, 2, -5);
    [SerializeField] private float _cameraDistance = 5.0f;
    private Vector3 _velocity = Vector3.zero;


    private void Update()
    {
        var targetPosition = _target.TransformPoint(_cameraLocation * _cameraDistance);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);

        var targetRotation = Quaternion.LookRotation(_target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed);
    }
}
