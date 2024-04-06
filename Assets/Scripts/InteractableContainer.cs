using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableContainer : MonoBehaviour, IInteractable
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
        isContainerObject = true;
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
        Debug.Log("INTERACTING WITH CONTAINER OBJECT: " + interactablePrompt);

        if (interactorObject == null)
        {
            Debug.Log("EMPTY HAND, CANT PLACE");
        }
        else
        {
            Debug.Log("PLACING OBJECT IN HAND: " + interactorObject.name);
            GameObject placedObject = Instantiate(interactorObject, transform);
            placedObject.transform.localPosition = Vector3.zero;
        }
    }
}
