using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlidingState : State
{
    public PlayerGlidingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    { }

    public override void Tick(float delta)
    {
        if (Input.GetButtonUp("Jump") || stateMachine.isGrounded && stateMachine.verticalVel <= 0)
        {
            stateMachine.ChangeState(new PlayerDefaultState(stateMachine));
            return;
        }

        float turnInput = Input.GetAxis("Horizontal");
        float turnAmount = turnInput * stateMachine.glidingTurnSpeed * delta;
        stateMachine.transform.Rotate(0, turnAmount , 0);

        //Quaternion newRotation = stateMachine.transform.rotation * Quaternion.Euler(new Vector3(0, turnInput * stateMachine.glidingTurnSpeed * delta, 0));

        //stateMachine.rb.MoveRotation(newRotation);
        
        if (stateMachine.verticalVel > 0)
        {
            stateMachine.verticalVel += stateMachine.defaultGravity * delta;
        }
        else
        {
            stateMachine.verticalVel += stateMachine.glidingGravity * delta;
        }
        stateMachine.verticalVel = Mathf.Clamp(stateMachine.verticalVel, stateMachine.minGlidingVerticalVel, Mathf.Infinity);
        
        stateMachine.forwardVel += stateMachine.forwardVel * stateMachine.glidingAccelerationPerSecond * delta;
        stateMachine.forwardVel = Mathf.Clamp(stateMachine.forwardVel, 0f, stateMachine.maxHorizontalSpeed);

        Vector3 vel = stateMachine.transform.forward * stateMachine.forwardVel + Vector3.up * stateMachine.verticalVel;
        stateMachine.rb.velocity = vel;
    }

    public override void Exit()
    { }
}
