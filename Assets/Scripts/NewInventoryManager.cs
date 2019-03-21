using System;
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


    #region Debug Variables
    int a = 0;
    public Text debugText;
    #endregion

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

    private List<SlotScript> playerSlots;
    private List<SlotScript> targetSlots;

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
        //SelectTargetContainer(); //Right click on something to set newTargetContainerData, currently hard-coded
        DebugEditText();
    }

    private void CreateNewContainerData(string str, int capacity)
    {
        //Single container can't display more than 56 slots right now (24 w/o moving transform)
        otherContainers.Add(new ContainerData(ContainerID, str, capacity));
        Debug.Log("Created: " + str + " with ID " + ContainerID);
        ContainerID++;
    }

    private void SelectTargetContainer(ContainerData containerData)
    {
        //Hard-coded value
        var newTargetContainerData = containerData;

        if (newTargetContainerData.containerID != targetContainerData.containerID)
        {
            targetContainerData = newTargetContainerData;
            Debug.Log("Selecting new container " + targetContainerData.containerName);
            UpdateContainerUI(targetContainerData);
        }
    }

    #region UI Functions
    private void CreateContainerUI()
    {
        playerContainerGO = Instantiate<GameObject>(containerPrefab, targetCanvas);
        playerContainerGO.name = "Player Container";
        playerContainerScript = playerContainerGO.GetComponent<NewContainerUI>();
        playerContainerGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(((1920/2) - 200), (-(1080 / 2) - 200));
        ToggleUI(playerContainerGO);

        targetContainerGO = Instantiate<GameObject>(containerPrefab, targetCanvas);
        targetContainerGO.name = "Target Container";
        targetContainerScript = targetContainerGO.GetComponent<NewContainerUI>();
        targetContainerGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(((1920 / 2) + 200), (-(1080 / 2) - 200));
        ToggleUI(targetContainerGO);
    }

    private void ToggleUI(GameObject go)
    {
        if (go.activeInHierarchy)
            go.SetActive(false);
        else
            go.SetActive(true);
    }

    private void UpdatePlayerContainerUI()
    {
        playerSlots = playerContainerScript.UpdateContainer(playerContainerData);
    }

    private void UpdateContainerUI(ContainerData containerData)
    {
        targetSlots = targetContainerScript.UpdateContainer(containerData);
        targetContainerGO.SetActive(true);
    }
    #endregion

    #region ButtonEvents
    public void SlotEnter(SlotScript slot)
    {

    }

    public void SlotLeftClick(SlotScript slot, int slotIndex)
    {

    }

    public void SlotRightClick(SlotScript slot, int slotIndex)
    {
        RequestMove(slot, slotIndex);
    }

    public void SlotExit(int slotIndex, ContainerData containerData)
    {

    }

    #region If Using Down/Up
    public void SlotDownLeft(int slotIndex, ContainerData containerData)
    {
        //Debug.Log("2a");
    }

    public void SlotDownRight(int slotIndex, ContainerData containerData)
    {
        //Debug.Log("2b");
    }

    public void SlotUpLeft(int slotIndex, ContainerData containerData)
    {
        //Debug.Log("3a");
    }

    public void SlotUpRight(int slotIndex, ContainerData containerData)
    {
        //Debug.Log("3b");
    }
    #endregion
    #endregion

    #region Slot Functions

    /*
     * 1. Get Slot
     * 2. See what container with BelongsToPlayer();
     * 3. See if that container and slot has an item
     * 4. See other container's slot if it has space available with GetFirstAvailableSlot();
     * 5. Move Item to other container
     */

    private void RequestMove(SlotScript slot, int slotIndex)
    {
        ContainerData currentContainer;
        ContainerData otherContainer;
        ItemData tempItem;

        currentContainer = GetCurrentContainer(slot);
        tempItem = GetItem(currentContainer, slotIndex);
        otherContainer = GetOtherContainer(currentContainer);
        if (HasSpace(otherContainer) && tempItem != null)
        {
            //freeSlotIndex = GetFirstAvailableSlot(otherContainer); Add this when/if you cba to implement null slots
            Debug.Log("Hej");
            //move what, in current slot, from container, to container
            MoveItem(tempItem, slotIndex, currentContainer, otherContainer);
        }
        else
            Debug.Log(otherContainer.containerName + " is full!");
    }

    private void MoveItem(ItemData tempItem, int slotIndex, ContainerData fromContainer, ContainerData toContainer)
    {
        fromContainer.items.RemoveAt(slotIndex);
        toContainer.items.Add(tempItem.GetClone());
    }

    private ContainerData GetCurrentContainer(SlotScript slot)
    {
        if (BelongsToPlayer(slot))
            return playerContainerData;
        else
            return targetContainerData;
    }
    
    private ContainerData GetOtherContainer(ContainerData containerData)
    {
        if (containerData.containerID != playerContainerData.containerID)
            return playerContainerData;
        else
            return targetContainerData;
    }

    private ItemData GetItem(ContainerData currentContainer, int slotIndex)
    {
        if (slotIndex < currentContainer.items.Count)
        {
            return currentContainer.items[slotIndex];
        }
        else
        {
            return null;
        }
    }

    private bool BelongsToPlayer(SlotScript slot)
    {
        return playerSlots.Contains(slot);
    }

    private bool HasSpace(ContainerData containerData)
    {
        return containerData.items.Count < containerData.maxCapacity;
    }

    private int GetFirstAvailableSlot(ContainerData containerData)
    {
        {
            for (int i = 0; i < containerData.maxCapacity - 1; i++)
            {
                if (containerData.items[i] == null)
                    return i;
            }
        }
        return 0; //This will never happen????
    } //Currently unused

    private void UpdateSlot()
    {
        //slot[index].image = containderData.items[index].sprite;
    }

    private void UpdateAllRemainingSlots()
    {
        //for i = currentslot; i < slot.count, UpdateSlot()
    }
    #endregion

    // --- DEBUG FUNCTIONS ---
    #region Debug Functions
    public void DebugCycleContainers() //Cycle containers
    {
        int b = a % otherContainers.Count;
        SelectTargetContainer(otherContainers[b]);
        a++;
    }

    public void DebugTogglePlayerInventory()
    {
        ToggleUI(playerContainerGO);
    }

    public void DebugEditText()
    {
        debugText.text = "List of containers: \n";
        for (int i = 0; i < otherContainers.Count; i++)
        {
            debugText.text += (otherContainers[i].containerName + '\n');
        }
    }
    #endregion

}
