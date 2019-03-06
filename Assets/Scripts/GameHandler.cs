using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler: MonoBehaviour
{

    // TA BORT ID-LISTOR, LÄGG TILL "CURRENTLY SELECTED CONTAINER" osv

    private int ContainerID = 0;
    public List<ContainerData> allContainers;
    public DebugScript debugScript;
    //public UIHandler uiHandler;

    public ItemData potion01;
    private ItemData tempItem;

    public void Awake()
    {
        allContainers = new List<ContainerData>();
        CreateInventory("N/A", 0);
        CreateInventory("Player Inventory", 7);
        CreateInventory("The Ground", 9);
    }

    private void CreateInventory(string str, int capacity)
    {
        ContainerData container = new ContainerData(ContainerID, str, capacity);
        allContainers.Add(container);
        ContainerID++;
        //AddDebugButton(str, container);
    }

    //private void AddDebugButton(string str, ContainerData container)
    //{
    //    debugScript.AddButton(str, container);
    //}

    public void SpawnItem()
    {
        tempItem = potion01.GetClone();
        Debug.Log("Spawned " + tempItem.itemName);
        AddItemToContainer(tempItem, 1);
        tempItem = null;
    }

    public void AddItemToContainer(ItemData item, int id)
    {
        ContainerData targetContainer = FindContainer(id);
        if (targetContainer.containerID >= 1)
        {
            if (targetContainer.items.Count < targetContainer.maxCapacity)
            {
                targetContainer.items.Add(item);
                Debug.Log("Added an/a " + item.itemName +
                          " to container " + targetContainer.containerName);
            }
            else
            {
                Debug.Log("Can't add item to container " + targetContainer.containerName +
                          " it is already at max capacity!");
            }
        }

    }

    public ContainerData FindContainer(int containerID)
    {
        foreach (ContainerData container in allContainers)
        {
            if (containerID == container.containerID)
            {
                return container;
            }
        }
        Debug.Log("No valid containerID match! Returning bad container");
        return allContainers[0]; //pls lemme null
    }
}
