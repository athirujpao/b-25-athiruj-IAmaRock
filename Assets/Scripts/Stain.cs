using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Stain : ICleanable
{ public float health;
    public float Health
        { get{ return health; }  set { health = value; } }

    public float cleanRate = 10f;

    public void StartCleaning() 
    {
        Debug.Log("Cleaing started on stain");
    }
    public void ApplyPressure(float pressure) 
    { 
        // Reduce health(Stain) by multiplied to sim the Cleaning process
        health -= cleanRate * pressure;
        Debug.Log($"Cleaning in progress. Current health: {health}");

        if (health <= 0) 
        {
            FinishCleaning();
        }
    }

    public void FinishCleaning() 
    {
        Debug.Log("Cleaing is done. Stain removed.");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
