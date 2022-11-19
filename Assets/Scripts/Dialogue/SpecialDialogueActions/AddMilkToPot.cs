using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMilkToPot : DialogueAction
{

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public override void PerformDialogueAction()
    {
        AddMilkToClayPot();
    }

    private void AddMilkToClayPot()
    {
        for (int i = 0; i < inventoryManager.inventoryItemsList.Count; i++)
        {
            if (inventoryManager.inventoryItemsList[i] == "Clay Pot")
            {
                inventoryManager.inventoryItemsList[i] = "Pot of Milk";
            }
        }
    }
}
