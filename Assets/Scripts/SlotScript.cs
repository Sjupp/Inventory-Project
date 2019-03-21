using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotScript : ClickableElement
{
    public int slotIndex;
    public ContainerData containerData;

    public override void DoubleClick(PointerEventData data)
    {
    }

    public override void Drag(PointerEventData data)
    {
        if (data.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left Click Dragging");
        }
    }

    public override void Enter(PointerEventData data)
    {
        NewInventoryManager.Instance.SlotEnter(this);
    }

    public override void Exit(PointerEventData data)
    {
        NewInventoryManager.Instance.SlotExit(slotIndex, containerData);
    }

    public override void LeftClick(PointerEventData data)
    {
        NewInventoryManager.Instance.SlotLeftClick(this, slotIndex);
    }

    public override void LeftDown(PointerEventData data)
    {
    }

    public override void LeftUp(PointerEventData data)
    {
    }

    public override void RightClick(PointerEventData data)
    {
        NewInventoryManager.Instance.SlotRightClick(this, slotIndex);
    }

    public override void RightDown(PointerEventData data)
    {
    }

    public override void RightUp(PointerEventData data)
    {
    }

}
