using System.Collections.Generic;
using UnityEngine;

public class SunCycle : MonoBehaviour
{
    [SerializeField] private float dayDuration = 60.0f; // Duration of a full day in seconds
    [SerializeField] private bool disableCycling = false;

    [SerializeField] private Color dayColor = Color.white;
    [SerializeField] private Color sunsetColor = new Color(1.0f, 0.5f, 0.0f);
    [SerializeField] private Color nightColor = Color.black;

    [SerializeField] private float dayIntensity = 1.0f; // Intensity during the day
    [SerializeField] private float nightIntensity = 0.2f; // Intensity during the night

    [SerializeField] private float maxYAxisAngle = 30.0f; // Maximum tilt angle on the y-axis

    [SerializeField] private List<Light> lamps; // List of point lights representing lamps
    [SerializeField] private Light sunLight; // Reference to the sun light

    private float currentTime = 0f;

    void Start()
    {
        if (disableCycling)
            return;

        if (sunLight == null)
        {
            Debug.LogError("SunCycle script must be assigned a Light component for the sun.");
        }

        foreach (var lamp in lamps)
        {
            if (lamp != null)
            {
                lamp.gameObject.SetActive(true);
            }
        }
    }

    void Update()
    {
        if (disableCycling || sunLight == null)
            return;

        // Update the current time of day
        currentTime += Time.deltaTime;
        float timePercentage = (currentTime % dayDuration) / dayDuration;

        // Calculate rotation angles
        float rotationAngleX = timePercentage * 360.0f - 90.0f;
        float rotationAngleY = Mathf.Sin(timePercentage * Mathf.PI * 2) * maxYAxisAngle;

        // Apply rotation to simulate the sun's movement
        sunLight.transform.rotation = Quaternion.Euler(new Vector3(rotationAngleX, 170.0f + rotationAngleY, 0.0f));

        // Adjust color and intensity based on rotationAngleX
        float sunIntensity;
        if (rotationAngleX >= -90.0f && rotationAngleX < 0.0f) // Sunrise
        {
            float t = (rotationAngleX + 90.0f) / 90.0f;
            sunLight.color = Color.Lerp(nightColor, sunsetColor, t);
            sunIntensity = Mathf.Lerp(nightIntensity, dayIntensity, t);
        }
        else if (rotationAngleX >= 0.0f && rotationAngleX < 90.0f) // Day
        {
            float t = rotationAngleX / 90.0f;
            sunLight.color = Color.Lerp(sunsetColor, dayColor, t);
            sunIntensity = dayIntensity;
        }
        else if (rotationAngleX >= 90.0f && rotationAngleX < 180.0f) // Sunset
        {
            float t = (rotationAngleX - 90.0f) / 90.0f;
            sunLight.color = Color.Lerp(dayColor, sunsetColor, t);
            sunIntensity = Mathf.Lerp(dayIntensity, nightIntensity, t);
        }
        else // Night
        {
            float t = (rotationAngleX + 180.0f) / 90.0f;
            sunLight.color = Color.Lerp(sunsetColor, nightColor, t);
            sunIntensity = nightIntensity;
        }

        sunLight.intensity = sunIntensity;

        // Update lamp intensities based on the inverse of the sun's intensity
        float lampIntensity = (1.0f - sunIntensity)*5.0f;
        foreach (var lamp in lamps)
        {
            if (lamp != null)
            {
                lamp.intensity = lampIntensity;
            }
        }
    }
}
