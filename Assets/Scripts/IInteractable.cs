using UnityEngine;

public interface IInteractable
{
    string InteractablePrompt { get; }
    bool IsPickableObject { get; }
    bool IsContainerObject { get; }
    GameObject InteractableGameObject { get; }

    void Interact(GameObject interactorObject);
    string GetInteractPrompt();
    Transform GetTransform();
}
