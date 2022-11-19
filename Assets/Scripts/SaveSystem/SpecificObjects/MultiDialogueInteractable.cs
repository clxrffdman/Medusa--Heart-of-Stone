using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDialogueInteractable : Interactable
{
    [HideInInspector]
    public EnvironmentalObject environmentalObject;
    public GameObject startDialogueSetup;
    public GameObject endDialogueSetup;
    public string taskNameForEndDialogue;

    public enum ObjectStates { Start, End };

    public override void Start()
    {
        environmentalObject = GetComponent<EnvironmentalObject>();
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        UpdateState();
        base.Start();
    }

    public override void UpdateState()
    {
        if (TaskManager.Instance.CheckTaskComplete(taskNameForEndDialogue))
        {
            environmentalObject.SetCurrentState(ObjectStates.End.ToString());
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                startDialogueSetup.SetActive(true);
                endDialogueSetup.SetActive(false);
                break;
            case "End":
                startDialogueSetup.SetActive(false);
                endDialogueSetup.SetActive(true);
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