using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaintingBehaviour : InteractablePickable
{
    [SerializeField]
    private int paintingID;
    [SerializeField] 
    private List<PaintingConditions> paintingConditions;

    private CompletionConditions completionConditions;
    private bool allConditionsMet;

    public int PaintingID { get => paintingID; set => paintingID = value; }

    public override void Start()
    {
        base.Start();
        paintingConditions = GlobalManager.Instance.GetPaintingConditions(paintingID); //This gets the painting conditions for the painting duh
    }

    public void UpdateRestorationCondition(string itemPlacedName) //Rework this later to implement dialogue choices
    {
        allConditionsMet = true;

        foreach (var condition in paintingConditions)
        {
            if (condition.itemToPlace == itemPlacedName)
            {
                condition.completedCondition = true;
            }

            if (!condition.completedCondition)
            {
                allConditionsMet = false;
            }
        }

        UpdatePainting(allConditionsMet);
    }

    private void UpdatePainting(bool completionStatus)
    {
        //Sets the painting status in global manager
        GlobalManager.Instance.PaintingStatusChange(PaintingID, completionStatus);
    }
}
