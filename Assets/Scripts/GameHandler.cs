using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler: MonoBehaviour
{

    // TA BORT ID-LISTOR, LÄGG TILL "CURRENTLY SELECTED CONTAINER" osv

    private int ContainerID = 0;
    public List<ContainerData> allContainers;
    public ContainerData playerInventory;

    public DebugScript debugScript;
    public UIHandler uinterfaceHandler;

    public List<ItemData> muhItems;
    public ItemData potion01;
    private ItemData tempItem;

    public void Awake()
    {
        allContainers = new List<ContainerData>();
        CreatePlayerInventory("Player Inventory", 7);
        CreateInventory("The Ground", 9);
        LoadItems();
        SpawnItem();
        SpawnItem();
        SpawnItem();
    }

    public void Start()
    {
        UIHandlerThing();
        UIHandlerThing1();
    }


    private void CreatePlayerInventory(string str, int capacity)
    {
        playerInventory = new ContainerData(ContainerID, str, capacity);
        allContainers.Add(playerInventory);
        ContainerID++;
    }

    private void CreateInventory(string str, int capacity)
    {
        allContainers.Add(new ContainerData(ContainerID, str, capacity));
        ContainerID++;
    }

    public void UIHandlerThing()
    {
        uinterfaceHandler.CreateContainerUIElement(allContainers[0], -200, -200);
    }
    public void UIHandlerThing1()
    {
        uinterfaceHandler.CreateContainerUIElement(allContainers[1], 200, -200);
    }

    private void LoadItems()
    {
        muhItems = new List<ItemData>();
        foreach (ItemData item in Resources.LoadAll("Scriptable Objects/Potions/"))
        {
            muhItems.Add(item);
        }
    }

    private ItemData GetRandomItem()
    {
        return muhItems[Random.Range(0, muhItems.Count)];
    }

    public void SpawnItem()
    {
        //tempItem = GetRandomItem().GetClone();
        tempItem = potion01.GetClone();
        Debug.Log("Spawned " + tempItem.itemName);
        AddItemToContainer(tempItem, allContainers[0]);
        tempItem = null;
    }

    public void AddItemToContainer(ItemData item, ContainerData targetContainer)
    {

        if (targetContainer.items.Count < targetContainer.maxCapacity)
        {
            targetContainer.items.Add(item);
            tempItem.currentContainer = targetContainer;
            Debug.Log(tempItem.currentContainer.containerName);

            Debug.Log("Added an/a " + item.itemName +
                        " to container " + targetContainer.containerName);
        }
        else
        {
            Debug.Log("Can't add item to container " + targetContainer.containerName +
                        " it is already at max capacity!");
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
