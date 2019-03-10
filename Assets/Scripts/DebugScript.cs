using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
    public GameHandler gameHandler;
    public CanvasScript canvas;
    public GameObject buttonPrefab;
    public UIHandler uiHandler;

    private ContainerData playerContainer;
    private ContainerData currentlySelectedContainer;
    private ItemData currentlySelectedItem;
    public ItemData tempSelect;

    public void Start()
    {
        currentlySelectedItem = tempSelect;
        playerContainer = gameHandler.allContainers[0];
        currentlySelectedContainer = gameHandler.allContainers[1];
    }

    public void MoveObject(ItemData item, ContainerData from, ContainerData to)
    {
        if (from.items.Count != 0)
        {
            ItemData tempItem = FindItemInContainer(item, from);

            if (to.items.Count < to.maxCapacity)
            {
                to.items.Add(tempItem.GetClone());
                from.items.RemoveAt(0);
                Debug.Log("Moved " + tempItem.itemName +
                          " from " + from.containerName +
                          " to " + to.containerName);
                uiHandler.UpdateContainer(from, to);
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

    public void MoveItemTo(ItemData item, ContainerData targetContainer)
    {
        Debug.Log(item);
        Debug.Log(item.currentContainer.containerName);
        ItemData tempItem = FindItemInContainer(item, item.currentContainer);

        if (targetContainer.items.Count < targetContainer.maxCapacity)
        {
            targetContainer.items.Add(tempItem.GetClone());
            item.currentContainer.items.RemoveAt(0);
            Debug.Log("Moved " + tempItem.itemName +
                        " from " + item.currentContainer.containerName +
                        " to " + targetContainer.containerName);
            uiHandler.UpdateContainer(item.currentContainer, targetContainer);
            item.currentContainer = targetContainer;
        }
        else
        {
            Debug.Log(targetContainer.containerName + " is full!");
        }
    }

    private ItemData FindItemInContainer(ItemData item, ContainerData container)
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
        currentlySelectedItem = gameHandler.allContainers[0].items[0];//wtf have I done
        //MoveObject(tempSelect, groundContainer, playerContainer);
        MoveItemTo(currentlySelectedItem, playerContainer);
    }

    public void DropObject() // drop
    {
        //MoveObject(tempSelect, playerContainer, groundContainer);
        MoveItemTo(currentlySelectedItem, currentlySelectedContainer);
    }

    public void DebugContainers()
    {
        Debug.Log("Total number of containers " + gameHandler.allContainers.Count);
        foreach (ContainerData container in gameHandler.allContainers)
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
        gameHandler.SpawnItem();
    }

    public void UIHandlerThing()
    {
        uiHandler.CreateContainerUIElement(playerContainer, -200, -200);
    }
    public void UIHandlerThing1()
    {
        uiHandler.CreateContainerUIElement(currentlySelectedContainer, 200, -200);
    }

    // *** Cool example of Listener stuff + delegate ***
    //
    //private void SpawnContainerUIButton(ContainerData container)
    //{
    //    canvas.InstantiateContainerUI(container);
    //}

    //public void RemoveContainerUIButton()
    //{
    //    canvas.RemoveContainerUI();
    //}

    //public void AddButton(string text, ContainerData containerData)
    //{
    //    GameObject button = Instantiate<GameObject>(buttonPrefab, transform);
    //    button.GetComponentInChildren<Text>().text = text;
    //    button.GetComponent<Button>().onClick.AddListener(delegate { SpawnContainerUIButton(containerData); });
    //}
}
