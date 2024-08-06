using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeController : InteractableObject, IUsable
{
    private bool isBeingUsed = false;

    public override void Interact()
    {
        base.Interact();
        GlobalManager.Instance.CamerasController.ToggleCamera();

        isBeingUsed = !isBeingUsed;
    }

    public bool IsBeingUsed()
    {
        return isBeingUsed;
    }

    public void Unuse(Transform focusTarget)
    {
        throw new System.NotImplementedException();
    }

    public void Use(Transform focusTarget)
    {
        throw new System.NotImplementedException();
    }
}
