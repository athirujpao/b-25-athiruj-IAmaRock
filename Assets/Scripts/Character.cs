using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    
    protected int health;
    protected int rockCount;
    protected int maxRocks = 5;

    protected bool isRockForm = false;

    // Shared methods for picking up rocks, throwing them, and parrying
    public abstract void PickUpRock();
    public abstract void ThrowRock(Vector2 direction);
    public abstract void TransformToRockForm();
}

