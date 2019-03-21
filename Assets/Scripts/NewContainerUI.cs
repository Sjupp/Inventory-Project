using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewContainerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private GameObject slotHolder;
    [SerializeField]
    private Text containerText;

    private ContainerData containerData;
    List<SlotScript> slots = new List<SlotScript>();

    public List<SlotScript> UpdateContainer(ContainerData containerData)
    {
        this.containerData = containerData;
        UpdateUIFrameSize(containerData.maxCapacity);
        AdjustSlots(containerData.maxCapacity);
        containerText.text = containerData.containerName;
        return slots;
    }

    private void UpdateUIFrameSize(int input)
    {
        int numberOfRows = Mathf.CeilToInt((input - 1) / 4);
        int height = 85 + (50 * numberOfRows);
        int width = 215;
        GetComponent<RectTransform>().sizeDelta =
            new Vector2(width, height);
        FitToScreen(width, height);
    }

    #region Slots
    private void AdjustSlots(int newAmountOfSlots)
    {
        int currentAmountOfSlots = slots.Count;
        int diff = newAmountOfSlots - currentAmountOfSlots;

        if (diff > 0)
            AddSlots(diff);
        else if (diff < 0)
            RemoveSlots(diff);

        currentAmountOfSlots = slots.Count;
        UpdateListeners();
    }

    private void AddSlots(int diff)
    {
        int sC = slots.Count - 1;

        for (int i = sC; i < sC + diff; i++)
        {
            GameObject temp = Instantiate<GameObject>(slotPrefab, slotHolder.transform);
            slots.Add(temp.GetComponent<SlotScript>());
        }
    }

    private void RemoveSlots(int diff)
    {
        int targetAmount = ((slots.Count - 1) - Mathf.Abs(diff));
        for (int i = slots.Count - 1; i > targetAmount; i--)
        {
            Destroy(slots[i].gameObject);
            slots.RemoveAt(i);
        }
    }
    #endregion

    private void UpdateListeners()
    {
        var iManager = NewInventoryManager.Instance;
        for (int i = 0; i < slots.Count; i++)
        {
            int slotIndex = i;
            var button = slots[i];

            button.slotIndex = slotIndex;
            button.containerData = containerData;
        }
    }

    private void FitToScreen(int containerWidth, int containerHeight)
    {
        var currentPos = GetComponent<RectTransform>();

        if (currentPos.anchoredPosition.y > 0)
        {
            currentPos.anchoredPosition = new Vector2(currentPos.anchoredPosition.x, 0);
        }

        if (currentPos.anchoredPosition.y - containerHeight < -Screen.height)
        {
            currentPos.anchoredPosition = new Vector2(currentPos.anchoredPosition.x, -Screen.height + containerHeight);
        }

        if (currentPos.anchoredPosition.x > containerWidth + Screen.width)
        {
            currentPos.anchoredPosition = new Vector2(Screen.width - containerWidth, currentPos.anchoredPosition.y);
        }

        if (currentPos.anchoredPosition.x < 0)
        {
            currentPos.anchoredPosition = new Vector2(0, currentPos.anchoredPosition.y);
        }
    }

}
