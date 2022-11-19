using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveChimeraKeyFromInventory : SpecialAction
{

    private InventoryManager inventoryManager;
    public InteractableItem chimeraKey;

    public override void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public override void PerformSpecialAction()
    {
        RemoveChimeraKey();
    }

    private void RemoveChimeraKey()
    {
        InventoryManager.Instance.RemoveItem("Cage Key");
        chimeraKey.SetItemAsUncollected();
    }
}

