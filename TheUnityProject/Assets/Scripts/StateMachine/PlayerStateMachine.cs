using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private const string GROUND_TAG = "Ground";
    
    public float moveSpeed;
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

    private State currentState;
    
    private void Start()
    {
        currentState = new PlayerDefaultState(this);
    }
    
    private void Update()
    {
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
    }
}
