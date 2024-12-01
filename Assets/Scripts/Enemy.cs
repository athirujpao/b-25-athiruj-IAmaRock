using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character , IAttackable
{
    public GameObject rockPrefab;
    private Transform playerTransform;

    private void Start()
    {
        health = 3;
        rockCount = 1; // Enemies start with one rock
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer < 8f && rockCount > 0)
            {
                ThrowRock(playerTransform.position);
            }
        }
    }

    public override void PickUpRock()
    {
        // Enemies may automatically pick up rocks when close
    }

    public override void ThrowRock(Vector2 direction)
    {
        if (rockCount > 0)
        {
            GameObject rock = Instantiate(rockPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = rock.AddComponent<Rigidbody2D>();
            rb.AddForce((direction - (Vector2)transform.position).normalized * 10f, ForceMode2D.Impulse);
            rockCount--;
            Debug.Log("Enemy threw a rock.");
        }
    }

    public override void TransformToRockForm()
    {
        if (rockCount > 0)
        {
            Debug.Log("Enemy transformed into Rock Form to parry.");
            rockCount--;
            StartCoroutine(ExitRockForm(1.5f)); // Stay in rock form for 1.5 seconds
        }
    }

    private System.Collections.IEnumerator ExitRockForm(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Enemy exited Rock Form.");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Enemy has died.");
            // Implement enemy death logic here
        }
        else
        {
            Debug.Log($"Enemy took {damage} damage. Health left: {health}");
        }
    }
}
