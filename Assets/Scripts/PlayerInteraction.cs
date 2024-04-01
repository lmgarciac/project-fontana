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
            InteractableObject interactable = GetInteractableObject();

            if (interactable == null) return;

            if (interactable.IsPickableObject)
            {
                interactable.Interact(); //Object interaction (maybe make himself instantiate)

                GameObject pickedObject = Instantiate(interactable.gameObject, playerLineOfSight.GetChild(0)); //First child (inventory slot 0?)
                pickedObject.transform.localPosition = Vector3.zero;
            }
            else
            {
                interactable.Interact();
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(playerLineOfSight.position, playerLineOfSight.forward, Color.green);
    }

    public InteractableObject GetInteractableObject()
    {
        List<InteractableObject> interactableList = new List<InteractableObject>();
        raycastArray = Physics.RaycastAll(playerLineOfSight.position, playerLineOfSight.forward, interactRange); //Make in non alloc just in case?
        foreach(RaycastHit objectCollision in raycastArray) //Do we need a foreach? Maybe check first collision and thats it
        {
            if (objectCollision.collider.TryGetComponent(out InteractableObject interactable))
            {
                interactableList.Add(interactable);
            }
        }

        InteractableObject closestInteractable = null;
        foreach(InteractableObject interactable in interactableList)
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
