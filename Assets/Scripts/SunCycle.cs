using UnityEngine;

public class SunCycle : MonoBehaviour
{
    [SerializeField] private float dayDuration = 60.0f; // Duration of a full day in seconds
    [SerializeField] private bool disableCycling = false;

    private Light sunLight;
    private float currentTime = 0f;

    void Start()
    {
        sunLight = GetComponent<Light>();
        if (sunLight == null)
        {
            Debug.LogError("SunCycle script must be attached to a GameObject with a Light component.");
        }
    }

    void Update()
    {
        if (disableCycling)
            return;

        // Calculate the current time of day
        currentTime += Time.deltaTime;
        float timePercentage = currentTime / dayDuration;

        // Loop the time percentage
        if (timePercentage >= 1.0f)
        {
            currentTime = 0f;
            timePercentage = 0f;
        }

        // Rotate the light
        float rotationAngle = timePercentage * 360.0f;
        transform.rotation = Quaternion.Euler(new Vector3(rotationAngle - 90.0f, 170.0f, 0.0f));

        // Change the color
        if (timePercentage <= 0.5f)
        {
            // Day to sunset transition
            sunLight.color = Color.Lerp(Color.white, new Color(1.0f, 0.5f, 0.0f), timePercentage * 2.0f);
        }
        else
        {
            // Sunset to night transition
            sunLight.color = Color.Lerp(new Color(1.0f, 0.5f, 0.0f), Color.black, (timePercentage - 0.5f) * 2.0f);
        }
    }
}
