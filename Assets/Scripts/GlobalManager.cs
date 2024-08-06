using System;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Utils;

public class GlobalManager : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private StarterAssetsInputs starterAssetsInput;

    [SerializeField] private PlayerInteractUI playerInteractUI;
    [SerializeField] private CamerasController camerasController;

    [SerializeField] private GameObject globalSpawnPoint;

    private static GlobalManager _instance;
    private List<InteractableParameters> _interactionParameters;
    private List<PaintingConditions> _paintingConditions;
    private Dictionary<int, bool> completedPaintings = new Dictionary<int, bool>();
    private Dictionary<InteractableType, IInteractable> pickedUpItems = new Dictionary<InteractableType, IInteractable>();

    private Color currentPaletteColor1 = new Color();
    private Color currentPaletteColor2 = new Color();
    private Color currentPaletteColor3 = new Color();
    private Color currentPaletteColorResult = new Color();
    private ColorType currentColorType = new ColorType();

    //This could be better
    public Color CurrentPaletteColor1 { get => currentPaletteColor1; set => currentPaletteColor1 = value; }
    public Color CurrentPaletteColor2 { get => currentPaletteColor2; set => currentPaletteColor2 = value; }
    public Color CurrentPaletteColor3 { get => currentPaletteColor3; set => currentPaletteColor3 = value; }
    public Color CurrentPaletteColorResult { get => currentPaletteColorResult; set => currentPaletteColorResult = value; }
    public ColorType CurrentColorType { get => currentColorType; set => currentColorType = value; }

    public PlayerInteractUI PlayerInteractUI { get => playerInteractUI; }
    public CamerasController CamerasController { get => camerasController; }
    public GameObject GlobalSpawnPoint { get => globalSpawnPoint; set => globalSpawnPoint = value; }

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
        Application.targetFrameRate = 120;
    }

    public void EnableCharacterController(bool enable)
    {
        if (characterController == null)
            return;

        characterController.enabled = enable;
    }

    public void EnableFirstPersonController(bool enable)
    {
        if (firstPersonController == null)
            return;

        firstPersonController.enabled = enable;
    }

    public void EnableStaticCursor(bool enable)
    {
        starterAssetsInput.cursorInputForLook = enable;
    }

    public void SetCursorLockState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
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
        }

        if (restoredStatus)
        {
            TriggerFinishRestoration(paintingID);
            //TODO: Make it work for both notepad sides in the future, depending on which page the painting is on.
            ((NotepadBehavior)GetFromInventory(InteractableType.Notepad)).SetPageComplete(PageSide.RIGHTPAGE, restoredStatus);
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
