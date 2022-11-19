using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManInteractable : Interactable
{
    private InventoryManager inventoryManager;
    private ActionController actionController;
    [HideInInspector]
    public EnvironmentalObject environmentalObject;
    public GameObject propositionDialogueSetup;
    public GameObject reminderDialogueSetup;
    public GameObject chimeraAnswerDialogueSetup;
    public GameObject exitAction;
    public enum ObjectStates { Start, Reminder, ChimeraAnswer, ExitCity, FinishedDemo };

    public override void Start()
    {
        environmentalObject = GetComponent<EnvironmentalObject>();
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        inventoryManager = FindObjectOfType<InventoryManager>();
        actionController = FindObjectOfType<ActionController>();
        UpdateState();
        base.Start();
    }

    public override void UpdateState()
    {
        if (TaskManager.Instance.CheckTaskComplete("talkedToOldMan"))
        {
            environmentalObject.SetCurrentState(ObjectStates.Reminder.ToString());
        }

        if (CheckHasAllBreadIngredients() && TaskManager.Instance.CheckTaskComplete("talkedToOldMan"))
        {
            environmentalObject.SetCurrentState(ObjectStates.ChimeraAnswer.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete("gotChimeraAnswer"))
        {
            environmentalObject.SetCurrentState(ObjectStates.ExitCity.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete("finishedDemo"))
        {
            environmentalObject.SetCurrentState(ObjectStates.FinishedDemo.ToString());
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                propositionDialogueSetup.SetActive(true);
                reminderDialogueSetup.SetActive(false);
                chimeraAnswerDialogueSetup.SetActive(false);
                break;
            case "Reminder":
                propositionDialogueSetup.SetActive(false);
                reminderDialogueSetup.SetActive(true);
                chimeraAnswerDialogueSetup.SetActive(false);
                break;
            case "ChimeraAnswer":
                propositionDialogueSetup.SetActive(false);
                reminderDialogueSetup.SetActive(false);
                chimeraAnswerDialogueSetup.SetActive(true);
                break;
            case "ExitCity":
                propositionDialogueSetup.SetActive(false);
                reminderDialogueSetup.SetActive(false);
                chimeraAnswerDialogueSetup.SetActive(false);
                LoadNextLevel();
                break;
            case "FinishedDemo":
                break;
            default:
                break;
        }
    }

    public bool CheckHasAllBreadIngredients()
    {
        bool hasAllIngredients = false;

        if (TaskManager.Instance.CheckTaskComplete("gotMilk") && TaskManager.Instance.CheckTaskComplete("gotOliveOil") && TaskManager.Instance.CheckTaskComplete("gotGrain"))
        {
            hasAllIngredients = true;
        }

        return hasAllIngredients;
    }


    public override void ResetStateToStart()
    {
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        UpdateState();
    }

    private void LoadNextLevel()
    {
        actionController.DeactivateAllActions();
        exitAction.SetActive(true);
    }

}
