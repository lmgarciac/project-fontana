using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CompletionConditions", menuName = "Completion Conditions", order = 52)]
public class CompletionConditions : ScriptableObject
{
    public List<PaintingConditions> completionConditions;
}

[Serializable]
public class PaintingConditions
{
    public int paintingID;
    public string paintingName;
    public ConditionType conditionType; //Need to think how can we implement dialogue choices into this
    public string conditionDescription;
    public string itemToPlace;
    public bool completedCondition;
}

public enum ConditionType
{
    Items = 0,
    Dialogue = 1,
}
