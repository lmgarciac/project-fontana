using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
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

    private void Start()
    {
        interactableGameObject = this.gameObject;
        isContainerObject = false;
        isPickableObject = false;
    }

    public string GetInteractPrompt(string objectInHand)
    {
        return interactablePrompt;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(GameObject interactorObject)
    {
        Debug.Log("INTERACTING WITH INTERACTIVE OBJECT: " + interactablePrompt);
    }
    public bool IsInteractionPossible(bool objectInHand)
    {
        return true;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public GameObject PickUp(Transform playerHand)
    {
        throw new System.NotImplementedException();
    }

    public void PlaceInside(GameObject objectToPlace)
    {
        throw new System.NotImplementedException();
    }

    public GameObject Replace(GameObject objectToPlace, Transform playerHand)
    {
        throw new System.NotImplementedException();
    }
}
