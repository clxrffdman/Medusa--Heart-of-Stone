using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigatingNpc : IState
{
    private NpcController npcController;
    private Animator animator;

    public InvestigatingNpc(NpcController npcController, Animator animator)
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
        npcController.isWandering = false;
        npcController.isInvestigating = true;

        npcController.aiPath.maxSpeed = npcController.investigateSpeed;
        npcController.SetAStarDestination(npcController.targetDestination);
        npcController.StartAStarPathfinding();
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", true);
        
    }

    public void Execute()
    {
        npcController.SetFacingDirection();

        if (npcController.CheckIfEndReached())
        {
            if (!npcController.shouldStopWanderingAfterInvestigating)
            {
                npcController.ChangeStateToWandering();
            }
            else
            {
                npcController.ChangeStateToIdle();
            }
            
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
        npcController.aiPath.maxSpeed = npcController.baseSpeed;
    }
}
