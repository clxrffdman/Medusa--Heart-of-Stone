using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliveOilInteractable : Interactable
{
    private InventoryManager inventoryManager;
    [HideInInspector]
    public EnvironmentalObject environmentalObject;
    public GameObject giveOliveOilDialogueSetup;
    public GameObject noOilDialogueSetup;

    public enum ObjectStates { Start, NoOil};

    public override void Start()
    {
        environmentalObject = GetComponent<EnvironmentalObject>();
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        inventoryManager = FindObjectOfType<InventoryManager>();
        UpdateState();
        base.Start();
    }

    public override void UpdateState()
    {
        if (TaskManager.Instance.CheckTaskComplete("gotOliveOil"))
        {
            environmentalObject.SetCurrentState(ObjectStates.NoOil.ToString());
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                giveOliveOilDialogueSetup.SetActive(true);
                noOilDialogueSetup.SetActive(false);
                break;
            case "NoOil":
                giveOliveOilDialogueSetup.SetActive(false);
                noOilDialogueSetup.SetActive(true);
                break;
            default:
                break;
        }
    }

    public override void ResetStateToStart()
    {
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        UpdateState();
    }

}
