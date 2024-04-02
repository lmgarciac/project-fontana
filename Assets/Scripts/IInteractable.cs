using UnityEngine;

public interface IInteractable
{
    string InteractablePrompt { get; }
    bool IsPickableObject { get; }

    void Interact();
    string GetInteractPrompt();
    Transform GetTransform();
}
