using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Color bulletColor; // Store the bullet's color
    private Transform target; // Store the target to shoot at
    private float speed; // Store the bullet speed
    private bool isShooting = false; // Track if the bullet is shooting

    private Renderer bulletRenderer; // Reference to the bullet's Renderer component

    private void Start()
    {
        // Get the Renderer component of the bullet
        bulletRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isShooting)
        {
            if (target != null)
            {
                // Move the bullet towards the target
                Vector3 direction = (target.position - transform.position).normalized;
                transform.Translate(direction * speed * Time.deltaTime);
            }
            else
            {
                // If the target is null (destroyed or out of range), stop shooting
                isShooting = false;
            }
        }
    }

    // Set the color of the bullet
    public void SetColor(Color color)
    {
        bulletColor = color;

        // Update the bullet's color via the Renderer component
        bulletRenderer.material.color = bulletColor;
    }

    public Color GetColor()
    {
        return bulletColor;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void Shoot()
    {
        isShooting = true;
    }
}



