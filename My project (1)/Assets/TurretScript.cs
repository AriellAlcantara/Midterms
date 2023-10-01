using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float shootingRadius = 10f;
    public float rotationSpeed = 60f;
    [SerializeField] private float timer = 5f;

    private float bulletTime;
    private Renderer turretRenderer;

    private bool isShooting = false;
    private GameObject currentBullet; // Store the currently active bullet

    void Start()
    {
        bulletTime = timer;
        turretRenderer = GetComponent<Renderer>();
        SpawnBullet(); // Spawn the initial bullet inside the turret
    }

    void Update()
    {
        // Check for objects with the "Target" tag within the shooting radius
        Collider[] targets = Physics.OverlapSphere(transform.position, shootingRadius);

        foreach (Collider target in targets)
        {
            if (target.CompareTag("Target"))
            {
                // Rotate towards the target
                RotateTowardsTarget(target.transform);

                // Shoot at the target
                if (!isShooting)
                {
                    ShootAtTarget(target.transform);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            ChangeBulletColor();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRadius);
    }

    void ChangeBulletColor()
    {
        Color[] allowedColors = new Color[] { Color.red, Color.blue, Color.green };
        Color bulletColor = allowedColors[Random.Range(0, allowedColors.Length)];

        turretRenderer.material.color = bulletColor;

        // Destroy the current bullet before changing color
        Destroy(currentBullet);

        // Spawn a new bullet with the updated color
        SpawnBullet();
    }

    void ShootAtTarget(Transform target)
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime <= 0)
        {
            bulletTime = timer;

            if (currentBullet != null)
            {
                currentBullet.GetComponent<BulletScript>().SetTarget(target);
                currentBullet.GetComponent<BulletScript>().SetSpeed(bulletSpeed);
                currentBullet.GetComponent<BulletScript>().Shoot();
                isShooting = true;
            }
        }
    }

    void RotateTowardsTarget(Transform target)
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
    }

    void SpawnBullet()
    {
        // Instantiate a new bullet inside the turret
        currentBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        currentBullet.GetComponent<BulletScript>().SetSpeed(0f); // Set initial speed to 0 (bullet is not moving)
    }
}
