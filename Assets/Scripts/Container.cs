using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour
{
    public ContainerData container;
    private GameObject[] slots;

    public GameObject slotPrefab;

    public void InitializeContainer(ContainerData container)
    {
        this.container = container;

        UpdateUIFrameSize();
        CreateSlots();
        UpdateSlots(container);
        GetComponent<RectTransform>().anchoredPosition =
            new Vector2(Random.Range(-300, 300), 0);
    }

    private void UpdateUIFrameSize()
    {
        int numberOfRows = Mathf.CeilToInt((container.maxCapacity - 1) / 4);
        GetComponent<RectTransform>().sizeDelta =
            new Vector2(215, 65 + (50 * numberOfRows));
    }

    private void CreateSlots()
    {
        slots = new GameObject[container.maxCapacity];
        for (int i = 0; i < container.maxCapacity; i++)
        {
            slotPrefab = Instantiate<GameObject>(slotPrefab, transform);
            slots[i] = slotPrefab;
        }
    }

    public void UpdateSlots(ContainerData container)
    {
        this.container = container;
        int i = 0;
        foreach (GameObject slot in slots)
        {
            if (i < container.items.Count)
            {
                slot.GetComponent<Image>().sprite = container.items[i].sprite;
            }
            else
            {
                slot.GetComponent<Image>().sprite = null;
            }
            i++;
        }
    }

}
