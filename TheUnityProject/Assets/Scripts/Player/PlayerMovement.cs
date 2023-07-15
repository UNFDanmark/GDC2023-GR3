using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string GROUND_TAG = "Ground";
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float defaultGravity;
    [SerializeField] private float glidingGravity;
    [SerializeField] private int midAirJumps;
    [SerializeField] private Rigidbody rb;

    private int jumpsSinceGrounded = 0;
    private int groundsTouched = 0;
    private bool isGrounded = false;
    private bool isGliding = false;
    
    private void Update()
    {
        float turnInput = Input.GetAxis("Horizontal");
        float moveInput = Input.GetAxis("Vertical");
        
        transform.Rotate(0, turnInput * turnSpeed * Time.deltaTime, 0);
        
        rb.velocity = (transform.forward * (moveInput * moveSpeed) + Vector3.up * rb.velocity.y);
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Jump from ground
            
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        }
        else if (Input.GetButtonDown("Jump") && jumpsSinceGrounded < midAirJumps)
        {
            //Jump in air
            jumpsSinceGrounded++;
            
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && !isGrounded)
        {
            //Start gliding in air
            isGliding = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && isGliding)
        {
            //Stop gliding
            
            isGliding = false;
        }

        if (isGliding)
        {
            rb.velocity += Vector3.up * (glidingGravity * Time.deltaTime);
        }
        else
        {
            rb.velocity += Vector3.up * (defaultGravity * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GROUND_TAG))
        {
            groundsTouched++;
            jumpsSinceGrounded = 0;
            isGliding = false;
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
}
