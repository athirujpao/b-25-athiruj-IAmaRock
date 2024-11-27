using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected string weaponName; //notthing for now 
    protected int durability;

    // abstract method to use weapon
    public abstract void PickUp(Vector2 direction);

    public int GetDurability() => durability;

}
