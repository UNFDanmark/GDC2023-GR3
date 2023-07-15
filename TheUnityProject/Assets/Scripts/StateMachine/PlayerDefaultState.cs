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
        
        stateMachine.rb.velocity = (stateMachine.transform.forward * (moveInput * stateMachine.moveSpeed) + Vector3.up * stateMachine.rb.velocity.y);
        
        if (Input.GetButtonDown("Jump") && stateMachine.isGrounded)
        {
            //Jump from ground
            
            stateMachine.rb.velocity = new Vector3(stateMachine.rb.velocity.x, stateMachine.jumpSpeed, stateMachine.rb.velocity.z);
        }
        else if (Input.GetButtonDown("Jump") && stateMachine.jumpsSinceGrounded < stateMachine.midAirJumps)
        {
            //Jump in air
            stateMachine.jumpsSinceGrounded++;
            
            stateMachine.rb.velocity = new Vector3(stateMachine.rb.velocity.x, stateMachine.jumpSpeed, stateMachine.rb.velocity.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && !stateMachine.isGrounded)
        {
            stateMachine.ChangeState(new PlayerGlidingState(stateMachine));
            return;
        }

        stateMachine.rb.velocity += Vector3.up * (stateMachine.defaultGravity * Time.deltaTime);
    }

    public override void Exit()
    {
    }
}
