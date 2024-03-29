using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float interactRange = 3f;
    [SerializeField]
    private Transform playerLineOfSight;
    private RaycastHit[] raycastArray;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactable = GetInteractableObject();
            if (interactable != null)
            {
                interactable.Interact(transform);
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(playerLineOfSight.position, playerLineOfSight.forward, Color.green);
    }

    public IInteractable GetInteractableObject()
    {
        List<IInteractable> interactableList = new List<IInteractable>();
        raycastArray = Physics.RaycastAll(playerLineOfSight.position, playerLineOfSight.forward, interactRange); //Make in non alloc just in case?
        foreach(RaycastHit objectCollision in raycastArray) //Do we need a foreach? Maybe check first collision and thats it
        {
            if (objectCollision.collider.TryGetComponent(out IInteractable interactable))
            {
                interactableList.Add(interactable);
            }
        }

        IInteractable closestInteractable = null;
        foreach(IInteractable interactable in interactableList)
        {
            if(closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {
                if(Vector3.Distance(transform.position, interactable.GetTransform().position) < Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
                {
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }
}
