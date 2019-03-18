using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewInventoryManager : MonoBehaviour
{

    public static NewInventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    #region ContainerData Variables
    public ContainerData playerContainerData;
    public ContainerData targetContainerData;
    public List<ContainerData> otherContainers = new List<ContainerData>();
    private int ContainerID = 1;
    #endregion

    #region Container Object Variables
    [SerializeField]
    private Transform targetCanvas;
    [SerializeField]
    private GameObject containerPrefab;
    private GameObject playerContainerGO;
    private NewContainerUI playerContainerScript;
    private GameObject targetContainerGO;
    private NewContainerUI targetContainerScript;
    #endregion

    private List<GameObject> playerSlots;
    private List<GameObject> targetSlots;

    private void Start()
    {
        //Required on startup
        playerContainerData = new ContainerData(-1, "Player Inventory", 16);
        CreateContainerUI();
        UpdatePlayerContainerUI();

        //Temp stuff
        CreateNewContainerData("Container 1", 24);
        CreateNewContainerData("Container 2", 8);
        CreateNewContainerData("Container 3", 16);
        SelectTargetContainer(); //Right click on something to set newTargetContainerData, currently hard-coded
    }

    private void CreateNewContainerData(string str, int capacity)
    {
        //Single container can't display more than 56 slots right now (24 w/o moving transform)
        otherContainers.Add(new ContainerData(ContainerID, str, capacity));
        Debug.Log("Created: " + str + " with ID " + ContainerID);
        ContainerID++;
    }

    private void CreateContainerUI()
    {
        playerContainerGO = Instantiate<GameObject>(containerPrefab, targetCanvas);
        playerContainerGO.name = "Player Container";
        playerContainerScript = playerContainerGO.GetComponent<NewContainerUI>();
        playerContainerGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, -200);

        targetContainerGO = Instantiate<GameObject>(containerPrefab, targetCanvas);
        targetContainerGO.name = "Target Container";
        targetContainerScript = targetContainerGO.GetComponent<NewContainerUI>();
        targetContainerGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, -200);
    }

    private void TogglePlayerContainerUI()
    {
        if (playerContainerGO.activeInHierarchy)
            playerContainerGO.SetActive(false);
        else
            playerContainerGO.SetActive(true);
    }

    private void UpdatePlayerContainerUI()
    {
        playerSlots = playerContainerScript.UpdateContainer(playerContainerData);
    }

    private void SelectTargetContainer()
    {
        //Hard-coded value
        var newTargetContainerData = otherContainers[0];

        if (newTargetContainerData.containerID != targetContainerData.containerID)
        {
            targetContainerData = newTargetContainerData;
            Debug.Log("Selecting new container " + targetContainerData.containerName);
            UpdateContainerUI(targetContainerData);
        }
    }

    private void UpdateContainerUI(ContainerData containerData)
    {
        targetSlots = targetContainerScript.UpdateContainer(containerData);
    }

    #region ButtonEvents
    public void SlotEnter(int slotIndex, ContainerData containerData)
    {
        //Debug.Log("1");
        
    }

    public void SlotDownLeft(int slotIndex, ContainerData containerData)
    {
        //Debug.Log("2a");
    }

    public void SlotDownRight(int slotIndex, ContainerData containerData)
    {
        Debug.Log(slotIndex);

        if (slotIndex < containerData.items.Count)
            Debug.Log(containerData.items[slotIndex]);
        else
            Debug.Log("No item in here!");
    }

    public void SlotUpLeft(int slotIndex, ContainerData containerData)
    {
        //Debug.Log("3a");
    }

    public void SlotUpRight(int slotIndex, ContainerData containerData)
    {
        //Debug.Log("3b");
    }

    public void SlotExit(int slotIndex, ContainerData containerData)
    {
        //Debug.Log("4");
    }
    #endregion

    private void UpdateSlot( )
    {
        //slot[index].image = containderData.items[index].sprite;
    }

    private void GetFirstAvailableSlot()
    {
        //foreach, if slot == null, return slot
    }

    private void UpdateAllRemainingSlots()
    {
        //for i = currentslot; i < slot.count, UpdateSlot()
    }

    //private void UpdateAllSlots(List<GameObject> slots)
    //{
    //    foreach (GameObject slot in slots)
    //    {
    //        slot.GetComponent<NewSlotScript>().image;
    //    }
    //}

}
