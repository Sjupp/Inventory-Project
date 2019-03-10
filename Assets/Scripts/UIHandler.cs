using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject containerPrefab;
    public List<ContainerUI> listOfActiveContainers;

    public ContainerData CurrentlySelectedContainer { get; set; }
    public ItemData CurrentlySelectedItem { get; set; }

    public void CreateContainerUIElement(ContainerData container, int x, int y)
    {
        GameObject containerObject = Instantiate<GameObject>(containerPrefab, transform);
        listOfActiveContainers.Add(containerObject.GetComponent<ContainerUI>());
        containerObject.GetComponent<ContainerUI>().InitializeContainer(container, x, y, this);
        containerObject.name = container.containerName;
    }

    public void UpdateContainer(ContainerData one, ContainerData two)
    {
        if (FindContainerDataInContainer(one) != null) 
        {
            FindContainerDataInContainer(one).UpdateSlots(one);
        }

        if (FindContainerDataInContainer(two) != null)
        {
            FindContainerDataInContainer(two).UpdateSlots(two);
        }
    }

    //public void ToggleContainer()
    //{

    //}

    public void SlotEnter(ContainerUI container, SlotScript slot)
    {
        //Debug.Log("");
    }

    public void SlotDown(ContainerUI container, SlotScript slot)
    {
        //Debug.Log("2");
    }

    public void SlotUp(ContainerUI container, SlotScript slot)
    {
        if (slot.slotItem != null)
        {
            Debug.Log("Clicked slot " + slot.SlotNumber + 
                " in " + container.containerData.containerName + ". It contains "
                + slot.slotItem.itemName);
        }
        else
        {
            Debug.Log("Clicked slot " + slot.SlotNumber +
                " in " + container.containerData.containerName + ". It contains no item!");
        }
    }

    public void SlotExit(ContainerUI container, SlotScript slot)
    {
        //Debug.Log("4");
    }

    public ContainerUI FindContainerDataInContainer(ContainerData containerData)
    {
        foreach (ContainerUI thisContainer in listOfActiveContainers)
        {
            if (thisContainer.containerData.containerID == containerData.containerID)
            {
                return thisContainer;
            }
        }
        return null;
    }
}
