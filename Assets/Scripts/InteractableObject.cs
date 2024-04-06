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

    public GameObject InteractableGameObject { get => interactableGameObject; }
    public string InteractablePrompt { get => interactablePrompt;}
    public bool IsPickableObject { get => isPickableObject;}
    public bool IsContainerObject { get => isContainerObject; }

    private void Start()
    {
        interactableGameObject = this.gameObject;
        isContainerObject = false;
        isPickableObject = false;
    }

    public string GetInteractPrompt()
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
}
