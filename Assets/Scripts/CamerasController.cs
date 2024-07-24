using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    Skybox = 0,
    Player,
}

[Serializable]
public struct CameraTypes
{
    public CameraType CameraType;
    public Camera camera;
}

public class CamerasController : MonoBehaviour
{
    public CameraTypes[] cameras; 
    private CameraType currentCamera = 0;
    private CameraType previousCamera = 0;

    void Start()
    {
        GlobalManager.Instance.EnableCharacterController(true);
        GlobalManager.Instance.EnableFirstPersonController(true);
        GlobalManager.Instance.EnableStaticCursor(true);
        GlobalManager.Instance.SetCursorLockState(true);
        SetCamera(CameraType.Player);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentCamera == CameraType.Player)
                SetCamera(CameraType.Skybox);
            else
                SetCamera(CameraType.Player);
        }
    }

    public void SetCamera(CameraType cameraSelected)
    {
        foreach (CameraTypes cam in cameras)
        {
            cam.camera.enabled = false;
            if (cam.CameraType == cameraSelected)
            {
                cam.camera.enabled = true;
                previousCamera = currentCamera;
                currentCamera = cameraSelected;

                if (cameraSelected == CameraType.Player)
                {
                    GlobalManager.Instance.EnableCharacterController(true);
                    GlobalManager.Instance.EnableFirstPersonController(true);
                    GlobalManager.Instance.EnableStaticCursor(true);
                    GlobalManager.Instance.SetCursorLockState(true);
                }
                else
                {
                    GlobalManager.Instance.EnableCharacterController(false);
                    GlobalManager.Instance.EnableFirstPersonController(false);
                    GlobalManager.Instance.EnableStaticCursor(false);
                    GlobalManager.Instance.SetCursorLockState(false);
                }
            }
        }
    }
}
