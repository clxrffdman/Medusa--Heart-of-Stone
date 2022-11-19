using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGrainToInventory : DialogueAction
{

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public override void PerformDialogueAction()
    {
        AddGrain();
    }

    private void AddGrain()
    {
        inventoryManager.AddItem("Bag of Grain");
    }
}
