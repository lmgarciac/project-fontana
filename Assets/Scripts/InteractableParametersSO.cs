using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InteractableType
{
    Bed = 0,
    Telescope = 1,
    RecliningChair = 2,
    StoragePainting = 3,
    StickyNotesBoard = 4,
    Notepad = 5,
    Stranger = 6,
    PaintingTheBedroom = 7,
    PaintingTheTree = 8,
}

[CreateAssetMenu(fileName = "InteractableParameters", menuName = "Interactable Parameters", order = 51)]
public class InteractableParametersSO : ScriptableObject
{
    public List<InteractableParameters> interactableParameters;
}

[Serializable]
public class InteractableParameters
{
    public InteractableType interactableIdentifier;
    public string interactableDescription;
    public List<string> interactableDialogue;
    public List<string> pickupDialogue;
    public bool isInventoryItem;
}