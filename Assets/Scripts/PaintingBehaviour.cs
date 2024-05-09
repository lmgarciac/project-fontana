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
    private RestorationCondition[] completionConditions;

    private Dictionary<int, bool> restorationConditions = new Dictionary<int, bool>();

    public int PaintingID { get => paintingID; set => paintingID = value; }

    protected override void Start()
    {
        base.Start();
        foreach (RestorationCondition condition in completionConditions)
        {
            restorationConditions.Add(condition.conditionID, condition.conditionCompleted);
        }
    }

    public void UpdateRestorationCondition(RestorationCondition restorationCondition)
    {
        restorationConditions[restorationCondition.conditionID] = restorationCondition.conditionCompleted;
        Debug.Log($"Condition {restorationCondition.conditionID} updated!");

        foreach (var condition in restorationConditions)
        {
            if (condition.Value != true)
            {
                UpdatePainting(false);
                return;
            }
        }
        UpdatePainting(true); //All this has to be refactored into better, more tidied up code
    }

    private void UpdatePainting(bool completionStatus)
    {
        //Sets the painting status in global manager
        GlobalManager.Instance.PaintingStatusChange(PaintingID, completionStatus);
        Debug.Log($"Painting {PaintingID} status updated!");
    }
}
