using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlotScript : MonoBehaviour
{
    public ContainerUI ParentContainer { get; set; }
    //public ContainerData ParentContainerData { get; set; }
    public int SlotID { get; set; }
    public int SlotNumber { get { return (SlotID + 1); } }

    public ItemData slotItem;

    public void UpdateSlot(ItemData item)
    {
        slotItem = item;
        gameObject.GetComponent<Image>().sprite = item.sprite;
    }

    public void UpdateSlot()
    {
        slotItem = null;
        gameObject.GetComponent<Image>().sprite = null;
    }

}
