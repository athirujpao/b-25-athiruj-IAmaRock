using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Rock : MonoBehaviour , ICollectible
{
    
    public void Collect()
    {
        Debug.Log("Rock collected.");
        Destroy(gameObject); // Remove from scene when collected
    }
}

