using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float defaultSpeed = 35f;
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private int deathBounty = 5;
    private float speed;

    [Header("UI")]
    [SerializeField]
    private GameObject deathEffect;

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

    public void Slow(float slowAmount) { speed = defaultSpeed * (1f - slowAmount); }

    //Accessors
    public float GetSpeed() { return speed; }
    public void SetSpeed(float newSpeed) { this.speed = newSpeed; }
    public float GetDefaultSpeed() { return this.defaultSpeed; }
}
