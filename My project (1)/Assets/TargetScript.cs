using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private Color targetColor; // Store the target's color
    private Vector3 originalPosition; // Store the original position of the target
    private bool isResetting = false; // Flag to track if the target is resetting

    public float resetSpeed = 2f; // Speed at which the target resets (adjust as needed)

    void Start()
    {
        // Store the original position of the target
        originalPosition = transform.position;

        // Set the target's initial color to a random color from {red, blue, green}
        SetRandomColor();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the target has collided with a bullet
        BulletScript bullet = other.gameObject.GetComponent<BulletScript>();

        if (bullet != null)
        {
            // Check if the bullet's color matches the target's color
            if (bullet.GetColor() == targetColor)
            {
                // Start resetting the target to its original position
                isResetting = true;

                // Change the target's color when hit
                SetRandomColor();
            }

            // Destroy the bullet regardless of color
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        // Check if the target is currently resetting
        if (isResetting)
        {
            // Move the target back to its original position smoothly
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, resetSpeed * Time.deltaTime);

            // Check if the target has reached its original position
            if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
            {
                isResetting = false; // Resetting is complete
            }
        }
    }

    private void SetRandomColor()
    {
        // Define an array of allowed colors (red, blue, green)
        Color[] allowedColors = new Color[] { Color.red, Color.blue, Color.green };

        // Choose a random color from the array
        targetColor = allowedColors[Random.Range(0, allowedColors.Length)];

        // Update the target's color
        GetComponent<Renderer>().material.color = targetColor;
    }
}


