using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public GameObject screenUI;
    public GameObject containerPanel;
    public GameObject image;
    public List<GameObject> listOfContainers;

    private List<GameObject> containerUIList = new List<GameObject>();

    private List<GameObject> slotList = new List<GameObject>();

    public void InstantiateContainerUI(ContainerData container)
    {
        GameObject selectedInstance = Instantiate<GameObject>(containerPanel, screenUI.transform, false);

        //Test
        selectedInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));

        listOfContainers.Add(selectedInstance);
        containerUIList.Add(selectedInstance);
        int numberOfRows = Mathf.CeilToInt(container.maxCapacity / 4);
        selectedInstance.GetComponent<RectTransform>().sizeDelta =
            new Vector2(215, 65 + (50 * numberOfRows));
        AddContainerSlots(container.maxCapacity, selectedInstance, container);
    }

    public void AddContainerSlots(int amount, GameObject targetContainer, ContainerData container)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject slot = Instantiate<GameObject>(image, targetContainer.transform);
            slotList.Add(slot);
        }
        UpdateContainer(container);
    }

    public void UpdateContainer(ContainerData container)
    {
        int a = slotList.Count;
        int b = container.items.Count;
        for (int i = 0; i < a; i++)
        {
            
        }
        //int i = container.items.Count;
        //Debug.Log("container items count " + i);

        foreach (GameObject slot in slotList)
        {
            //if (i >= 0)
            //{
                slot.GetComponent<Image>().sprite = container.items[0].sprite;
            //    i--;
            //}
        }
    }



    public void RemoveContainerUI()
    {
        if (containerUIList.Count > 0)
        {
            GameObject hej = containerUIList[containerUIList.Count - 1].gameObject;
            Destroy(hej);
            containerUIList.RemoveAt(containerUIList.Count - 1);
        }
        else
        {
            Debug.Log("Nothing to Remove!");
        }
        
    }
    
}
