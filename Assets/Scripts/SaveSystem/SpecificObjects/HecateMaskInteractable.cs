using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HecateMaskInteractable : Interactable
{

    public GameObject hecateMask;


    [HideInInspector]
    public EnvironmentalObject environmentalObject;
    [HideInInspector]
    public enum ObjectStates { Start, Active };

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

        if (TaskManager.Instance.CheckTaskComplete("talkToHecate01"))
        {
            environmentalObject.SetCurrentState(ObjectStates.Active.ToString());
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                hecateMask.SetActive(false);
                break;
            case "Active":
                hecateMask.SetActive(true);
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
