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
        UpdateAllSlots();
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
            var slot = slots[i].GetComponent<OldSlotScript>();
            slot.ParentContainer = this;
            slot.SlotID = i;

            var button = slots[i].GetComponent<CustomButton>();
            button.enter.AddListener(delegate { handler.SlotEnter(this, slot); });
            button.downLeft.AddListener(delegate { handler.SlotDown(this, slot); });
            button.upLeft.AddListener(delegate { handler.SlotUp(this, slot); });
            button.exit.AddListener(delegate { handler.SlotExit(this, slot); });
        }
    }

    public GameObject GetSlot(int x)
    {
        return slots[x];
    }

    public void UpdateAllSlots()
    {
        foreach (var item in slots)
        {
            item.GetComponent<OldSlotScript>().UpdateSlot();
        }
    }

    public void UpdateOneSlot(int slotID)
    {
        slots[slotID].GetComponent<OldSlotScript>().UpdateSlot();
    }

}
