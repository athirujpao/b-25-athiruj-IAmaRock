using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Rock : Weapon
{
    public float throwForce = 10f; //Force appiled to throw the rock 
    public void Start()
    {
        weaponName = "Rock";
        durability = 3;  // Can be pick up three times
    }

    public override void PickUp(Vector2 direction)
    {
        if (durability > 0)
        {
            GameObject rockInstance = Instantiate(this.gameObject, transform.position, Quaternion.identity);
            Rigidbody2D rb = rockInstance.AddComponent<Rigidbody2D>();
            rb.AddForce(direction * throwForce,ForceMode2D.Impulse);

            Debug.Log($"Throwing {weaponName}. Durability left: {durability - 1}");
            durability--;
        }
        else
        {
            Debug.Log($"{weaponName} is broken.");
        }
    }
}
