using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character , IAttackable
{
    public GameObject rockPrefab;      
    public Camera mainCamera;          
    private GameObject currentRock;    // The currently held rock
    private Rock rockScript;           // The Rock script for the held rock
    private bool isHoldingRock = false; 

    private void Start()
    {
        health = 3; // Starting health
        rockCount = 0; // No rocks at the start
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Left mouse button (Mouse0) to pick up or throw the rock
        if (Input.GetMouseButtonDown(0) && !isHoldingRock)  // Pick up the rock
        {
            TryPickUpRock();
        }
        else if (Input.GetMouseButton(0) && isHoldingRock)  // While holding, aim the rock
        {
            AimRock();
        }
        else if (Input.GetMouseButtonUp(0) && isHoldingRock)  // Release to throw the rock
        {
            ThrowRock();
        }
    }

    private void TryPickUpRock()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D rockCollider = Physics2D.OverlapPoint(mousePosition);

        if (rockCollider != null && rockCollider.CompareTag("Rock"))
        {
            currentRock = rockCollider.gameObject;
            rockScript = currentRock.GetComponent<Rock>();
            isHoldingRock = true;
            Debug.Log("Picked up a rock.");
        }
    }

    private void AimRock()
    {
        if (currentRock != null && rockScript != null)
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

            // Position the rock slightly in front of the player
            currentRock.transform.position = (Vector2)transform.position + direction * 1f;
        }
    }

    private void ThrowRock()
    {
        if (currentRock != null && rockScript != null)
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            rockScript.Throw(direction);  // Call the Throw method from the Rock script
            rockCount--;
            
            currentRock = null;
            rockScript = null;
            isHoldingRock = false;
        }
    }

    

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Player has died.");
            // Implement death logic here
        }
        else
        {
            Debug.Log($"Player took {damage} damage. Health left: {health}");
        }
    }
}
