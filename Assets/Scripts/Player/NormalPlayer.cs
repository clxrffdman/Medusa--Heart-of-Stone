using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalPlayer : IState
{
    protected Animator animator;
    protected PlayerController playerController;

    public NormalPlayer(PlayerController playerController, Animator animator)
    {
        this.animator = animator;
        this.playerController = playerController;
    }

    public void Enter()
    {
        
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {

    }
}

