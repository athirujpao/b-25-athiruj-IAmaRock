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
    public float lastPickupTime = 0f;     // needed to add this because enemy pick up rock too fast steal all rock from player and i dont want to add radius for pick up in both of them it make the flow of the game feel unhinged 


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

