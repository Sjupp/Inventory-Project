using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Meta")]
    public string itemName;
    public int itemID;

    [Header("Physical")]
    public float weight;
    public int value;
    public int stackValue;
    public int maxStackCount;

    public abstract ItemData GetClone();
}
