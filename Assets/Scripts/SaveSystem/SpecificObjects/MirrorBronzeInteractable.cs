using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBronzeInteractable : Interactable
{
    public GameObject particles;
    public GameObject erosReflection;
    public GameObject startDialogueSetup;
    public GameObject erosPrayer01DialogueSetup;


    [HideInInspector]
    public EnvironmentalObject environmentalObject;
    [HideInInspector]
    public enum ObjectStates { Start, ErosPrayer01, ErosActive, ErosDeactivating};

    public override void Start()
    {
        environmentalObject = GetComponent<EnvironmentalObject>();
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        UpdateState();
        base.Start();
    }

    public override void UpdateState()
    {

        if (TaskManager.Instance.CheckTaskComplete("foughtSoldiers1"))
        {
            environmentalObject.SetCurrentState(ObjectStates.ErosPrayer01.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete("preyToEros01"))
        {
            environmentalObject.SetCurrentState(ObjectStates.ErosActive.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete("askErosForHelp"))
        {
            environmentalObject.SetCurrentState(ObjectStates.ErosDeactivating.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete("thankErosForHelp"))
        {
            environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                erosReflection.SetActive(false);
                particles.SetActive(false);
                startDialogueSetup.SetActive(true);
                erosPrayer01DialogueSetup.SetActive(false);
                break;
            case "ErosPrayer01":
                erosReflection.SetActive(false);
                particles.SetActive(false);
                startDialogueSetup.SetActive(false);
                erosPrayer01DialogueSetup.SetActive(true);
                break;
            case "ErosActive":
                particles.SetActive(true);
                Invoke("ActivateEros", 3f);
                break;
            case "ErosDeactivating":
                particles.SetActive(false);
                particles.SetActive(true);
                Invoke("DeactivateEros", 3f);
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

    private void ActivateEros()
    {
        erosReflection.SetActive(true);
    }

    private void DeactivateEros()
    {
        erosReflection.SetActive(false);
    }
}
