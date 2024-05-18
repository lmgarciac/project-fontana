using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Stranger : InteractableObject
{
    public override void Start()
    {
        base.Start();
        Debug.LogError("START STRANGER");
        Debug.LogError($"{IsPickableObject} {InteractableParameters.interactableIdentifier} {InteractableParameters.interactableDescription}");
    }

    public override void Interact()
    {
        Debug.LogError("INTERACT STRANGER");
    }
}
