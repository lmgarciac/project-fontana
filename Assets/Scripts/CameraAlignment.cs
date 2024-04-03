using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAlignment : MonoBehaviour
{
    [SerializeField] private bool _lockCameraXTilt = false;

    private Camera _playerCamera;
    private Camera _portalCamera;
    private Quaternion _direction;
    private Vector3 _distance;
    private float _xAxisTilt = 0f;

    private void Awake()
    {
        _portalCamera = GetComponent<Camera>();
        _playerCamera = Camera.main;
    }

    void Update()
    {
        _direction = Quaternion.Inverse(transform.rotation) * _playerCamera.transform.rotation;
        _direction = _playerCamera.transform.rotation;

        if (_lockCameraXTilt)
            _xAxisTilt = _portalCamera.transform.localEulerAngles.x;
        else
            _xAxisTilt = _direction.eulerAngles.x;


        _portalCamera.transform.localEulerAngles = new Vector3(_xAxisTilt,
                                                               _direction.eulerAngles.y + 180,
                                                               _direction.eulerAngles.z);
    }
}
