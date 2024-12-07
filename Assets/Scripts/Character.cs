using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour 
{
    protected int health;
    protected int rockCount;

    public float pickupRadius = 1.5f;  // Radius to pick up rocks
    public float pickupCooldown = 1f;
    private float lastPickupTime = 0f;

    public virtual void PickUpRock() { }
    public virtual void ThrowRock() { }

    public void UpdatePickupCooldown()
    {
        if (Time.time >= lastPickupTime + pickupCooldown)
        {
            lastPickupTime = Time.time;
        }
    }
    public bool CanPickUpRock()
    {
        return Time.time >= lastPickupTime + pickupCooldown;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
            Destroy(gameObject);
        }
    }
    protected virtual void Die()
    {
        Debug.Log($"{name} has died.");
    }

    
}

