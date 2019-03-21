using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ClickableElement : EventTrigger
{

    public override void OnPointerClick(PointerEventData eventData)
    {
        
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                if (eventData.clickCount == 2)
                {
                    DoubleClick(eventData);
                }
                else
                {
                    LeftClick(eventData);
                }
                break;
            case PointerEventData.InputButton.Right:
                RightClick(eventData);
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                break;
        }

    }
    public abstract void LeftClick(PointerEventData data);
    public abstract void RightClick(PointerEventData data);
    public abstract void DoubleClick(PointerEventData data);

    public override void OnPointerDown(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                LeftDown(eventData);
                break;
            case PointerEventData.InputButton.Right:
                RightDown(eventData);
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                break;
        }

    }
    public abstract void LeftDown(PointerEventData data);
    public abstract void RightDown(PointerEventData data);

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Enter(eventData);
    }
    public abstract void Enter(PointerEventData data);


    public override void OnPointerExit(PointerEventData eventData)
    {
        Exit(eventData);
    }
    public abstract void Exit(PointerEventData data);


    public override void OnPointerUp(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                LeftUp(eventData);
                break;
            case PointerEventData.InputButton.Right:
                RightUp(eventData);
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                break;
        }
    }
    public abstract void LeftUp(PointerEventData data);
    public abstract void RightUp(PointerEventData data);


    public override void OnDrag(PointerEventData eventData)
    {
        Drag(eventData);
    }
    public abstract void Drag(PointerEventData data);


}
