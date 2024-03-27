using UnityEngine;

public interface IInteractable
{
    void Interact(Transform interactorTransform);
    string GetInteractPrompt();
    Transform GetTransform();
}
