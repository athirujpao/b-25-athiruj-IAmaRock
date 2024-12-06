using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    protected int health;
    protected int rockCount;

    public virtual void PickUpRock() { }
    public virtual void ThrowRock(Vector2 direction) { }

    // Handle taking damage for both Player and Enemy
    

    // Update Rock Count UI for both Player and Enemy
    public void UpdateRockCountUI(Text rockCountText)
    {
        if (rockCountText != null)
        {
            rockCountText.text = $"Rocks: {rockCount}";
        }
    }
}

