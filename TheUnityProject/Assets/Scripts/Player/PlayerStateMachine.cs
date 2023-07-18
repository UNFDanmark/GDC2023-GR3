using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public static PlayerStateMachine Instance { get; private set; }
    
    private const string GROUND_TAG = "Ground";
    
    public float defaultMoveSpeed;
    public float defaultAccelerationConstant;
    public float roundMoveDownSpeedLimit;
    public float turnSpeed;
    public float glidingTurnSpeed;
    public float jumpSpeed;
    public float defaultGravity;
    public float glidingGravity;
    public int midAirJumps;
    public float minGlidingVerticalVel;
    public float maxHorizontalSpeed;
    public float glidingAccelerationPerSecond;
    public Rigidbody rb;

    public int jumpsSinceGrounded = 0;
    public int groundsTouched = 0;
    public float forwardVel;
    public float verticalVel;
    public bool isGrounded = false;

    public enum DeaccelerationMethod
    {
        constant, 
        LERP,
    }

    [Header("Gliding to default deacceleration")]
    public DeaccelerationMethod deaccelerationMethod;

    [Header("Constant")] 
    public float constantDeaccelerationPerSecond;

    [Header("LERP")] 
    public float groundedLerpDampeningConstant;

    public float groundedLerpConstantDeaccelerationPerSecond;
    public float inAirLerpDampeningConstant;

    public float inAirLerpConstantDeaccelerationPerSecond;

    private State currentState;
    private bool gameStarted;

    private void Awake()
    {
        gameStarted = false;
        Instance = this;
    }

    public void StartPlayer()
    {
        gameStarted = true;
        currentState = new PlayerDefaultState(this);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update()
    {
        if (!gameStarted) return;
        
        currentState.Tick(Time.deltaTime);
    }
    
    public void ChangeState(State newState)
    {
        currentState?.Exit();

        currentState = newState;
        
        currentState?.Enter();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GROUND_TAG))
        {
            groundsTouched++;
            jumpsSinceGrounded = 0;
        }

        isGrounded = groundsTouched > 0;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GROUND_TAG))
        {
            groundsTouched--;
        }
        
        isGrounded = groundsTouched > 0;
    }

    public void AddVerticalVelocity(float newVel)
    {
            verticalVel += newVel;
            rb.velocity = new Vector3(rb.velocity.x, verticalVel, rb.velocity.z);
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
