using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string interactablePrompt;

    public string GetInteractPrompt()
    {
        return interactablePrompt;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("INTERACTING WITH OBJECT: " + interactablePrompt);
    }
}
