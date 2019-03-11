using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Meta")]
    public string itemName;
    public int itemID;
    public string description = "No Description";
    public Sprite sprite;
    public ContainerData currentContainer;
    public SlotScript currentSlot; // probably unused ???

    [Header("Physical")]
    public float weight;

    [Header("Store Related")]
    public int value;

    [Header("Stackable")]
    public int stackCount = 1;
    public int stackMaxCount = 1;



    public abstract ItemData GetClone();
}
