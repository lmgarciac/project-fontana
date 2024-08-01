using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    [Header ("Auto movement parameters")]
    [SerializeField] private bool enableAutoMovement = false;
    [SerializeField] private float autoMovementSpeed = 1f;

    [Header("Manual movement parameters")]
    [SerializeField, Range(0.0f, 0.5f)]
    private float edgeThreshold = 0.15f;
    [SerializeField] private float cameraRotateSpeed = 30f;

    [Header("Zoom Parameters")]
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField, Range(15f, 89f)]
    private float minFov = 15f;
    [SerializeField, Range(16f, 90f)]
    private float maxFov = 90f;

    private Camera camera;
    private Material skyboxMaterial;

    void Start()
    {
        camera = this.GetComponent<Camera>();
        skyboxMaterial = camera.GetComponent<Skybox>().material;
    }

    void OnValidate()
    {
        // Ensure minFov is always less than maxFov
        if (minFov >= maxFov)
        {
            minFov = maxFov - 1f;
        }
    }

    void Update()
    {
        if (enableAutoMovement)
        {
            RotateSkybox();
        }

        MoveCamera();
        ApplyZoom();
    }

    private void ApplyZoom()
    {
        float fov = camera.fieldOfView;

        if (Input.GetKey(KeyCode.W))
        {
            fov -= zoomSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            fov += zoomSpeed * Time.deltaTime;
        }

        fov = Mathf.Clamp(fov, minFov, maxFov);
        camera.fieldOfView = fov;
    }

    void RotateSkybox()
    {
        float rotation = skyboxMaterial.GetFloat("_Rotation");

        rotation += autoMovementSpeed * Time.deltaTime;

        if (rotation >= 360.0f)
        {
            rotation -= 360.0f;
        }

        skyboxMaterial.SetFloat("_Rotation", rotation);
    }

    void MoveCamera()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Normalize the mouse position
        float normalizedX = mousePosition.x / Screen.width;
        float normalizedY = mousePosition.y / Screen.height;

        // Calculate the direction from the screen center to the mouse position
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0f); // center in normalized coordinates
        Vector3 mousePosNormalized = new Vector3(normalizedX, normalizedY, 0f);
        Vector3 direction = mousePosNormalized - screenCenter;

        // Check if the mouse is within the threshold from the screen edges
        bool isWithinThreshold = Mathf.Abs(normalizedX - 0.5f) > (0.5f - edgeThreshold) ||
                                 Mathf.Abs(normalizedY - 0.5f) > (0.5f - edgeThreshold);

        if (isWithinThreshold)
        {
            // Calculate the rotation based on the direction vector
            Vector3 rotation = new Vector3(-direction.y, direction.x, 0f);

            // Apply the rotation to the camera
            camera.transform.Rotate(rotation * cameraRotateSpeed * Time.deltaTime, Space.Self);
        }
    }
}
