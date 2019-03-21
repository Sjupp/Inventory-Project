using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OldSlotScript : MonoBehaviour
{
    public ContainerUI ParentContainer { get; set; } // Is set upon creation inside ContainerUI class
    //public ContainerData ParentContainerData { get; set; }
    public int SlotID { get; set; }
    public int SlotNumber { get { return (SlotID + 1); } }

    public ItemData slotItem;

    public void UpdateSlot()
    {
        if (ParentContainer != null)
        {
            if (ParentContainer.containerData.items.Count > SlotID)
            {
                slotItem = ParentContainer.containerData.items[SlotID];
                gameObject.GetComponent<Image>().sprite = slotItem.sprite;
            }
            else
            {
                slotItem = null;
                gameObject.GetComponent<Image>().sprite = null;
            }
        }
    }

}
