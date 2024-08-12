using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D customCursorTexture;
    [SerializeField] private Camera targetCamera;

    [Range(5f, 100f)]
    [SerializeField] private float distanceFromCamera;

    [SerializeField] private bool cursorVisible = false;


    private Vector2 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;

    private void Start()
    {
        distanceFromCamera = 20f;
        Cursor.visible = cursorVisible;

        if (customCursorTexture != null)
        {
            hotSpot = new Vector2(customCursorTexture.width / 2, customCursorTexture.height / 2);
            Cursor.SetCursor(customCursorTexture, hotSpot, cursorMode);
        }

    }

    private void Update()
    {
        // Convert mouse position to world position
        Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = targetCamera.nearClipPlane + distanceFromCamera; // Set the distance from the camera
        mousePosition.z = distanceFromCamera; // Set the distance from the camera

        Vector3 worldPosition = targetCamera.ScreenToWorldPoint(mousePosition);

        // Move the trail object to the mouse position
        transform.position = worldPosition;

        // Rotate the object to face the camera
        transform.LookAt(targetCamera.transform);
    }
}

