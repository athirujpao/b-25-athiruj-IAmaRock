using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Rock : MonoBehaviour , ICollectible
{

    public float throwForce = 10f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;   // Disable gravity to prevent falling
    }

    public void Throw(Vector2 direction)
    {
        if (rb != null)
        {
            rb.isKinematic = false;  // Enable physics
            rb.velocity = direction * throwForce;  // Apply force in the specified direction
            Debug.Log("Rock thrown.");
        }
    }

    // Called when the rock collides with something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Optionally, check for specific collisions (like player, enemy, ground, etc.)
        Debug.Log($"Rock hit something: {collision.gameObject.name}");
        Destroy(gameObject);  // Destroy the rock on collision
    }

    // Collect the rock (e.g., when the player picks it up)
    public void Collect()
    {
        Destroy(gameObject);  // Destroy the rock when it is collected
    }

    // Check if the rock is on the ground (this method will return true if the rock is on the ground)
    public bool IsOnGround()
    {
        return rb.IsSleeping(); // Checks if the Rigidbody is resting on the ground (not moving)
    }
}

