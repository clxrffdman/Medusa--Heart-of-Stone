using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WalkingNpc : IState
{
    protected NpcController npcController;
    protected Animator animator;

    public WalkingNpc(NpcController npcController, Animator animator)
    {
        this.npcController = npcController;
        this.animator = animator;
    }

    public void Enter()
    {
        npcController.isIdle = false;
        npcController.isWalking = true;
        npcController.isFrozen = false;
        npcController.isSceneControlled = false;
        npcController.isWandering = false;
        npcController.isInvestigating = false;

        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", true);
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
