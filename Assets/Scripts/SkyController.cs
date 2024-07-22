using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    
    private Camera camera;

    private Material skyboxMaterial;

    void Start()
    {
        camera = this.GetComponent<Camera>();
        skyboxMaterial = camera.GetComponent<Skybox>().material;
    }

    void Update()
    {
        float rotation = skyboxMaterial.GetFloat("_Rotation");

        rotation += rotationSpeed * Time.deltaTime;

        if (rotation >= 360.0f)
        {
            rotation -= 360.0f;
        }

        skyboxMaterial.SetFloat("_Rotation", rotation);
    }
}
