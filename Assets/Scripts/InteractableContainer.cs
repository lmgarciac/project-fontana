using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class InteractableContainer : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string interactableNameExpected;
    [SerializeField]
    private string interactablePrompt;
    [SerializeField]
    private bool activatesAlternativeMesh;

    //This should be if the container works as a restoring condition. Use EDITORGUI later so this looks much better
    [SerializeField]
    private bool activatesCompletionCondition;
    [SerializeField]
    private int paintingID;
    [SerializeField]
    private int conditionID;

    private bool isPickableObject;
    private bool isContainerObject;

    private GameObject interactableGameObject;
    private GameObject containedGameObject;

    public GameObject InteractableGameObject { get => interactableGameObject; }
    public string InteractablePrompt { get => interactablePrompt;}
    public bool IsPickableObject { get => isPickableObject;}
    public bool IsContainerObject { get => isContainerObject; }
    public bool ActivatesAlternativeMesh { get => activatesAlternativeMesh;}
    public GameObject ContainedGameObject { get => containedGameObject; }
    public string InteractableNameExpected { get => interactableNameExpected; }

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

        ChangeObjectMesh(returnContained.GetComponent<InteractablePickable>(), false);

        return returnContained;
    }

    public void PlaceInside(GameObject objectToPlace)
    {
        Debug.Log($"Place {objectToPlace.name} inside container {this.name}");

        if (objectToPlace.GetComponent<InteractablePickable>().InteractableName != interactableNameExpected) //There are more performant options for sure
        {
            Debug.LogError("THIS ITEM CANT BE PLACED HERE!!!");
            return;
        }

        if (activatesCompletionCondition)
        {
            RestorationCondition resCondition = new RestorationCondition();
            resCondition.conditionID = conditionID;
            resCondition.conditionCompleted = true;

            GlobalManager.Instance.PaintingConditionCompletion(paintingID, resCondition); //I dont like this, change it later
        }

        objectToPlace.GetComponent<Collider>().enabled = false;
        objectToPlace.transform.parent = transform;
        objectToPlace.transform.localPosition = Vector3.zero;
        objectToPlace.transform.localRotation = Quaternion.identity;

        ChangeObjectMesh(objectToPlace.GetComponent<InteractablePickable>(), activatesAlternativeMesh);

        containedGameObject = objectToPlace;
    }

    public GameObject Replace(GameObject objectToPlace, Transform playerHand)
    {
        Debug.Log($"Replace {containedGameObject.name} inside container {this.name} with {objectToPlace.name}");

        GameObject returnContained = containedGameObject;
        containedGameObject.transform.parent = playerHand;
        containedGameObject.transform.localPosition = Vector3.zero;
        containedGameObject.transform.localRotation = Quaternion.identity;

        PlaceInside(objectToPlace);
        
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
