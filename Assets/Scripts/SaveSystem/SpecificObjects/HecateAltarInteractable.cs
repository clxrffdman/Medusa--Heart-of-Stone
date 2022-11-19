using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HecateAltarInteractable : Interactable
{

    private InventoryManager inventoryManager;
    public GameObject cornucopiaEmpty;
    public GameObject cornucopiaFull;
    public int numberOfBerriesNeeded;
    public string taskNeededToDeactivate;
    public GameObject hecate;
    public GameObject startDialogueSetup;
    public GameObject activateHecate;
    public GameObject cornucopiaDialogue;

    [HideInInspector]
    public enum ObjectStates { Start, CanBeActivated, NotActive };
    [HideInInspector]
    public EnvironmentalObject environmentalObject;

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

        if (CheckEnoughBerries())
        {
            environmentalObject.currentState = ObjectStates.CanBeActivated.ToString();
        }
        else
        {
            environmentalObject.currentState = ObjectStates.Start.ToString();
        }

        if (TaskManager.Instance.CheckTaskComplete(taskNeededToDeactivate))
        {
            environmentalObject.currentState = ObjectStates.NotActive.ToString();
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                startDialogueSetup.SetActive(true);
                cornucopiaEmpty.SetActive(true);
                cornucopiaFull.SetActive(false);
                activateHecate.SetActive(false);
                hecate.SetActive(false);
                cornucopiaDialogue.SetActive(false);
                break;
            case "CanBeActivated":
                startDialogueSetup.SetActive(false);
                cornucopiaEmpty.SetActive(true);
                cornucopiaFull.SetActive(false);
                activateHecate.SetActive(true);
                hecate.SetActive(false);
                cornucopiaDialogue.SetActive(false);
                break;
            case "NotActive":
                startDialogueSetup.SetActive(false);
                cornucopiaEmpty.SetActive(false);
                cornucopiaFull.SetActive(true);
                activateHecate.SetActive(false);
                RemoveAllBerries();
                hecate.SetActive(false);
                cornucopiaDialogue.SetActive(true);
                break;
            default:
                break;
        }
    }

    private bool CheckEnoughBerries()
    {
        int numberOfBerries = 0;
        bool hasEnoughBerries = false;

        for (int i = 0; i < inventoryManager.inventoryItemsList.Count; i++)
        {
            if (inventoryManager.inventoryItemsList[i] == "Berry")
            {
                numberOfBerries++;
            }
        }

        if (numberOfBerries == numberOfBerriesNeeded)
        {
            hasEnoughBerries = true;
        }
        return hasEnoughBerries;
    }

    private void RemoveAllBerries()
    {
        for (int i = 0; i < inventoryManager.inventoryItemsList.Count; i++)
        {
            if (inventoryManager.inventoryItemsList[i] == "Berry")
            {
                inventoryManager.inventoryItemsList[i] = "";
            }
        }
    }

    public override void ResetStateToStart()
    {
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        UpdateState();
    }
}
