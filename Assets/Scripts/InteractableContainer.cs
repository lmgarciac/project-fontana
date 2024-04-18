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
    private GameObject containedGameObject;

    public GameObject InteractableGameObject { get => interactableGameObject; }
    public string InteractablePrompt { get => interactablePrompt;}
    public bool IsPickableObject { get => isPickableObject;}
    public bool IsContainerObject { get => isContainerObject; }
    public GameObject ContainedGameObject { get => containedGameObject; }

    private void Start()
    {
        interactableGameObject = this.gameObject;
        isContainerObject = true;
        isPickableObject = false;
    }

    public string GetInteractPrompt(string objectInHand)
    {
        if (containedGameObject != null)
            return $"PICK UP {containedGameObject.name}";
        else if (objectInHand != null)
            return $"Place {objectInHand}";
        else 
            return null;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public GameObject PickUp(Transform playerHand)
    {
        Debug.Log($"PickUp {containedGameObject.name} from container {this.name}");

        GameObject returnContained = containedGameObject;

        containedGameObject.transform.parent = playerHand;
        containedGameObject.transform.localPosition = Vector3.zero;
        containedGameObject.transform.localRotation = Quaternion.identity;
        
        containedGameObject = null;

        returnContained.GetComponent<Collider>().enabled = true;

        //This is needed for using changing the object mesh while picking it up/placing it down
        ChangeObjectMesh(returnContained.GetComponent<InteractablePickable>(), true);

        return returnContained;
    }

    public void PlaceInside(GameObject objectToPlace)
    {
        Debug.Log($"Place {objectToPlace.name} inside container {this.name}");

        objectToPlace.GetComponent<Collider>().enabled = false;
        objectToPlace.transform.parent = transform;
        objectToPlace.transform.localPosition = Vector3.zero;
        objectToPlace.transform.localRotation = Quaternion.identity;

        //This is needed for using changing the object mesh while picking it up/placing it down
        ChangeObjectMesh(objectToPlace.GetComponent<InteractablePickable>(), false);

        containedGameObject = objectToPlace;
    }

    public GameObject Replace(GameObject objectToPlace, Transform playerHand)
    {
        Debug.Log($"Replace {containedGameObject.name} inside container {this.name} with {objectToPlace.name}");

        GameObject returnContained = containedGameObject;

        objectToPlace.GetComponent<Collider>().enabled = false;
        objectToPlace.transform.parent = transform;
        objectToPlace.transform.localPosition = Vector3.zero;
        objectToPlace.transform.localRotation = Quaternion.identity;

        if (objectToPlace.GetComponent<InteractablePickable>().HasAlternativeMesh)
        {
            objectToPlace.transform.GetChild(0).gameObject.SetActive(true);
            objectToPlace.transform.GetChild(1).gameObject.SetActive(false);
        }

        containedGameObject.transform.parent = playerHand;
        containedGameObject.transform.localPosition = Vector3.zero;
        containedGameObject.transform.localRotation = Quaternion.identity;

        containedGameObject = objectToPlace;
        
        returnContained.GetComponent<Collider>().enabled = true;

        return returnContained;
    }

    public bool IsInteractionPossible(bool objectInHand)
    {
        return (containedGameObject != null || objectInHand == true);
    }

    public void ChangeObjectMesh(InteractablePickable interactable, bool setAlternative)
    {
        if (interactable.HasAlternativeMesh)
        {
            interactable.transform.GetChild(0).gameObject.SetActive(!setAlternative);
            interactable.transform.GetChild(1).gameObject.SetActive(setAlternative);
        }
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
