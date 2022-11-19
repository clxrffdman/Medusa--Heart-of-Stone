using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorInteractableScript : Interactable
{
    public EnvironmentalObject environmentalObject;
    public SpriteRenderer mirrorSpriteRenderer;
    public Sprite[] spriteStates;
    public GameObject startDialogueSetup;
    public GameObject brokenDialogueSetup;

    [HideInInspector]
    public enum ObjectStates { Start, Broken};

    public override void Start()
    {
        environmentalObject = GetComponent<EnvironmentalObject>();
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        UpdateState();
        base.Start();
    }

    public override void UpdateState()
    {

        if (TaskManager.Instance.CheckTaskComplete("brokeMirror"))
        {
            environmentalObject.SetCurrentState(ObjectStates.Broken.ToString());
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                mirrorSpriteRenderer.sprite = spriteStates[0];
                startDialogueSetup.SetActive(true);
                brokenDialogueSetup.SetActive(false);
                break;
            case "Broken":
                mirrorSpriteRenderer.sprite = spriteStates[1];
                startDialogueSetup.SetActive(false);
                brokenDialogueSetup.SetActive(true);
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
