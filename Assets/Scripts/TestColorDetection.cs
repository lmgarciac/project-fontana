using UnityEngine;

public class TestColorDetection : MonoBehaviour
{
    public Material starShaderMaterial;
    public Renderer cubeRenderer;

    private Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast to detect if the cube is clicked
            if (Physics.Raycast(ray, out hit))
            {
                    // Convert the hit point to texture UV coordinates
                    Vector2 uv = hit.textureCoord;

                    // Pass the UV coordinates to the shader
                    starShaderMaterial.SetVector("_MouseUV", new Vector4(uv.x, uv.y, 0, 0));

                    // Check the shader output to see if a white point was detected
                    float starDetected = starShaderMaterial.GetFloat("_StarDetected");

                    if (starDetected > 0.5f)
                    {
                        // A white point was detected on the mask, trigger an action
                        Debug.Log("Star detected on the cube!");

                        // Trigger additional actions here
                    }
            }
    }
}
