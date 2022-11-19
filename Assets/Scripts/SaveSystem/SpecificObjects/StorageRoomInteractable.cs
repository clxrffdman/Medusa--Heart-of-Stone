using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRoomInteractable : Interactable
{
    private InventoryManager inventoryManager;
    [HideInInspector]
    public EnvironmentalObject environmentalObject;
    public GameObject killRatsDialogueSetup;
    public GameObject storageLocationDialogueSetup;
    public GameObject giveGrainDialogueSetup;
    public GameObject ratsDeadDialogueSetup;
    public GameObject storageDoor;

    public enum ObjectStates { Start, StorageLocation, GiveGrain, DeadRats };

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
        if (TaskManager.Instance.CheckTaskComplete("talkedToStorageMan"))
        {
            environmentalObject.SetCurrentState(ObjectStates.StorageLocation.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete("killedRats"))
        {
            environmentalObject.SetCurrentState(ObjectStates.GiveGrain.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete("gotGrain"))
        {
            environmentalObject.SetCurrentState(ObjectStates.DeadRats.ToString());
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                killRatsDialogueSetup.SetActive(true);
                storageLocationDialogueSetup.SetActive(false);
                giveGrainDialogueSetup.SetActive(false);
                ratsDeadDialogueSetup.SetActive(false);
                storageDoor.SetActive(false);
                break;
            case "StorageLocation":
                killRatsDialogueSetup.SetActive(false);
                storageLocationDialogueSetup.SetActive(true);
                giveGrainDialogueSetup.SetActive(false);
                ratsDeadDialogueSetup.SetActive(false);
                storageDoor.SetActive(true);
                break;
            case "GiveGrain":
                killRatsDialogueSetup.SetActive(false);
                storageLocationDialogueSetup.SetActive(false);
                giveGrainDialogueSetup.SetActive(true);
                ratsDeadDialogueSetup.SetActive(false);
                storageDoor.SetActive(false);
                break;
            case "DeadRats":
                killRatsDialogueSetup.SetActive(false);
                storageLocationDialogueSetup.SetActive(false);
                giveGrainDialogueSetup.SetActive(false);
                ratsDeadDialogueSetup.SetActive(true);
                storageDoor.SetActive(false);
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
