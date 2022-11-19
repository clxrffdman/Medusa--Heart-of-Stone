using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemFromInventory : DialogueAction
{

    private InventoryManager inventoryManager;
    public string itemNameToRemove;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public override void PerformDialogueAction()
    {
        RemoveItem();
    }

    private void RemoveItem()
    {
        InventoryManager.Instance.RemoveItem(itemNameToRemove);
    }
}
