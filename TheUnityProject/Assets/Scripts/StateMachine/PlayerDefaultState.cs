using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultState : State
{
    public PlayerDefaultState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
    }

    public override void Tick(float delta)
    {
        float turnInput = Input.GetAxis("Horizontal");
        float moveInput = Input.GetAxis("Vertical");
        
        stateMachine.transform.Rotate(0, turnInput * stateMachine.turnSpeed * Time.deltaTime, 0);
        
        stateMachine.forwardVel = moveInput * stateMachine.moveSpeed;
        
        if (Input.GetButtonDown("Jump"))
        {
            if (stateMachine.isGrounded)
            {
                stateMachine.verticalVel = stateMachine.jumpSpeed;
            }
            else if (stateMachine.jumpsSinceGrounded < stateMachine.midAirJumps)
            {
                stateMachine.verticalVel = stateMachine.jumpSpeed;
                stateMachine.jumpsSinceGrounded++;
            }
            
            stateMachine.ChangeState(new PlayerGlidingState(stateMachine));
            return;
        }

        if (!stateMachine.isGrounded)
        {
            stateMachine.verticalVel += stateMachine.defaultGravity * delta;
        }
        else
        {
            stateMachine.verticalVel = stateMachine.rb.velocity.y;
        }

        Vector3 vel = stateMachine.transform.forward * stateMachine.forwardVel + stateMachine.transform.up * stateMachine.verticalVel;
        stateMachine.rb.velocity = vel;
    }

    public override void Exit()
    {
    }
}
