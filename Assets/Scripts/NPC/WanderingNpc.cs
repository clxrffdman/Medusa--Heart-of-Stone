using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WanderingNpc : IState
{
    private NpcController npcController;
    private Animator animator;

    public WanderingNpc(NpcController npcController, Animator animator)
    {
        this.npcController = npcController;
        this.animator = animator;
    }

    public void Enter()
    {
        npcController.isIdle = false;
        npcController.isWalking = false;
        npcController.isFrozen = false;
        npcController.isSceneControlled = false;
        npcController.isWandering = true;
        npcController.isInvestigating = false;

        if (npcController.targetDestination != null)
        {
            npcController.SetAStarDestination(npcController.targetDestination);
            npcController.StartAStarPathfinding();
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", true);
        }
    }

    public void Execute()
    {
        npcController.SetFacingDirection();
        
        if (npcController.CheckIfEndReached())
        {
            npcController.ChangeStateToFrozen();
            if (npcController.targetDestination != null)
            {
                npcController.hasReachedDestination = true;
                npcController.targetDestination = null;
                npcController.aIDestinationSetter.targetASTAR = null;
            }
        }
    }

    public void Exit()
    {
        npcController.StopAStarPathfinding();
    }
}
