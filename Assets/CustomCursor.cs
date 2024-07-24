using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D customCursorTexture;
    [SerializeField] private ParticleSystem cursorTrail;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private void Start()
    {
        Cursor.SetCursor(customCursorTexture, hotSpot, cursorMode);
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        this.transform.position = mousePosition;
    }
}
