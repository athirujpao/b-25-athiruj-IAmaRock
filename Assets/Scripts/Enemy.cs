using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : Character , IAttackable
{
    public GameObject RockPrefab;
    
    private Transform playerTransform;

    public Transform bulletSpawnPoint;
    public float shootCooldown = 2f; 
    private float lastShotTime = 0f;
    public float moveSpeed = 2f;

    

    private void Start()
    {
        health = 3;
        rockCount = 1; // Enemies start with one rock
        playerTransform = GameObject.FindWithTag("Player").transform;
          
    }

    private void Update()
    {
        MoveTowardsPlayer();  // Move the enemy toward the player

        // Only shoot if cooldown has passed
        if (Time.time >= lastShotTime + shootCooldown)
        {
            ThrowRock();
            lastShotTime = Time.time;
        }

        // If no rocks are held, pick up rocks from the player (if player is holding one)
        if (rockCount == 0 && CanPickUpRock())
        {
            PickUpRock();  // Enemy automatically picks up rocks
        }

        // Only pick up rocks if cooldown has passed
        if (Time.time >= lastPickupTime + pickupCooldown)
        {
            PickUpRock();  // Use the inherited method to pick up a rock
        }
    }
    private void MoveTowardsPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        if (direction.magnitude > 3f)  // Only move if not too close
        {
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        // Dodging logic (simple: move away if too close to player)
        else if (Vector3.Distance(transform.position, playerTransform.position) < 2f)
        {
            transform.position -= direction * moveSpeed * Time.deltaTime;  // Dodge
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
                
                rockCount++;  // Increment rock count
                
                Debug.Log("Enemy picked up a rock.");
                lastPickupTime = Time.time;  // Set the time for the last pickup
                return;  // Only pick up one rock at a time
            }
                else
                {
                    Debug.Log("Enemy has no rocks to throw.");
                }
        }
    }
}

    public override void ThrowRock()
    {
        if (rockCount > 0)
        {
            Vector2 directionToPlayer = (playerTransform.position - bulletSpawnPoint.position).normalized;
            GameObject rockInstance = Instantiate(RockPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rock rockScript = rockInstance.GetComponent<Rock>();
            rockScript.Throw(directionToPlayer);  // Shoot toward the player and both are the at this point why doesnt not work with both
            rockCount--;
            
        }

    }

    

    protected override void Die()
    {
        base.Die();
        Debug.Log("Enemy has died!");
    }




}
