using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ContainerData
{
    public int containerID;
    public string containerName;
    public int maxCapacity;
    public List<ItemData> items;
    
    public ContainerData(int id, string ctrNm, int maxC)
    {
        this.containerID = id;
        this.containerName = ctrNm;
        this.maxCapacity = maxC;
        this.items = new List<ItemData>();
    }

}
