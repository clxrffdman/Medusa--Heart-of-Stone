using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowInteractable : Interactable
{
    [HideInInspector]
    public EnvironmentalObject environmentalObject;
    public GameObject cowHungry;
    public GameObject cowEating;
    public GameObject hungryCowDialogueSetup;
    public GameObject feedCowDialogueSetup;
    public GameObject needPotDialogueSetup;
    public GameObject canMilkDialogueSetup;
    public GameObject gotMilkDialogueSetup;
    public GameObject farmerNoMilkDialogueSetup;
    public GameObject farmerCowFedDialogueSetup;

    public enum ObjectStates { Start, CanFeed, NoPot, CanMilk, GotMilk };

    public override void Start()
    {
        environmentalObject = GetComponent<EnvironmentalObject>();
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());

        UpdateState();
        base.Start();
    }

    public override void UpdateState()
    {
        if (TaskManager.Instance.CheckTaskComplete("foundGrass"))
        {
            environmentalObject.SetCurrentState(ObjectStates.CanFeed.ToString());
        }

        // fed_cow is task being set in the dialogue.
        if (TaskManager.Instance.CheckTaskComplete("fedCow"))
        {
            if (TaskManager.Instance.CheckTaskComplete("foundPot"))
            {
                environmentalObject.SetCurrentState(ObjectStates.CanMilk.ToString());
            }
            else
            {
                environmentalObject.SetCurrentState(ObjectStates.NoPot.ToString());
            }
            
        }

        // got_milk is task being set in the dialogue.
        if (TaskManager.Instance.CheckTaskComplete("gotMilk"))
        {
            environmentalObject.SetCurrentState(ObjectStates.GotMilk.ToString());
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                cowHungry.SetActive(true);
                hungryCowDialogueSetup.SetActive(true);
                feedCowDialogueSetup.SetActive(false);

                cowEating.SetActive(false);

                farmerNoMilkDialogueSetup.SetActive(true);
                farmerCowFedDialogueSetup.SetActive(false);
                break;
            case "CanFeed":
                cowHungry.SetActive(true);
                hungryCowDialogueSetup.SetActive(false);
                feedCowDialogueSetup.SetActive(true);

                cowEating.SetActive(false);

                farmerNoMilkDialogueSetup.SetActive(true);
                farmerCowFedDialogueSetup.SetActive(false);
                break;
            case "NoPot":
                cowHungry.SetActive(false);

                cowEating.SetActive(true);
                needPotDialogueSetup.SetActive(true);
                canMilkDialogueSetup.SetActive(false);
                gotMilkDialogueSetup.SetActive(false);

                farmerNoMilkDialogueSetup.SetActive(false);
                farmerCowFedDialogueSetup.SetActive(true);

                break;
            case "CanMilk":
                cowHungry.SetActive(false);

                cowEating.SetActive(true);
                needPotDialogueSetup.SetActive(false);
                canMilkDialogueSetup.SetActive(true);
                gotMilkDialogueSetup.SetActive(false);

                farmerNoMilkDialogueSetup.SetActive(false);
                farmerCowFedDialogueSetup.SetActive(true);

                break;
            case "GotMilk":
                cowHungry.SetActive(false);

                cowEating.SetActive(true);
                needPotDialogueSetup.SetActive(false);
                canMilkDialogueSetup.SetActive(false);
                gotMilkDialogueSetup.SetActive(true);

                farmerNoMilkDialogueSetup.SetActive(false);
                farmerCowFedDialogueSetup.SetActive(true);

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
