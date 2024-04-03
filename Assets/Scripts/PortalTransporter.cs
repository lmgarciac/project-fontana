using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTransporter : MonoBehaviour
{
    [SerializeField] private PortalBehaviorRT _targetPortal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Transform cameraTransform = _targetPortal.GetCamera().transform;
            Debug.LogError($"Player Enter! {cameraTransform.gameObject.name} {cameraTransform.gameObject.transform.position}");

            CharacterController playerController = other.gameObject.GetComponent<CharacterController>();
            if (playerController != null)
            {
                // Move the player using CharacterController's Move method
                playerController.enabled = false; // Disable the controller temporarily to avoid interference
                other.gameObject.transform.position = cameraTransform.position;
                //other.gameObject.transform.rotation = cameraTransform.rotation;
                other.gameObject.transform.rotation = Quaternion.Euler(0f, cameraTransform.rotation.eulerAngles.y, 0f);

                playerController.enabled = true; // Re-enable the controller

                _targetPortal.ExitPortalZone(other);
            }
            else
            {
                Debug.LogError("Player object does not have a CharacterController component attached.");
            }
        }
    }
}
