using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float defaultSpeed = 35f;
    [HideInInspector]
    public float speed;
    [SerializeField]
    private float health = 100f;
    public int deathBounty = 5;
    public GameObject deathEffect;


    private void Start()
    {
        speed = defaultSpeed;
    }

    public void TakeDamage(float damageDealt)
    {
        health -= damageDealt;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerStats.money += deathBounty;
        GameObject particles = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(particles, 1f);
        Destroy(gameObject);
    }

    public void Slow(float slowAmount)
    {
        speed = defaultSpeed * (1f - slowAmount);
    }
}
