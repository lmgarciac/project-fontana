using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string interactablePrompt;
    [SerializeField]
    private bool isPickableObject;

    public string InteractablePrompt { get => interactablePrompt;}
    public bool IsPickableObject { get => isPickableObject;}

    public string GetInteractPrompt()
    {
        return interactablePrompt;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact()
    {
        Debug.Log("INTERACTING WITH OBJECT: " + interactablePrompt);

        if (isPickableObject)
        {
            Debug.Log("DESTROYING GAMEOBJECT - PICKED UP");
            Destroy(this.gameObject);
        }
    }
}
