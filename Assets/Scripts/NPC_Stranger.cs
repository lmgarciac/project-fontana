using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Stranger : InteractableObject
{
    public override void Start()
    {
        base.Start();
        GlobalManager.Instance.FinishRestoration += OnFinishRestoration;
    }

    public override void Interact()
    {
        base.Interact();
        Debug.LogError("INTERACT STRANGER");
    }

    private void OnFinishRestoration(int paintingID)
    {
        if (paintingID != 1) //First painting, "The Bedroom". Check if we need the whole PaintingBehavior later
        {
            return;
        }

        this.gameObject.transform.GetChild(0).gameObject.SetActive(true); //Dont like how this is done, will refactor later
        this.gameObject.GetComponent<BoxCollider>().enabled = true;

        Debug.Log($"Showing the Stranger...");
    }
}
