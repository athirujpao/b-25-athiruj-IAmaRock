using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // shared properties for health and rock count for encap
    protected int health;
    protected int rockCount;
    

    

    // Shared methods for picking up rocks, throwing them, and parrying shared to enemy
    public abstract void PickUpRock();
    public abstract void ThrowRock(Vector2 direction); // 
    public abstract void TransformToRockForm();
}

