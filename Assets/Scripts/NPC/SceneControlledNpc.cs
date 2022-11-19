using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneControlledNpc : IState
{
    private NpcController npcController;
    private Animator animator;

    public SceneControlledNpc(NpcController npcController, Animator animator)
    {
        this.npcController = npcController;
        this.animator = animator;
    }

    public void Enter()
    {
        npcController.isIdle = false;
        npcController.isWalking = false;
        npcController.isFrozen = false;
        npcController.isSceneControlled = true;
        npcController.isWandering = false;
        npcController.isInvestigating = false;

        if (npcController.targetDestination != null)
        {
            npcController.SetAStarDestination(npcController.targetDestination);
            npcController.StartAStarPathfinding();
            npcController.hasReachedDestination = false;
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
        }
    }

    public void Exit()
    {
        if (npcController.targetDestination != null)
        {
            npcController.StopAStarPathfinding();
            npcController.hasReachedDestination = true;
            npcController.targetDestination = null;
            npcController.aIDestinationSetter.targetASTAR = null;
        }
    }
}

