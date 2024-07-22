using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    private bool isPanelVisible = true;
    private GUIStyle style;

    private void Start()
    {
        style = new GUIStyle();
        style.fontSize = 60;
        style.normal.textColor = Color.white;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            isPanelVisible = !isPanelVisible;
        }
    }

    private void OnGUI()
    {
        if (isPanelVisible)
        {
            float panelWidth = 600;
            float panelHeight = 600;
            float marginX = 100;
            float marginY = 50;
            float panelX = Screen.width - panelWidth - marginX;
            Rect panelRect = new Rect(panelX - panelWidth * 0.5f, marginY, panelWidth, panelHeight);

            GUI.Box(panelRect, "Debug Panel", style);

            // Display FPS
            float fps = 1.0f / Time.deltaTime;
            GUI.Label(new Rect(panelRect.x + 10, panelRect.y + 100, panelWidth - 20, 40), $"FPS: {fps:F2}", style);

            // Display memory usage
            float memoryUsed = System.GC.GetTotalMemory(false) / (1024f * 1024f); // Memory in MB
            GUI.Label(new Rect(panelRect.x + 10, panelRect.y + 200, panelWidth - 20, 40), $"Memory Usage: {memoryUsed:F2} MB", style);

            // Display additional info lines
            GUI.Label(new Rect(panelRect.x + 10, panelRect.y + 300, panelWidth - 20, 40), "Debug Panel: \"|\"", style);
            GUI.Label(new Rect(panelRect.x + 10, panelRect.y + 400, panelWidth - 20, 40), "Telescope Camera: \"C\"", style);
        }
    }
}
