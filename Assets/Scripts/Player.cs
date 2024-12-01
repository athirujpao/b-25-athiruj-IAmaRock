using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character , IAttackable
{
    public GameObject rockPrefab;
    public Camera mainCamera;

    private void Start()
    {
        health = 3; // Starting health
        rockCount = 0; // No rocks at the start
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click to pick up or throw
        {
            if (rockCount > 0)
            {
                Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                ThrowRock(mousePosition);
            }
            else
            {
                PickUpRock();
            }
        }

        if (Input.GetMouseButtonDown(1)) // Right-click to transform to rock form
        {
            TransformToRockForm();
        }
    }

    public override void PickUpRock()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D rockCollider = Physics2D.OverlapPoint(mousePosition);

        if (rockCollider != null && rockCollider.CompareTag("Rock"))
        {
            Destroy(rockCollider.gameObject); // Remove rock from scene
            rockCount++;
            Debug.Log($"Picked up a rock. Rocks: {rockCount}");
        }
        else
        {
            Debug.Log("No rock to pick up here.");
        }
    }

    public override void ThrowRock(Vector2 direction)
    {
        if (rockCount > 0)
        {
            GameObject rock = Instantiate(rockPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = rock.AddComponent<Rigidbody2D>();
            rb.AddForce((direction - (Vector2)transform.position).normalized * 10f, ForceMode2D.Impulse);
            rockCount--;
            Debug.Log($"Threw a rock. Rocks left: {rockCount}");
        }
    }

    public override void TransformToRockForm()
    {
        if (rockCount > 0)
        {
            Debug.Log("Transformed to Rock Form - parry mode");
            rockCount--;
            StartCoroutine(ExitRockForm(2f)); // Stay in rock form for 2 seconds
        }
        else
        {
            Debug.Log("No rocks available for transformation.");
        }
    }

    private System.Collections.IEnumerator ExitRockForm(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Exited Rock Form.");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Player has died.");
            // Implement death logic here
        }
        else
        {
            Debug.Log($"Player took {damage} damage. Health left: {health}");
        }
    }
}
