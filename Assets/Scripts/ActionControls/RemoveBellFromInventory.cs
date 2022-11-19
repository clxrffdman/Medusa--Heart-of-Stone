using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBellFromInventory : SpecialAction
{

    private InventoryManager inventoryManager;
    public InteractableItem bell;
    public BellInteractable bellHolder;

    public override void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public override void PerformSpecialAction()
    {
        RemoveBell();
    }

    private void RemoveBell()
    {
        InventoryManager.Instance.RemoveItem("Bell");
        bell.SetItemAsUncollected();
        bellHolder.ResetStateToStart();
    }
}