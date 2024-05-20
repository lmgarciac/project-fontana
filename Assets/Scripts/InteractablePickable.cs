using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickable : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractableType interactableType;
    [SerializeField]
    private string interactableName;
    [SerializeField]
    private string interactablePrompt;
    [SerializeField]
    private bool hasAlternativeMesh;

    private bool isPickableObject;
    private bool isContainerObject;

    private GameObject interactableGameObject;
    private GameObject containedGameObject;

    private InteractableParameters interactableParameters;

    public GameObject InteractableGameObject { get => interactableGameObject; }
    public string InteractableName { get => interactableName; }
    public string InteractablePrompt { get => interactablePrompt;}
    public bool IsPickableObject { get => isPickableObject;}
    public bool IsContainerObject { get => isContainerObject; }
    public bool HasAlternativeMesh { get => hasAlternativeMesh; }
    public GameObject ContainedGameObject { get => containedGameObject; }
    public InteractableParameters InteractableParameters { get => interactableParameters; }

    public virtual void Start()
    {
        interactableGameObject = this.gameObject;
        isContainerObject = false;
        isPickableObject = true;
        interactableParameters = GlobalManager.Instance.GetInteractionParameters(interactableType);
    }

    public string GetInteractPrompt(string objectInHand)
    {
        return interactablePrompt;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public GameObject PickUp(Transform playerHand)
    {
        if (!InteractableParameters.isInventoryItem)
        {
            Debug.Log($"PickUp {interactableGameObject.name}");

            transform.parent = playerHand;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            containedGameObject = null;

            ChangeObjectMesh(this, false); //This way it always displays non-alternative mesh on hand (child 0)

            return interactableGameObject;
        }
        else
        {
            transform.gameObject.SetActive(false);
            SendToInventory();
            return null;
        }
    }

    public virtual void SendToInventory()
    {
        GlobalManager.Instance.AddToInventory(interactableType, this);
        Debug.Log($"Sending {interactableGameObject.name} to inventory");
    }

    public bool IsInteractionPossible(bool objectInHand)
    {
        return true;
    }

    public void ChangeObjectMesh(InteractablePickable interactable, bool setAlternative)
    {
        if (interactable.HasAlternativeMesh)
        {
            interactable.transform.GetChild(0).gameObject.SetActive(!setAlternative);
            interactable.transform.GetChild(1).gameObject.SetActive(setAlternative);
        }
    }

    public void PlaceInside(GameObject objectToPlace)
    {
    }

    public GameObject Replace(GameObject objectToPlace, Transform playerHand)
    {
        return null;
    }

    public void Interact()
    {
        Debug.Log($"Interact with item");
    }


}
