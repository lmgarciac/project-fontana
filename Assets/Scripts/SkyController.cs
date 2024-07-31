using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private bool enableRotation = false;

    [SerializeField, Range(0.0f, 0.5f)]
    private float normalizedEdgeThreshold = 0.05f; // Valid range: 0.0 to 0.5

    [SerializeField] private float cameraRotateSpeed = 30f;

    private Camera camera;
    private Material skyboxMaterial;

    void Start()
    {
        camera = this.GetComponent<Camera>();
        skyboxMaterial = camera.GetComponent<Skybox>().material;
    }

    void Update()
    {
        if (enableRotation)
        {
            RotateSkybox();
        }

        RotateCameraTowardsMouse();
    }

    void RotateSkybox()
    {
        float rotation = skyboxMaterial.GetFloat("_Rotation");

        rotation += rotationSpeed * Time.deltaTime;

        if (rotation >= 360.0f)
        {
            rotation -= 360.0f;
        }

        skyboxMaterial.SetFloat("_Rotation", rotation);
    }

    void RotateCameraTowardsMouse()
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
        bool isWithinThreshold = Mathf.Abs(normalizedX - 0.5f) > (0.5f - normalizedEdgeThreshold) ||
                                 Mathf.Abs(normalizedY - 0.5f) > (0.5f - normalizedEdgeThreshold);

        if (isWithinThreshold)
        {
            // Calculate the rotation based on the direction vector
            Vector3 rotation = new Vector3(-direction.y, direction.x, 0f);

            // Apply the rotation to the camera
            camera.transform.Rotate(rotation * cameraRotateSpeed * Time.deltaTime, Space.Self);
        }
    }
}
