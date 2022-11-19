using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageInteractable : Interactable
{

    public string taskNeededToOpen;
    public string taskNeededToEmpty;
    public EnvironmentalObject environmentalObject;
    public GameObject cageDoorClosed;
    public GameObject cageDoorOpen;
    public GameObject startDialogueSetup;
    public GameObject openDialogueSetup;

    public enum ObjectStates { Start, CanOpen, Empty };

    // Start is called before the first frame update
    public override void Start()
    {
        environmentalObject = GetComponent<EnvironmentalObject>();
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        UpdateState();
        base.Start();
    }

    public override void UpdateState()
    {
        if (TaskManager.Instance.CheckTaskComplete(taskNeededToOpen))
        {
            environmentalObject.SetCurrentState(ObjectStates.CanOpen.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete(taskNeededToEmpty))
        {
            environmentalObject.SetCurrentState(ObjectStates.Empty.ToString());
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                cageDoorClosed.SetActive(true);
                cageDoorOpen.SetActive(false);
                startDialogueSetup.SetActive(true);
                openDialogueSetup.SetActive(false);
                break;
            case "CanOpen":
                cageDoorClosed.SetActive(true);
                cageDoorOpen.SetActive(false);
                startDialogueSetup.SetActive(false);
                openDialogueSetup.SetActive(true);
                break;
            case "Empty":
                cageDoorClosed.SetActive(false);
                cageDoorOpen.SetActive(true);
                startDialogueSetup.SetActive(false);
                openDialogueSetup.SetActive(false);
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
