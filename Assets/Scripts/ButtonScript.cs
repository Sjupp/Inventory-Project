﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameHandler main;

    ContainerData playerContainer;
    ContainerData selectedContainer;
    public ItemData tempSelect;
    ItemData currentlySelectedItem;

    public void Start()
    {
        currentlySelectedItem = tempSelect;
        playerContainer = main.allContainers[1];
        selectedContainer = main.allContainers[2];
    }

    public void MoveObject(ItemData item, ContainerData from, ContainerData to)
    {
        if (from.items.Count != 0)
        {
            ItemData tempItem = findItemInContainer(item, from);

            if (to.items.Count < to.maxCapacity)
            {
                to.items.Add(tempItem.GetClone());
                from.items.RemoveAt(0);
                Debug.Log("Moved " + tempItem.itemName +
                          " from " + from.containerName +
                          " to " + to.containerName);
            }
            else
            {
                Debug.Log(to.containerName + " is full!");
            }
        }
        else
        {
            Debug.Log(from.containerName + " is empty!");
        }
    }

    private ItemData findItemInContainer(ItemData item, ContainerData container)
    {
        foreach (ItemData itm in container.items)
        {
            if (itm.itemID == item.itemID)
            {
                return itm;
            }
        }
        Debug.Log("Found no " + item.itemName + " in " + container.containerName);
        return null;
    }

    public void PickUpObject() //pick up
    {
        MoveObject(tempSelect, selectedContainer, playerContainer);
    }

    public void DropObject() // drop
    {
        MoveObject(tempSelect, playerContainer, selectedContainer);
    }

    public void DebugContainers()
    {
        Debug.Log("Total number of containers " + main.allContainers.Count);
        foreach (ContainerData container in main.allContainers)
        {
            Debug.Log(container.containerName + " has " + container.items.Count + " items stored:");
            foreach (ItemData item in container.items)
            {
                Debug.Log(item.itemName);
            }
        }
    }

    public void SpawnItem()
    {
        main.SpawnItem();
    }

    public void AddItemToGround()
    {
        Debug.Log((playerContainer.items[0] as Potion).restoreAmount);
        //Debug.Log(playerContainer.items[0].restoreAmount);
    }
}