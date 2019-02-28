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
        AddImage(container.maxCapacity, selectedInstance);
    }

    public void AddImage(int amount, GameObject targetContainer)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate<GameObject>(image, targetContainer.transform);
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
