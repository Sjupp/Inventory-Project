using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameHandler gameHandler;
    public GameObject containerPrefab;
    public List<ContainerUI> listOfActiveContainers;

    private ItemData CurrentlySelectedItem { get; set; }

    public void CreateContainerUIElement(ContainerData container, int x, int y)
    {
        GameObject containerObject = Instantiate<GameObject>(containerPrefab, transform);
        listOfActiveContainers.Add(containerObject.GetComponent<ContainerUI>());
        containerObject.GetComponent<ContainerUI>().InitializeContainer(container, x, y, this);
        containerObject.name = container.containerName;
    }

    //public void ToggleContainer()
    //{

    //}

    #region ButtonEvents

    public void SlotEnter(ContainerUI container, SlotScript slot)
    {
        //Debug.Log("");
        slot.UpdateSlot();
    }

    public void SlotDown(ContainerUI container, SlotScript slot)
    {
        //Debug.Log("2");
    }

    public void SlotUp(ContainerUI container, SlotScript slot)
    {

        if (slot.slotItem != null)
        {
            //Debug.Log("Clicked slot " + slot.SlotNumber + 
            //    " in " + container.containerData.containerName + ". It contains "
            //    + slot.slotItem.itemName);

            if (container.containerData.containerID == gameHandler.currentlySelectedContainer.containerID)
                MoveObjectInSlot(slot, slot.slotItem.currentContainer, gameHandler.playerInventory); //pick up
            else
                MoveObjectInSlot(slot, slot.slotItem.currentContainer, gameHandler.currentlySelectedContainer); //drop

            //CurrentlySelectedItem = slot.slotItem;
            //Debug.Log("curSelItem: " + CurrentlySelectedItem.itemName);

        }
        else
        {
            Debug.Log("Clicked slot " + slot.SlotNumber +
                " in " + container.containerData.containerName + ". It contains no item!");

            //if (slot.slotItem != CurrentlySelectedItem)
            //{
            //    Debug.Log("Theoretically moving " + CurrentlySelectedItem.itemName + " from "
            //              + CurrentlySelectedItem.currentContainer.containerName + " to slot " + slot.SlotNumber + " in " + container.containerData.containerName);
            //    CurrentlySelectedItem = null;
            //}
        }
    }

    public void SlotExit(ContainerUI container, SlotScript slot)
    {
        //Debug.Log("4");
    }

    #endregion

    public void MoveObjectInSlot(SlotScript slot, ContainerData from, ContainerData to)
    {
        if (from.items.Count != 0)
        {
            if (to.items.Count < to.maxCapacity)
            {
                //Add clone and update its currentContainer
                ItemData clone = slot.slotItem.GetClone();
                clone.currentContainer = to;
                to.items.Add(clone);

                //Update new container slots
                // ???

                //Remove item from its old container
                from.items.RemoveAt(slot.SlotID);

                Debug.Log("Moved " + slot.slotItem.itemName +
                          " from " + from.containerName +
                          " to " + to.containerName);
                //Update old container slots
                slot.ParentContainer.UpdateAllSlots();


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

}
