using System.Collections.Generic;
using UnityEngine;

public class ConstellationController : MonoBehaviour
{
    [SerializeField]
    private List<ConstellationHitPoint> hitPoints;

    private void Update()
    {
        // Check for left mouse click
        if (Input.GetMouseButton(0))
        {
            // Go through all ConstellationHitPoints
            foreach (var hitPoint in hitPoints)
            {
                // Check if the point is hovering
                if (hitPoint.IsHovering())
                {
                    // Activate the point
                    hitPoint.ActivatePoint();
                }
            }
        }
    }
}
