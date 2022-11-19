using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGrassFromInventory : DialogueAction
{

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public override void PerformDialogueAction()
    {
        RemoveGrass();
    }

    private void RemoveGrass()
    {
        InventoryManager.Instance.RemoveItem("Green Grass");
    }
}
