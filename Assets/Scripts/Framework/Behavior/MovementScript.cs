using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a simple script to move object horizontally and vertically
/// Attach this script to object, use "wasd" to move the attached object
/// </summary>
public class MovementScript : MonoBehaviour
{
    // Movement speed
    public float moveSpeed = 5f;
    private void Update()
    {
        // Check user input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Set movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // Apply movement
        transform.Translate(movement);
    }
}
