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
    [SerializeField]
    private Transform playerContainer;
    private RaycastHit[] raycastArray;
    private GameObject objectInHand;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactable = GetInteractableObject();

            if (interactable == null) return;

            if (interactable.IsPickableObject) //Add interactable to hand 
            {
                //objectInHand = Instantiate(interactable.InteractableGameObject, playerLineOfSight.GetChild(0)); //(inventory slot 0?)
                interactable.InteractableGameObject.transform.parent = playerContainer;
                interactable.InteractableGameObject.transform.localPosition = Vector3.zero;
                interactable.InteractableGameObject.transform.localRotation = Quaternion.identity;
                
                interactable.Interact(interactable.InteractableGameObject); //Object interaction

                objectInHand = interactable.InteractableGameObject;
            }
            else if (interactable.IsContainerObject)
            {
                interactable.Interact(objectInHand);
                objectInHand = null;
            }
            else
            {
                interactable.Interact(interactable.InteractableGameObject); //Sending object in hand could be useful 
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
        foreach (RaycastHit objectCollision in raycastArray) //Do we need a foreach? Maybe check first collision and thats it
        {
            if (objectCollision.collider.TryGetComponent(out IInteractable interactable))
            {
                interactableList.Add(interactable);
            }
        }

        IInteractable closestInteractable = null;
        foreach (IInteractable interactable in interactableList)
        {
            if (closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {
                if (Vector3.Distance(transform.position, interactable.GetTransform().position) < Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
                {
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }
}
