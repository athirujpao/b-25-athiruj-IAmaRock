using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Rock : MonoBehaviour , ICollectible 
{

    public float throwForce = 10f;
    private Rigidbody2D rb;
    private Vector2 target;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;   // Disable gravity to prevent falling
    }
    

    public void Throw(Vector2 direction)
    {
        if (rb != null)
        {
            rb.isKinematic = false;  
            rb.velocity = direction * throwForce;  // Apply force in the specified direction
            Debug.Log("Rock thrown.");
        }
    }

    // Called when the rock collides with something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Rock hit: {collision.gameObject.name}");

        // Check if the rock hit an enemy or player
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to the enemy or player
            Character character = collision.gameObject.GetComponent<Character>();
            if (character != null)
            {
                character.TakeDamage(1);  
            }
        }

        Destroy(gameObject);  
    }

    
    public void Collect() // if not have this enemy wont shoot method for enemy pickup rock and shoot
    {
        
    }

    
    public bool IsOnGround()
    {
        return rb.IsSleeping(); // Checks if the Rigidbody is resting on the ground (not moving)
    }
    
}

