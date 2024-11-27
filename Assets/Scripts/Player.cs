using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private List<Weapon> inventory;
    private int maxInventorySize;

    public Camera mainCamera;

    private void Start()
    {
        inventory = new List<Weapon>();
        maxInventorySize = 2;

        if (mainCamera == null) 
        {
            mainCamera = Camera.main;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        { 
            PickUpWeapon(); 
        }
    }

    private void PickUpWeapon()
    { 
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colli = Physics2D.OverlapPointAll(mousePosition);

        foreach (Collider2D collider in colli)
        { 
            Weapon weapon = collider.GetComponent<Weapon>();
            if (weapon != null && inventory.Count < maxInventorySize)
            {
                inventory.Add(weapon);
                Des
            }
        }

    }
}
