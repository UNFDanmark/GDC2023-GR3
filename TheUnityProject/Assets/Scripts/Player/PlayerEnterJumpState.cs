using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterJumpState : State
{
    private float enterJumpDelay = 0.15f;

    private float timer;
    
    public PlayerEnterJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Tick(float delta)
    {
        timer += delta;
    }

    public override void Exit()
    {

    }
}
