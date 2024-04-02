using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAlignment : MonoBehaviour
{

    [SerializeField] private Camera _playerCamera;

    private Camera _portalCamera;
    private Quaternion _direction;

    private void Awake()
    {
        _portalCamera = GetComponent<Camera>();
    }

    void Update()
    {
        _direction = Quaternion.Inverse(transform.rotation) * _playerCamera.transform.rotation;
        _direction = _playerCamera.transform.rotation;

        _portalCamera.transform.localEulerAngles = new Vector3(_direction.eulerAngles.x,
                                                               _direction.eulerAngles.y + 180,
                                                               _direction.eulerAngles.z);
    }
}
