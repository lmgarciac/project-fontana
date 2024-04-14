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
    private Transform playerHand;
    [SerializeField]
    private LayerMask interactableLayer;
    private RaycastHit[] raycastArray;
    private GameObject objectInHand;

    public GameObject ObjectInHand { get => objectInHand;}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactable = GetInteractableObject();

            if (interactable == null) return;

            if (interactable.IsPickableObject && objectInHand == null)
            {
                objectInHand = interactable.PickUp(playerHand);
            }
            else if (interactable.IsContainerObject)
            {
                if (objectInHand != null && interactable.ContainedGameObject == null)
                {
                    interactable.PlaceInside(objectInHand);
                    objectInHand = null;
                }
                else if (objectInHand == null && interactable.ContainedGameObject != null)
                    objectInHand = interactable.PickUp(playerHand);
                else if (objectInHand != null && interactable.ContainedGameObject != null)
                    objectInHand = interactable.Replace(objectInHand, playerHand);
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
        raycastArray = Physics.RaycastAll(playerLineOfSight.position, playerLineOfSight.forward, interactRange, interactableLayer); //Make in non alloc just in case?

        IInteractable closestInteractable = null;
        Vector3 closestImpactPoint = Vector3.zero;

        foreach (RaycastHit objectCollision in raycastArray) //Do we need a foreach? Maybe check first collision and thats it
        {

            if (objectCollision.collider.TryGetComponent(out IInteractable interactable))
            {
                interactableList.Add(interactable);

                if (closestInteractable == null)
                {
                    closestInteractable = interactable;
                    closestImpactPoint = objectCollision.point;
                }

                //Debug.LogError($"Colliding with {interactable.InteractableGameObject.name} distance: {Vector3.Distance(playerLineOfSight.position, objectCollision.point)}");
                //Debug.LogError($"Closest is {closestInteractable.InteractableGameObject.name} distance: {Vector3.Distance(playerLineOfSight.position, closestImpactPoint)}");

                if (Vector3.Distance(playerLineOfSight.position, objectCollision.point) < Vector3.Distance(playerLineOfSight.position, closestImpactPoint))
                {
                    closestInteractable = interactable;
                }
            }
        }

        //IInteractable closestInteractable = null;
        //foreach (IInteractable interactable in interactableList)
        //{
        //    if (closestInteractable == null)
        //    {
        //        closestInteractable = interactable;
        //    }
        //    else
        //    {
        //        if (Vector3.Distance(transform.position, interactable.GetTransform().position) < Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
        //        {
        //            closestInteractable = interactable;
        //        }
        //    }
        //}

        return closestInteractable;
    }

}
