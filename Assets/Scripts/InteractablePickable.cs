using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string interactablePrompt;
    private bool isPickableObject;
    private bool isContainerObject;

    private GameObject interactableGameObject;
    private GameObject containedGameObject;

    public GameObject InteractableGameObject { get => interactableGameObject; }
    public string InteractablePrompt { get => interactablePrompt;}
    public bool IsPickableObject { get => isPickableObject;}
    public bool IsContainerObject { get => isContainerObject; }
    public GameObject ContainedGameObject { get => containedGameObject; }

    protected virtual void Start()
    {
        interactableGameObject = this.gameObject;
        isContainerObject = false;
        isPickableObject = true;
    }

    public string GetInteractPrompt(string objectInHand)
    {
        return interactablePrompt;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public GameObject PickUp(Transform playerHand)
    {
        Debug.Log($"PickUp {interactableGameObject.name}");

        transform.parent = playerHand;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        containedGameObject = null;

        return interactableGameObject;
    }
    public bool IsInteractionPossible(bool objectInHand)
    {
        return true;
    }

    public void PlaceInside(GameObject objectToPlace)
    {
        throw new System.NotImplementedException();
    }

    public GameObject Replace(GameObject objectToPlace, Transform playerHand)
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
