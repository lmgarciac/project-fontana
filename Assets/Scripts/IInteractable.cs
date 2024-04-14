using UnityEngine;

public interface IInteractable
{
    string InteractablePrompt { get; }
    bool IsPickableObject { get; }
    bool IsContainerObject { get; }
    GameObject InteractableGameObject { get; }
    GameObject ContainedGameObject { get; }

    void Interact();
    GameObject PickUp(Transform playerHand);
    void PlaceInside(GameObject objectToPlace);
    GameObject Replace(GameObject objectToPlace, Transform playerHand);

    string GetInteractPrompt(string objectInHand = null);
    Transform GetTransform();
    bool IsInteractionPossible(bool objectInHand);
}
