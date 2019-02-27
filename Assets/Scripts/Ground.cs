using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public ContainerData groundInventory;

    public void Start()
    {
        CreateInventory();
    }

    private void CreateInventory()
    {
        groundInventory = new ContainerData(1, "The Ground", 10);
    }
}
