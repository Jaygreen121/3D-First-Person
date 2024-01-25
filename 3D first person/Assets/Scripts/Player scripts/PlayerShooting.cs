using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    private Vector3 launchPoint;
    private float projectileSpeed = 50f;
    private float maxDistance = 50f;

    private List<GameObject> currentProjectiles = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Shoot();
        }

        destroyProjectiles();
    }

    void Shoot()
    {

        launchPoint = transform.position;

        GameObject newProjectile = Instantiate(projectilePrefab, launchPoint, transform.rotation);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;

        // Adding them to the array
        currentProjectiles.Add(newProjectile);
    }

    void destroyProjectiles()
    {
        for (int i = 0; i < currentProjectiles.Count; i++)
        {
            GameObject projectile = currentProjectiles[i];
            float distanceTravelled = Vector3.Distance(launchPoint, projectile.transform.position);

            if (distanceTravelled >= maxDistance) // or when they collide with something
            {
                Destroy(projectile);

                // Then remove it from the array
                currentProjectiles.RemoveAt(i);
            }
        }
    }
}
