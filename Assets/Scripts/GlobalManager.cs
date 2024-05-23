using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalManager : MonoBehaviour
{
    private static GlobalManager _instance;
    private List<InteractableParameters> _interactionParameters;
    private List<PaintingConditions> _paintingConditions;
    private Dictionary<int, bool> completedPaintings = new Dictionary<int, bool>();
    private Dictionary<InteractableType, IInteractable> pickedUpItems = new Dictionary<InteractableType, IInteractable>();

    [SerializeField] private PlayerInteractUI playerInteractUI;

    public Action<int> FinishRestoration;
    public List<PaintingBehaviour> paintings;

    // Public property to access the singleton instance
    public static GlobalManager Instance
    {
        get
        {
            // If the instance hasn't been set yet, try to find it in the scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<GlobalManager>();

                // If it's still null, create a new GameObject and attach the singleton script to it
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("GlobalManager");
                    _instance = managerObject.AddComponent<GlobalManager>();
                }
            }

            return _instance;
        }
    }

    public PlayerInteractUI PlayerInteractUI { get => playerInteractUI; }

    private void Awake()
    {
        // Ensure there's only one instance of the singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        LoadConfigurations();
    }

    private void LoadConfigurations()
    {
        _interactionParameters = Resources.Load<InteractableParametersSO>("Configuration/InteractableParameters").interactableParameters;
        _paintingConditions = Resources.Load<CompletionConditions>("Configuration/CompletionConditions").completionConditions;
    }

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public InteractableParameters GetInteractionParameters(InteractableType interactableType)
    {
        foreach (var parameter in _interactionParameters)
        {
            if (interactableType == parameter.interactableIdentifier)
            {
                return parameter;
            }
        }

        return null;
    }

    public List<PaintingConditions> GetPaintingConditions(int paintingID)
    {
        List<PaintingConditions> conditions = new List<PaintingConditions>();

        foreach (var condition in _paintingConditions)
        {
            if (paintingID == condition.paintingID)
            {
                conditions.Add(condition);
            }
        }

        return conditions;
    }

    public void PaintingStatusChange(int paintingID, bool restoredStatus) //Maybe change name later if something fits better
    {
        Debug.Log($"Painting {paintingID} status is updating!");

        if (completedPaintings.ContainsKey(paintingID))
        {
            completedPaintings[paintingID] = restoredStatus; //Theres probably a bug here, we need to fix when the condition is no longer completed

            Debug.Log($"Painting {paintingID} status updated!. Status {restoredStatus}");
        }
        else
        {
            completedPaintings.Add(paintingID, restoredStatus);

            Debug.Log($"Painting {paintingID} was added to completed paintings list!. Status {restoredStatus}");

            //TODO: Make it work for both notepad sides in the future, depending on which page the painting is on.
            ((NotepadBehavior)GetFromInventory(InteractableType.Notepad)).SetPageComplete(PageSide.RIGHTPAGE, restoredStatus); 
        }

        if (restoredStatus)
        {
            TriggerFinishRestoration(paintingID);
        }
    }

    public void PaintingConditionCompletion(int paintingID, string itemPlacedName)
    {
        foreach (PaintingBehaviour painting in paintings)
        {
            if (painting.PaintingID == paintingID)
            {
                painting.UpdateRestorationCondition(itemPlacedName);
            }
        }
    }

    public void TriggerFinishRestoration(int paintingID)
    {
        FinishRestoration?.Invoke(paintingID);
    }

    public void AddToInventory(InteractableType inventoryItem, IInteractable item)
    {
        pickedUpItems.Add(inventoryItem, item);
    }

    public IInteractable GetFromInventory(InteractableType inventoryItem)
    {
        IInteractable interactable = null;
        pickedUpItems.TryGetValue(inventoryItem, out interactable);

        return interactable;
    }
}
