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
    private GameObject objectInHand;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractableObject interactable = GetInteractableObject();

            if (interactable == null) return;

            if (interactable.IsPickableObject) //Add interactable to hand 
            {
                objectInHand = Instantiate(interactable.gameObject, playerLineOfSight.GetChild(0)); //(inventory slot 0?)
                objectInHand.transform.localPosition = Vector3.zero;

                interactable.Interact(objectInHand); //Object interaction
            }
            else if (interactable.IsContainerObject)
            {
                interactable.Interact(objectInHand);
                Destroy(objectInHand);
            }
            else
            {
                interactable.Interact(objectInHand); //Sending object in hand could be useful 
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
