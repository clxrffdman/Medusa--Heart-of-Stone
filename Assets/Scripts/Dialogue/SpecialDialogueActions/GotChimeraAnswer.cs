using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotChimeraAnswer : DialogueAction
{

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public override void PerformDialogueAction()
    {
        RemoveIngredients();
    }

    private void RemoveIngredients()
    {
        inventoryManager.RemoveItem("Bag of Grain");
        inventoryManager.RemoveItem("Pot of Milk");
        inventoryManager.RemoveItem("Jar of Oil");
    }
}

