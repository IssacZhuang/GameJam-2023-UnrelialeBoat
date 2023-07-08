using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vocore;

/// <summary>
/// This script enables simple horizontal and vertical movement for an object.
/// Attach this script to an object and use "WASD" or arrow keys to move the object.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;                     // The base speed of the player
    public float accelarateDuration = 1.5f;      // The duration for accelerating the player's speed
    public float stopMovingDecayFactor = 0.1f;   // The factor to decay the player's speed when not moving
    public float[] speedBizerLerpCurveParam = new float[4] { 0f, 0.39f, 0.68f, 1.08f }; // Parameters for the speed curve

    private float buttonPressTime;                // Records the time when a button is pressed
    private bool isMoving;                        // Indicates whether the player is currently moving
    private Rigidbody2D rb;                       // The Rigidbody2D component of this object
    private Animator animator;                     // The Animator component of this object

    private Func<float, float> speedCurve;         // Function for calculating the speed curve

    private void OnValidate()
    {
        // Generate the speed curve based on the provided parameters
        speedCurve = UtilsCurve.GenerateBizerLerpCurve(speedBizerLerpCurveParam[0], speedBizerLerpCurveParam[1], speedBizerLerpCurveParam[2], speedBizerLerpCurveParam[3]);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        buttonPressTime = 0f;
    }

    private void Update()
    {
        // Check if any WASD or arrow keys are pressed
        if (MovementKeyDown())
        {
            if (!isMoving)
            {
                buttonPressTime = Time.time;  // Record the time when the first WASD key is pressed
            }
            isMoving = true;
        }

        // Decelerate the player's speed when the WASD or arrow key is released (to provide a smoother feel)
        if (MovementKeyUp())
        {
            if (!AnyMovementKeyDown())
            {
                isMoving = false;
                //// Reset all animation triggers
                animator.ResetTrigger("LeftRun");
                animator.ResetTrigger("RightRun");
                animator.ResetTrigger("UpRun");
                animator.ResetTrigger("DownRun");
                animator.SetTrigger("Idle");
            }
        }
    }

    private void FixedUpdate()
    {
        // Code for movement
        if (isMoving)
        {
            // Get the horizontal and vertical input for movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Apply a small deadzone
            if (Mathf.Abs(moveHorizontal) > 0.01f || Mathf.Abs(moveVertical) > 0.01f)
            {
                // Calculate the movement speed
                float pressDuration = Time.time - buttonPressTime;                                       // Calculate how long a button has been held
                float timeValue = GetNormalizedTimeClamp01(pressDuration, accelarateDuration);         // Normalize the time to a value between 0 and 1
                float curveValue = speedCurve(timeValue);                                               // Get the value from the speed curve
                float moveSpeed = curveValue * speed;                                                   // Multiply by the base speed to get the final speed

                // Apply the movement speed
                Vector2 movement = new Vector2(moveHorizontal, moveVertical) * moveSpeed;
                rb.velocity = movement;


                // Switch to the appropriate animation based on the movement direction
                if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
                {
                    if (moveHorizontal < 0)
                    {
                        animator.SetTrigger("LeftRun");
                    }
                    else
                    {
                        animator.SetTrigger("RightRun");
                    }
                }
                else
                {
                    if (moveVertical > 0)
                    {
                        animator.SetTrigger("UpRun");
                    }
                    else
                    {
                        animator.SetTrigger("DownRun");
                    }
                }
            }
        }
        else
        {
            rb.velocity = rb.velocity * stopMovingDecayFactor;  // Reset the momentum
        }
    }

    private bool MovementKeyDown()
    {
        // Check if any WASD or arrow keys are pressed down
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            return true;
        }
        return false;
    }

    private bool MovementKeyUp()
    {
        // Check if any WASD or arrow keys are released
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
            Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            return true;
        }
        return false;
    }

    private bool AnyMovementKeyDown()
    {
        // Check if any WASD or arrow keys are being held down
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            return true;
        }
        return false;
    }

    // Get the normalized time value clamped between 0 and 1
    private static float GetNormalizedTimeClamp01(float TimeToNorm, float NormFactor)
    {
        float timeSinceButtonPressed = TimeToNorm / NormFactor; // Calculate the time since the button was pressed
        float normalizedTime = Mathf.Clamp01(timeSinceButtonPressed); // Clamp the time value between 0 and 1
        return normalizedTime;
    }
}
