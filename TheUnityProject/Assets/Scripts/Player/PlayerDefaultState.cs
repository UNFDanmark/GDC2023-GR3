using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultState : State
{
    private int isFallingHash = Animator.StringToHash("isFalling");
    private int isGlidingHash = Animator.StringToHash("isGliding");
    private int locomotionHash = Animator.StringToHash("LocomotionBlend");
    private int jumpHash = Animator.StringToHash("Jump");

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
        
        stateMachine.transform.Rotate(0, turnInput * stateMachine.turnSpeed * delta, 0);

        HandleForwardMovement(moveInput, delta);
        
        if (Input.GetButtonDown("Jump"))
        {
            if (stateMachine.isGrounded)
            {
                stateMachine.verticalVel = stateMachine.jumpSpeed;
                stateMachine.animator.SetTrigger(jumpHash);
                
                AudioSource.PlayClipAtPoint(stateMachine.jumpClip, Camera.main.transform.position);
            }
            else if (stateMachine.jumpsSinceGrounded < stateMachine.midAirJumps)
            {
                stateMachine.verticalVel = stateMachine.jumpSpeed;
                stateMachine.jumpsSinceGrounded++;
                stateMachine.animator.SetTrigger(jumpHash);
                AudioSource.PlayClipAtPoint(stateMachine.doubleJumpClip, Camera.main.transform.position);
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
            stateMachine.verticalVel = 0f;
        }

        Vector3 vel = stateMachine.transform.forward * stateMachine.forwardVel + stateMachine.transform.up * stateMachine.verticalVel;
        stateMachine.rb.velocity = vel;

        UpdateAnimation(moveInput);
    }

    private void UpdateAnimation(float moveInput)
    {
        if (stateMachine.isGrounded)
        {
            stateMachine.animator.SetBool(isFallingHash, false);
            stateMachine.animator.SetFloat(locomotionHash, moveInput/2f + 0.5f);
        }
        else
        {
            stateMachine.animator.SetBool(isFallingHash, true);
        }
    }

    private void HandleForwardMovement(float moveInput, float delta)
    {
        
        if (stateMachine.forwardVel <= stateMachine.defaultMoveSpeed)
        {
            float newMove = Mathf.Lerp(stateMachine.forwardVel, moveInput * stateMachine.defaultMoveSpeed, delta * stateMachine.defaultAccelerationConstant);
            
            stateMachine.forwardVel = newMove;
        }
        else
        {
            if (stateMachine.deaccelerationMethod == PlayerStateMachine.DeaccelerationMethod.constant)
            {
                float desiredForwardVel = stateMachine.forwardVel;

                if (stateMachine.isGrounded)
                {
                    desiredForwardVel -= stateMachine.constantDeaccelerationPerSecond * delta * 2;   
                }
                else
                {
                    desiredForwardVel -= stateMachine.constantDeaccelerationPerSecond * delta;
                }
                
                desiredForwardVel = Mathf.Max(desiredForwardVel, stateMachine.defaultMoveSpeed);

                stateMachine.forwardVel = desiredForwardVel;
            }
            else
            {
                float desiredForwardVel;

                if (stateMachine.isGrounded)
                {
                    desiredForwardVel = Mathf.Lerp(stateMachine.forwardVel, stateMachine.defaultMoveSpeed, delta * stateMachine.groundedLerpDampeningConstant);
                    desiredForwardVel -= stateMachine.groundedLerpConstantDeaccelerationPerSecond * delta;
                }
                else
                {
                    desiredForwardVel = Mathf.Lerp(stateMachine.forwardVel, stateMachine.defaultMoveSpeed, delta * stateMachine.inAirLerpDampeningConstant);
                    desiredForwardVel -= stateMachine.inAirLerpConstantDeaccelerationPerSecond * delta;
                }
                
                desiredForwardVel = Mathf.Max(desiredForwardVel, stateMachine.defaultMoveSpeed);
                
                stateMachine.forwardVel = desiredForwardVel;
            }
        }
        
        //Set forward velocity to 0 if it negligible
        if (Mathf.Abs(stateMachine.forwardVel) < stateMachine.roundMoveDownSpeedLimit)
        {
            stateMachine.forwardVel = 0f;
        }
    }
    
    public override void Exit()
    {
        stateMachine.animator.SetBool(isFallingHash, false);
    }
}
