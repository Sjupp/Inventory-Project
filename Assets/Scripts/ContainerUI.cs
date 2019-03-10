using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ContainerUI : MonoBehaviour
{
    public ContainerData containerData;
    private GameObject[] slots;

    public GameObject slotPrefab;

    public void InitializeContainer(ContainerData container, int x, int y, UIHandler handler)
    {
        this.containerData = container;

        UpdateUIFrameSize();
        CreateSlots(handler);
        UpdateSlots(container);
        GetComponent<RectTransform>().anchoredPosition =
            new Vector2(x, y);
    }

    private void UpdateUIFrameSize()
    {
        int numberOfRows = Mathf.CeilToInt((containerData.maxCapacity - 1) / 4);
        GetComponent<RectTransform>().sizeDelta =
            new Vector2(215, 65 + (50 * numberOfRows));
    }

    private void CreateSlots(UIHandler handler)
    {
        slots = new GameObject[containerData.maxCapacity];
        for (int i = 0; i < containerData.maxCapacity; i++)
        {
            slots[i] = Instantiate<GameObject>(slotPrefab, transform);
            var slot = slots[i].GetComponent<SlotScript>();
            slot.ParentContainer = this;
            slot.SlotID = i;

            var button = slots[i].GetComponent<CustomButton>();
            button.enter.AddListener(delegate { handler.SlotEnter(this, slot); });
            button.down.AddListener(delegate { handler.SlotDown(this, slot); });
            button.up.AddListener(delegate { handler.SlotUp(this, slot); });
            button.exit.AddListener(delegate { handler.SlotExit(this, slot); });
        }
    }

    public void UpdateSlots(ContainerData container)
    {
        this.containerData = container;
        int i = 0;
        foreach (GameObject slot in slots)
        {
            if (i < container.items.Count)
            {
                slots[i].GetComponent<SlotScript>().UpdateSlot(container.items[i]);
            }
            else
            {
                slots[i].GetComponent<SlotScript>().UpdateSlot();
            }
            i++;
        }
    }

}
