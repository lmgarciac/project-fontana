using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotepadBehavior : InteractablePickable, IUsable
{
    [SerializeField] Transform notepad_top;
    [SerializeField] Transform notepad_bottom;
    
    private bool isBeingUsed;
    private Vector3 previousPosition;
    private Quaternion previousRotation;

    public void Use(Transform focusTarget)
    {
        GetComponent<Collider>().enabled = false;

        previousPosition = transform.localPosition;
        previousRotation = transform.localRotation;

        transform.parent = focusTarget;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        notepad_bottom.Rotate(Vector3.forward, 180f);
        isBeingUsed = true;
    }

    public void Unuse(Transform focusTarget)
    {
        transform.parent = focusTarget;

        transform.localPosition = previousPosition;
        transform.localRotation = previousRotation;

        notepad_bottom.Rotate(Vector3.forward, 180f);
        isBeingUsed = false;

        GetComponent<Collider>().enabled = true;

    }

    public bool IsBeingUsed()
    {
        return isBeingUsed;
    }

    public override void SendToInventory()
    {
        base.SendToInventory();
        GlobalManager.Instance.PlayerInteractUI.ShowNotepad(true);
    }
}

