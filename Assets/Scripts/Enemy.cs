using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character , IAttackable
{
    public GameObject rockPrefab;
    private Transform playerTransform;
    private Vector3 spawnPoint;

    public float shootCooldown = 2f; 
    private float lastShotTime = 0f;

    public float pickupCooldown = 1f; // Cooldown time between rock pickups (in seconds)
                                      // needed to add this because enemy pick up rock too fast steal all rock from player and i dont want to add radius for pick up in both of them it make the flow of the game feel unhinged 
    private float lastPickupTime = 0f;

    private void Start()
    {
        health = 3;
        rockCount = 1; // Enemies start with one rock
        playerTransform = GameObject.FindWithTag("Player").transform;
        spawnPoint = transform.position;  
    }

    private void Update()
    {
        // Only shoot if cooldown has passed
        if (Time.time >= lastShotTime + shootCooldown)
        {
            if (playerTransform != null)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

                // Shoot rock at player if close enough
                if (distanceToPlayer < 8f && rockCount > 0)
                {
                    ShootRockAtPlayer();
                    lastShotTime = Time.time; // Reset the last shot time
                }
            }
        }

        // Only pick up rocks if cooldown has passed
        if (Time.time >= lastPickupTime + pickupCooldown)
        {
            PickUpRock();  // Use the inherited method to pick up a rock
        }
    }


public override void PickUpRock()
    {
    if (rockCount == 0)
    {
        // Find nearby rocks and pick them up
        Collider2D[] nearbyRocks = Physics2D.OverlapCircleAll(transform.position, 3f); // Smaller pickup radius

        foreach (Collider2D rockCollider in nearbyRocks)
        {
            if (rockCollider.CompareTag("Rock"))
            {
                Rock rockScript = rockCollider.GetComponent<Rock>();
                rockScript.Collect();  // Collect the rock
                rockCount++;  // Increment rock count
                
                Debug.Log("Enemy picked up a rock.");
                lastPickupTime = Time.time;  // Set the time for the last pickup
                return;  // Only pick up one rock at a time
            }
        }
    }
}

    public override void ThrowRock(Vector2 direction)
    {
        if (rockCount > 0)
        {
            GameObject rockInstance = Instantiate(rockPrefab, transform.position, Quaternion.identity);
            Rock rockScript = rockInstance.GetComponent<Rock>();
            rockScript.Throw(direction);
            rockCount--;
            
            Debug.Log("Enemy threw a rock.");
        }
    }

private void ShootRockAtPlayer()
{
    Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

    // Instantiate and throw the rock towards the player
    GameObject rockInstance = Instantiate(rockPrefab, transform.position, Quaternion.identity);
    Rock rockScript = rockInstance.GetComponent<Rock>();
    rockScript.Throw(directionToPlayer); // Pass direction to shoot towards the player

    rockCount--;  // Reduce the rock count
    
    Debug.Log("Enemy threw a rock towards the player.");
}

// Override TakeDamage to add Enemy-specific behavior (e.g., respawn after death)


    private void Respawn()
    {
        health = 3;
        rockCount = 1; 
        transform.position = spawnPoint; 
        
        Debug.Log("Enemy respawned.");
    }
}
