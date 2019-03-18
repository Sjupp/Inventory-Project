using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewContainerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPrefab;
    private ContainerData containerData;
    List<GameObject> slots = new List<GameObject>();

    public List<GameObject> UpdateContainer(ContainerData containerData)
    {
        this.containerData = containerData;
        UpdateUIFrameSize(containerData.maxCapacity);
        AdjustSlots(containerData.maxCapacity);
        return slots;
    }

    private void UpdateUIFrameSize(int input)
    {
        int numberOfRows = Mathf.CeilToInt((input - 1) / 4);
        GetComponent<RectTransform>().sizeDelta =
            new Vector2(215, 65 + (50 * numberOfRows));
    }

    private void AdjustSlots(int newAmountOfSlots)
    {
        int currentAmountOfSlots = slots.Count;
        int diff = newAmountOfSlots - currentAmountOfSlots;

        if (diff > 0)
            AddSlots(diff);
        else if (diff < 0)
            RemoveSlots(diff);

        currentAmountOfSlots = slots.Count;
        Debug.Log("Adjusting slot count to " + currentAmountOfSlots);
    }

    private void AddSlots(int diff)
    {
        int sC = slots.Count - 1;

        for (int i = sC; i < sC + diff; i++)
        {
            slots.Add(Instantiate<GameObject>(slotPrefab, transform));
        }

        AddButtonListeners();

    }

    private void RemoveSlots(int diff)
    {
        int targetAmount = ((slots.Count - 1) - Mathf.Abs(diff));
        for (int i = slots.Count - 1; i > targetAmount; i--)
        {
            Destroy(slots[i]);
            slots.RemoveAt(i);
        }
    }

    private void AddButtonListeners()
    {
        var iManager = NewInventoryManager.Instance;
        for (int i = 0; i < slots.Count; i++)
        {
            int slotIndex = i;
            //var slotIndex = slots[i].GetComponent<NewSlotScript>();
            var button = slots[i].GetComponent<CustomButton>();

            button.enter.AddListener(delegate { iManager.SlotEnter(slotIndex, containerData); });
            button.downLeft.AddListener(delegate { iManager.SlotDownLeft(slotIndex, containerData); });
            button.downRight.AddListener(delegate { iManager.SlotDownRight(slotIndex, containerData); });
            button.upLeft.AddListener(delegate { iManager.SlotUpLeft(slotIndex, containerData); });
            button.upRight.AddListener(delegate { iManager.SlotUpRight(slotIndex, containerData); });
            button.exit.AddListener(delegate { iManager.SlotExit(slotIndex, containerData); });
        }
    }

}
