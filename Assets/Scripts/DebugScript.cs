using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
    public GameHandler main;
    public CanvasScript canvas;
    public GameObject buttonPrefab;
    public UIHandler uiHandler;

    private ContainerData playerContainer;
    private ContainerData groundContainer;
    private ItemData currentlySelectedItem;
    public ItemData tempSelect;

    public void Start()
    {
        currentlySelectedItem = tempSelect;
        playerContainer = main.allContainers[1];
        groundContainer = main.allContainers[2];
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
        MoveObject(tempSelect, groundContainer, playerContainer);
    }

    public void DropObject() // drop
    {
        MoveObject(tempSelect, playerContainer, groundContainer);
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

    public void UIHandlerThing()
    {
        uiHandler.CreateContainerUIElement(playerContainer);
    }
    public void UIHandlerThing1()
    {
        uiHandler.CreateContainerUIElement(groundContainer);
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
