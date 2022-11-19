using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOliveOilToInventory : DialogueAction
{

    private InventoryManager inventoryManager;
    private LevelControl levelController;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        levelController = FindObjectOfType<LevelControl>();
    }

    public override void PerformDialogueAction()
    {
        AddOliveOil();
    }

    private void AddOliveOil()
    {
        bool canAddOliveOil = true;
        for (int i = 0; i < inventoryManager.inventoryItemsList.Count; i++)
        {
            if(inventoryManager.inventoryItemsList[i] == "Jar of Oil")
            {
                canAddOliveOil = false;
            }
        }

        if (canAddOliveOil)
        {
            inventoryManager.AddItem("Jar of Oil");
        }
        
        levelController.UpdateStatesForEnvironmentalObjects();
    }
}
