using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAlignment : MonoBehaviour
{
    [SerializeField] private bool _lockCameraXTilt = false;
    [SerializeField] private bool _fixed = false;

    private Camera _playerCamera;
    private Camera _portalCamera;
    private Quaternion _direction;
    private Vector3 _distance;
    private float _xAxisTilt = 0f;

    private Quaternion _initialRotation;

    private void Awake()
    {
        _portalCamera = GetComponent<Camera>();
        _playerCamera = Camera.main;
        _initialRotation = this.transform.rotation;
    }

    void LateUpdate()
    {
        if (_fixed)
            return;

        //_direction = Quaternion.Inverse(transform.rotation) * _playerCamera.transform.rotation;
        _direction = _playerCamera.transform.localRotation * _initialRotation;

        if (_lockCameraXTilt)
            _xAxisTilt = _portalCamera.transform.localEulerAngles.x;
        else
            _xAxisTilt = _direction.eulerAngles.x;


        _portalCamera.transform.localEulerAngles = new Vector3(_xAxisTilt,
                                                               _direction.eulerAngles.y,
                                                               _direction.eulerAngles.z);
    }
}
