using UnityEngine;

public interface IInteractable
{
    string InteractablePrompt { get; }
    bool IsPickableObject { get; }
    bool IsContainerObject { get; }
    GameObject InteractableGameObject { get; }
    GameObject ContainedGameObject { get; }   
    InteractableParameters InteractableParameters { get; }
    
    void Interact();

    void ChangeObjectMesh(InteractablePickable interactable, bool setAlternative);
    void PlaceInside(GameObject objectToPlace);
    GameObject PickUp(Transform playerHand);
    GameObject Replace(GameObject objectToPlace, Transform playerHand);

    Transform GetTransform();
    string GetInteractPrompt(string objectInHand = null);
    bool IsInteractionPossible(bool objectInHand);
}
