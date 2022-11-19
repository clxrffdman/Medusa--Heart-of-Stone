using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;

    private PlayerController playerController;

    public GameObject inventoryUI;
    public Button useButton;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Image itemImage;
    public Sprite transparentSprite;
    public GameObject openInventorySfx;
    public GameObject closeInventorySfx;

    [Header("Base Data")]
    public List<Item> itemTypesList;
    public GameObject itemButtonGrid;
    public Button[] itemButtons;
    public Item currentlySelectedItem;
    public int currentButtonIndex;

    [Header("Saved Data")]
    public List<string> inventoryItemsList = new List<string>(10);
    public List<Item> inventoryItems = new List<Item>(10);

    [Header("Inventory-Related Values")]
    public int selectedMain;
    public int selectedSlot;
    public bool discarding;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        itemButtons = itemButtonGrid.GetComponentsInChildren<Button>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        Item[] allItems = Resources.LoadAll<Item>("ItemList/");
        for (int i = 0; i < allItems.Length; i++)
        {
            itemTypesList.Add(allItems[i]);
        }
    }

    void Start()
    {
        //CloseInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            
            if (!GameManager.Instance.inventoryActive && !GameManager.Instance.dialogueActive && !GameManager.Instance.pauseMenuActive)
            {
                OpenInventory();

            }
            else if (GameManager.Instance.inventoryActive)
            {
                CloseInventory();
            }
        }
    }

    public void PickUp(string itemKind)
    {

        //Checks to see if player can pickup items or if inventory is full.
        bool canPickUpItems = false;
        for (int i = 0; i < inventoryItemsList.Count; i++)
        {
            if (string.IsNullOrEmpty(inventoryItemsList[i]))
            {
                canPickUpItems = true;
            }
        }

        if (!canPickUpItems)
        {
            print("playing inventory full");
            return;
        }

        //Adds the item to the player's item list.
        for (int t = 0; t < inventoryItemsList.Count; t++)
        {
            if(string.IsNullOrEmpty(inventoryItemsList[t]))
            {
                inventoryItemsList[t] = itemKind;
                return;
            }
        }
    }

    void OpenInventory()
    {
        playerController.interactEnable = false;
        GameManager.Instance.inventoryActive = true;
        GameManager.Instance.PauseGame();
        inventoryUI.SetActive(true);
        PlayOpenInventorySfx();
        RefreshUI();
    }

    public void CloseInventory()
    {
        playerController.interactEnable = true;
        GameManager.Instance.inventoryActive = false;
        GameManager.Instance.UnPauseGame();
        currentlySelectedItem = null;
        currentButtonIndex = -1;
        ClearItemDescription();
        PlayCloseInventorySfx();
        inventoryUI.SetActive(false);
    }

    //This code will be used by the Use Button to use an item.
    public void Use()
    {

        if(currentButtonIndex == -1)
        {
            return;
        }

        string itemType = currentlySelectedItem.itemName;

        switch (itemType)
        {
            case "match":
                break;
            default:
                break;
        }

        RemoveItemFromSlot(currentButtonIndex);
        ClearItemDescription();
        RefreshUI();
        CloseInventory();
    }

    //Turns the inventory strings in the inventoryItemsList to actual items in the inventoryItems.
    public void InventoryStringsToItems()
    {
        for (int i = 0; i < inventoryItemsList.Count; i++)
        {

            if (!string.IsNullOrEmpty(inventoryItemsList[i]))
            {
                for (int t = 0; t < itemTypesList.Count; t++)
                {
                    if (inventoryItemsList[i] == itemTypesList[t].name)
                    {
                        inventoryItems[i] = itemTypesList[t];
                    }
                }
            }
        }
    }

    public Item InventoryStringToItems(string name)
    {
        for (int t = 0; t < itemTypesList.Count; t++)
        {
            if (name == itemTypesList[t].name)
            {
                return itemTypesList[t];
            }
        }

        return null;

    }

    //Sets the each item button to the correct item type according to the inventoryItemsList.
    public void AssignInventoryToButtons()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            ItemButton itemButton = itemButtons[i].GetComponent<ItemButton>();
            if(inventoryItems[i] != null)
            {
                itemButton.SetUpButton(inventoryItems[i]);
            }
            else
            {
                itemButton.DeactivateButton();
            }
        }
    }

    //This activates and deactivates the use button depending on whether there are useable items.
    public void CheckUseButtonShouldBeActive()
    {
        if(currentlySelectedItem.canUse == false)
        {
            useButton.interactable = false;
        } else
        {
            useButton.interactable = true;
        }
    }

    public void SetItemAsCurrentlySelected(int buttonIndex, Item item)
    {
        itemDescription.text = item.description;
        itemImage.sprite = item.icon;
        itemName.text = item.itemName;
        currentlySelectedItem = item;
        currentButtonIndex = buttonIndex;
        CheckUseButtonShouldBeActive();
    }

    public void RemoveItemFromSlot(int slotIndex)
    {
        inventoryItemsList.RemoveAt(slotIndex);
        inventoryItemsList.Add("");
        inventoryItems.RemoveAt(slotIndex);
        inventoryItems.Add(null);
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < inventoryItemsList.Count; i++)
        {
            if(itemName == inventoryItemsList[i])
            {
                inventoryItemsList.RemoveAt(i);
                inventoryItemsList.Add("");
            }
        }
    }

    public void AddItem(string itemName)
    {
        if (!CheckIfItemExistsFromName(itemName))
        {
            print("Item not found!");
            return;
        }

        for (int i = 0; i < inventoryItemsList.Count; i++)
        {
            if (inventoryItemsList[i] == "")
            {
                inventoryItemsList[i] = itemName;
                return;
            }
        }
    }

    public bool CheckIfItemExistsFromName(string itemName)
    {
        bool found = false;
        for (int i = 0; i < itemTypesList.Count; i++)
        {
            if(itemName == itemTypesList[i].itemName)
            {
                found = true;
            }
        }

        return found;
    }


    public bool CheckIfItemExistsFromID(string id)
    {
        bool found = false;
        foreach (string s in inventoryItemsList)
        {
            if (s == id)
            {
                found = true;
            }
        }

        if (found)
        {
            return true;
        }
        return false;
    }

    public void LoadObjectivesToMain()
    {
        ObjectiveManager.Instance.RemoveAllObjectives();
        for (int i = 0; i < SaveGameManager.Instance.savedObjectives.Length; i++)
        {
            ObjectiveManager.Instance.AddObjective(SaveGameManager.Instance.savedObjectives[i]);
        }
    }

    public void ClearItemDescription()
    {
        itemDescription.text = "Item Description";
        itemImage.sprite = transparentSprite;
        itemName.text = "Selected Item";
    }

    public void RefreshUI()
    {
        currentlySelectedItem = null;
        currentButtonIndex = -1;
        ClearItems();
        InventoryStringsToItems();
        AssignInventoryToButtons();
    }

    public bool CheckInventoryFull()
    {
        bool isFull = true;
        for (int i = 0; i < inventoryItemsList.Count; i++)
        {
            if (string.IsNullOrEmpty(inventoryItemsList[i]))
            {
                isFull = false;
            }   
        }

        return isFull;
    }

    public void ClearItems()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryItems[i] = null;
        }
    }

    public void PlayOpenInventorySfx()
    {
        Instantiate(openInventorySfx, transform.position, transform.rotation);
    }

    public void PlayCloseInventorySfx()
    {
        Instantiate(closeInventorySfx, transform.position, transform.rotation);
    }

    public void Drop(int slotIndex)
    {
        RefreshUI();
    }

    public void ClearSlot(int slotIndex)
    {
        RefreshUI();
    }
}
