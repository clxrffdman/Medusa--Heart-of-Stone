using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellInteractable : Interactable
{
    private bool isConvergingOnBell;

    public EnvironmentalObject environmentalObject;

    public GameObject hingeFull;
    public GameObject bellAttached;
    public Animator bellAnimator;
    public NpcPatrol[] npcPatrol;
    public NpcController[] npcControllers;
    public Transform[] destinations;

    public GameObject startDialogueSetup;
    public GameObject openDialogueSetup;
    public GameObject ringBellDialogueSetup;

    public enum ObjectStates { Start, CanAttach, Attached, BellRung};

    public void OnEnable()
    {
        ResetStateToStart();
    }

    public override void Start()
    {
        environmentalObject = GetComponent<EnvironmentalObject>();
        
        base.Start();
    }

    public override void Update()
    {
       
    }

    public override void UpdateState()
    {
        if (TaskManager.Instance.CheckTaskComplete("gotBell"))
        {
            print("got bell");
            environmentalObject.SetCurrentState(ObjectStates.CanAttach.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete("bellAttached"))
        {
            print("bell attached");
            environmentalObject.SetCurrentState(ObjectStates.Attached.ToString());
        }

        if (TaskManager.Instance.CheckTaskComplete("bellRung"))
        {
            print("bell has been rung");
            environmentalObject.SetCurrentState(ObjectStates.BellRung.ToString());
        }

        switch (environmentalObject.currentState)
        {
            case "Start":
                startDialogueSetup.SetActive(true);
                openDialogueSetup.SetActive(false);
                ringBellDialogueSetup.SetActive(false);
                hingeFull.SetActive(true);
                bellAttached.SetActive(false);
                break;
            case "CanAttach":
                startDialogueSetup.SetActive(false);
                openDialogueSetup.SetActive(true);
                ringBellDialogueSetup.SetActive(false);
                hingeFull.SetActive(true);
                bellAttached.SetActive(false);
                break;
            case "Attached":
                startDialogueSetup.SetActive(false);
                openDialogueSetup.SetActive(false);
                ringBellDialogueSetup.SetActive(true);
                hingeFull.SetActive(false);
                bellAttached.SetActive(true);
                break;
            case "BellRung":
                startDialogueSetup.SetActive(false);
                openDialogueSetup.SetActive(false);
                ringBellDialogueSetup.SetActive(false);
                bellAnimator.SetBool("isRinging", true);
                if (!isConvergingOnBell)
                {
                    MakeNpcGoToBell();
                }
                break;
            default:
                break;
        }
    }

    public override void ResetStateToStart()
    {
        print("resetting state");
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        bellAnimator.SetBool("isRinging", false);
        isConvergingOnBell = false;

        TaskManager.Instance.SetTaskAsComplete("gotBell", false);
        TaskManager.Instance.SetTaskAsComplete("bellAttached", false);
        TaskManager.Instance.SetTaskAsComplete("bellRung", false);
        UpdateState();
    }

    private void MakeNpcGoToBell()
    {
        print("making npc go to bell");
        for (int i = 0; i < npcControllers.Length; i++)
        {
            npcControllers[i].shouldStopWanderingAfterInvestigating = true;
            npcControllers[i].targetDestination = destinations[i];
            npcControllers[i].ChangeStateToInvestigating();
            npcPatrol[i].StopPatrolling();
        }

        isConvergingOnBell = true;

    }
}
