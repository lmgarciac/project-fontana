using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    private static GlobalManager _instance;
    private List<InteractableParameters> _interactionParameters;
    private Dictionary<int, bool> completedPaintings = new Dictionary<int, bool>();
       
    [SerializeField] private PlayerInteractUI playerInteractUI;

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

    public PlayerInteractUI PlayerInteractUI { get => playerInteractUI;}

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

    public void PaintingStatusChange(int paintingID, bool restoredStatus) //Maybe change name later if something fits better
    {
        Debug.Log($"Painting {paintingID} status is updating!");

        if (completedPaintings.ContainsKey(paintingID))
        {
            completedPaintings[paintingID] = restoredStatus;

            Debug.Log($"Painting {paintingID} status updated!");
        }
        else
        {
            completedPaintings.Add(paintingID, restoredStatus);

            Debug.Log($"Painting {paintingID} was added to completed paintings list!");
        }
    }

    public void PaintingConditionCompletion(int paintingID, RestorationCondition restorationCondition)
    {
        foreach (PaintingBehaviour painting in paintings)
        {
            if (painting.PaintingID == paintingID)
            {
                painting.UpdateRestorationCondition(restorationCondition);
            }
        }
    }
}
