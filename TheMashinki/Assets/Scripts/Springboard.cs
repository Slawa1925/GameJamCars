using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springboard : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _jumpPlatform;
    [SerializeField] private Transform _playerLauchPoint;
    [SerializeField] private PlayerInput _player;

    private void OnTriggerEnter(Collider other)
    {
        print("LaunchPlayer");
        _player.SpeedBoost(_jumpForce);
        //other.transform.rotation = _playerLauchPoint.rotation;
        //other.transform.position = _playerLauchPoint.position;
        //other.GetComponent<Rigidbody>().AddForce(_jumpPlatform.forward * _jumpForce, ForceMode.Impulse);
    }
}
