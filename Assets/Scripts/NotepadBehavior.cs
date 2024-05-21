using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PageSide
{
    LEFTPAGE = 0,
    RIGHTPAGE = 1,
}

public class NotepadBehavior : InteractablePickable, IUsable
{
    [Header("Transforms")]
    [SerializeField] Transform notepad_top;
    [SerializeField] Transform notepad_bottom;

    [Header("Pages UI")]
    [SerializeField] GameObject checkMarkLeft;
    [SerializeField] GameObject checkMarkRight;

    private bool isBeingUsed;
    private Vector3 previousPosition;
    private Quaternion previousRotation;

    public void Use(Transform focusTarget)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        GetComponent<Collider>().enabled = false;

        previousPosition = transform.localPosition;
        previousRotation = transform.localRotation;

        transform.parent = focusTarget;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        notepad_bottom.Rotate(Vector3.forward, 180f);
        isBeingUsed = true;

        GlobalManager.Instance.PlayerInteractUI.ShowNotepadIcon(false);
    }

    public void Unuse(Transform focusTarget)
    {
        transform.parent = focusTarget;

        transform.localPosition = previousPosition;
        transform.localRotation = previousRotation;

        notepad_bottom.Rotate(Vector3.forward, 180f);
        isBeingUsed = false;

        GetComponent<Collider>().enabled = true;

        if (gameObject.activeSelf)
            gameObject.SetActive(false);

        GlobalManager.Instance.PlayerInteractUI.ShowNotepadIcon(true);

    }

    public bool IsBeingUsed()
    {
        return isBeingUsed;
    }

    public override void SendToInventory()
    {
        base.SendToInventory();
        GlobalManager.Instance.PlayerInteractUI.ShowNotepadIcon(true);
    }

    public void SetPageComplete(PageSide pageSide, bool complete)
    {
        if (pageSide == PageSide.LEFTPAGE) 
            checkMarkLeft.SetActive(complete);
        else
            checkMarkRight.SetActive(complete);
    }
}

