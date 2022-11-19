using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrozenNpc : IState
{
    protected NpcController npcController;
    protected Animator animator;

    public FrozenNpc(NpcController npcController, Animator animator)
    {
        this.npcController = npcController;
        this.animator = animator;
    }

    public void Enter()
    {
        npcController.isIdle = false;
        npcController.isWalking = false;
        npcController.isFrozen = true;
        npcController.isSceneControlled = false;
        npcController.isWandering = false;
        npcController.isInvestigating = false;

        npcController.npcRigidBody.velocity = Vector2.zero;
        animator.SetBool("isIdle", true);
        animator.SetBool("isWalking", false);
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}