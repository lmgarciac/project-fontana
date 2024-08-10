using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeController : InteractableObject, IUsable
{
    [SerializeField] SkyController skyController;

    private bool isBeingUsed = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GlobalManager.Instance.CamerasController.ToggleCamera();
            skyController.EnableSkyView(false);
            isBeingUsed = false;
        }
    }

    public override void Interact()
    {
        base.Interact();
        GlobalManager.Instance.CamerasController.ToggleCamera();
        skyController.EnableSkyView(true);

        isBeingUsed = true;
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
