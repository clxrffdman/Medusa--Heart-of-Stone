using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable
{
    public EnvironmentalObject environmentalObject;
    public SpriteRenderer chestSpriteRenderer;
    public Sprite[] spriteStates;
    public GameObject startDialogueSetup;
    public GameObject openDialogueSetup;
    public GameObject emptyDialogueSetup;
    public GameObject actionActivator;

    public enum ObjectStates { Start, CanOpen, Empty};

    // Start is called before the first frame update
    public override void Start()
    {
        chestSpriteRenderer = GetComponent<SpriteRenderer>();
        environmentalObject = GetComponent<EnvironmentalObject>();
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        UpdateState();
        base.Start();
    }

    public override void UpdateState()
    {
        if (TaskManager.Instance.CheckTaskComplete("spokeToOracle"))
        {
            environmentalObject.SetCurrentState(ObjectStates.CanOpen.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete("collectedHeartJar"))
        {
            environmentalObject.SetCurrentState(ObjectStates.Empty.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete("collectedHeartJar") && !TaskManager.Instance.CheckTaskComplete("talkToSnakey"))
        {
            actionActivator.SetActive(true);
        } else
        {
            actionActivator.SetActive(false);
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                chestSpriteRenderer.sprite = spriteStates[0];
                startDialogueSetup.SetActive(true);
                openDialogueSetup.SetActive(false);
                emptyDialogueSetup.SetActive(false);
                break;
            case "CanOpen":
                chestSpriteRenderer.sprite = spriteStates[0];
                startDialogueSetup.SetActive(false);
                openDialogueSetup.SetActive(true);
                emptyDialogueSetup.SetActive(false);
                break;
            case "Empty":
                chestSpriteRenderer.sprite = spriteStates[1];
                startDialogueSetup.SetActive(false);
                openDialogueSetup.SetActive(false);
                emptyDialogueSetup.SetActive(true);
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
