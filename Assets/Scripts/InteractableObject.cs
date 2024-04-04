using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string interactablePrompt;
    [SerializeField]
    private bool isPickableObject;
    [SerializeField]
    private bool isContainerObject;

    public string InteractablePrompt { get => interactablePrompt;}
    public bool IsPickableObject { get => isPickableObject;}
    public bool IsContainerObject { get => isContainerObject; }

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
        Debug.Log("INTERACTING WITH OBJECT: " + interactablePrompt);

        if (isPickableObject)
        {
            Debug.Log("DESTROYING GAMEOBJECT - PICKED UP");
            Destroy(this.gameObject);
        }
        else if (isContainerObject)
        {
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
}
