using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float interactRange = 3f;
    [SerializeField]
    private Transform playerLineOfSight;
    [SerializeField]
    private Transform playerHand;
    [SerializeField]
    private Transform playerEyes;
    [SerializeField]
    private LayerMask interactableLayer;
    [SerializeField]
    private PlayerInteractUI playerInteractUI;

    private CharacterController characterController;
    private FirstPersonController firstPersonController;

    private RaycastHit[] raycastArray;
    private GameObject objectInHand;
    private IUsable usableObject;

    public GameObject ObjectInHand { get => objectInHand;}

    public Action OnDialogueFinished;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        firstPersonController = GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactable = GetInteractableObject();
            objectInHand?.TryGetComponent<IUsable>(out usableObject);
            
            if (interactable != null && interactable is InteractableObject) 
            {
                characterController.enabled = false;
                firstPersonController.CanMoveCamera = false;

                if (interactable.InteractableParameters?.interactableDialogue != null &&
                    interactable.InteractableParameters?.interactableDialogue.Count != 0)
                {
                    if (!playerInteractUI.ShowingDialogue)
                        playerInteractUI.ShowDialogue(interactable.InteractableParameters.interactableDialogue, DialogueFinished, interactable);
                    else
                        playerInteractUI.ContinueDialogue();

                    return;
                }

                characterController.enabled = true;
                firstPersonController.CanMoveCamera = true;
            }

            if (interactable == null || (usableObject != null && usableObject.IsBeingUsed()))
            {
                if (!usableObject.IsBeingUsed()) usableObject.Use(playerEyes);
                else usableObject.Unuse(playerHand);
                return;
            }

            if (interactable is InteractablePickable pickable && objectInHand == null)
            {
                characterController.enabled = false;
                firstPersonController.CanMoveCamera = false;

                if (pickable.InteractableParameters?.pickupDialogue != null &&
                    pickable.InteractableParameters?.pickupDialogue.Count != 0)
                {
                    if (!playerInteractUI.ShowingDialogue)
                        playerInteractUI.ShowDialogue(pickable.InteractableParameters.pickupDialogue, PickupDialogueFinished, interactable);
                    else
                        playerInteractUI.ContinueDialogue();

                    return;
                }

                characterController.enabled = true;
                firstPersonController.CanMoveCamera = true;

                objectInHand = pickable.PickUp(playerHand);

                return;
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
                return;
            }

            interactable.Interact(); //This is for interactables that are not pickable or containers
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            IInteractable notepad = GlobalManager.Instance.GetFromInventory(InteractableType.Notepad);
            if (notepad != null)
            {
                NotepadBehavior notepadBehavior = (NotepadBehavior)notepad;

                if (!notepadBehavior.IsBeingUsed()) notepadBehavior.Use(playerEyes);
                else notepadBehavior.Unuse(playerHand);
            }
            else
                Debug.Log("You don't have the Notepad");
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

    private void DialogueFinished(IInteractable interactable)
    {
        characterController.enabled = true;
        firstPersonController.CanMoveCamera = true;
    }

    private void PickupDialogueFinished(IInteractable interactable)
    {
        characterController.enabled = true;
        firstPersonController.CanMoveCamera = true;

        interactable.PickUp(playerHand);
    }
}
