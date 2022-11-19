using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatingGameInteractable : Interactable
{
    public GameObject newDatePrompt;
    public GameObject startDialogueSetup;
    public GameObject hephaestusDialogueSetup;

    [HideInInspector]
    public EnvironmentalObject environmentalObject;
    [HideInInspector]
    public enum ObjectStates { Start, Hephaestus, Dionysus, Sphinx, Off };

    public override void Start()
    {
        environmentalObject = GetComponent<EnvironmentalObject>();
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        UpdateState();
        base.Start();
    }

    public override void UpdateState()
    {
        if (TaskManager.Instance.CheckTaskComplete("askErosForHelp"))
        {
            environmentalObject.SetCurrentState(ObjectStates.Hephaestus.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete("datedHephaestus"))
        {
            environmentalObject.SetCurrentState(ObjectStates.Off.ToString());
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                newDatePrompt.SetActive(false);
                startDialogueSetup.SetActive(true);
                hephaestusDialogueSetup.SetActive(false);
                break;
            case "Hephaestus":
                newDatePrompt.SetActive(true);
                startDialogueSetup.SetActive(false);
                hephaestusDialogueSetup.SetActive(true);
                break;
            case "Off":
                newDatePrompt.SetActive(false);
                startDialogueSetup.SetActive(false);
                hephaestusDialogueSetup.SetActive(false);
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
