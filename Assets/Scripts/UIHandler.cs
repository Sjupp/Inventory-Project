using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject containerPrefab;
    public List<Container> listOfActiveContainers;

    public void CreateContainerUIElement(ContainerData container)
    {
        GameObject containerObject = Instantiate<GameObject>(containerPrefab, transform);
        listOfActiveContainers.Add(containerObject.GetComponent<Container>());
        containerObject.GetComponent<Container>().InitializeContainer(container);
        //listOfActiveContainers[listOfActiveContainers.Count].InitializeContainer(container); //maybe?
    }

    public void UpdateContainer(ContainerData one, ContainerData two)
    {
        if (FindContainerDataInContainer(one) != null) 
        {
            FindContainerDataInContainer(one).UpdateSlots(one);
        }

        if (FindContainerDataInContainer(two) != null)
        {
            FindContainerDataInContainer(two).UpdateSlots(two);
        }
    }

    private void ToggleContainer()
    {

    }

    public Container FindContainerDataInContainer(ContainerData containerData)
    {
        foreach (Container container in listOfActiveContainers)
        {
            if (container.container.containerID == containerData.containerID)
            {
                return container;
            }
        }
        return null;
    }
}
