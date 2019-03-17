using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ContainerData
{
    public int containerID;
    public string containerName;
    public int maxCapacity;
    public List<ItemData> items;
    
    // To prevent accidentally putting stuff in a temp loot window
    //public bool canMoveItemsInto;
    
    public ContainerData(int id, string containerName, int maxC)
    {
        this.containerID = id;
        this.containerName = containerName;
        this.maxCapacity = maxC;
        this.items = new List<ItemData>();
    }
}
