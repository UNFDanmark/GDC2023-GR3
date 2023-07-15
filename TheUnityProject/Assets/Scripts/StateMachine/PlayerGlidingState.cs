using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlidingState : State
{
    //private Vector3 vel;
    private float forwardVel;
    private float verticalVel;
    
    public PlayerGlidingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //vel = stateMachine.rb.velocity;
        forwardVel = new Vector2(stateMachine.rb.velocity.x, stateMachine.rb.velocity.z).magnitude;
        verticalVel = stateMachine.rb.velocity.y;
    }

    public override void Tick(float delta)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) || stateMachine.isGrounded)
        {
            stateMachine.ChangeState(new PlayerDefaultState(stateMachine));
            return;
        }
        
        float turnInput = Input.GetAxis("Horizontal");
        stateMachine.transform.Rotate(0, turnInput * stateMachine.glidingTurnSpeed * delta, 0);

        if (verticalVel > 0)
        {
            verticalVel += stateMachine.defaultGravity * delta;
        }
        else
        {
            verticalVel += stateMachine.glidingGravity * delta;
        }
        verticalVel = Mathf.Clamp(verticalVel, stateMachine.minGlidingVerticalVel, Mathf.Infinity);

        
        forwardVel += forwardVel * stateMachine.glidingAccelerationPerSecond * delta;
        forwardVel = Mathf.Clamp(forwardVel, 0f, stateMachine.maxHorizontalSpeed);


        Vector3 vel = stateMachine.transform.forward * forwardVel + Vector3.up * verticalVel;
        stateMachine.rb.velocity = vel;
    }

    public override void Exit()
    {
        
    }
}
