using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("General")]
    public float range = 15f;
    private Transform target;
    private Enemy enemyTarget;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Use Laser")]
    public bool UseLaser;
    public int damageOverTime = 40;
    public float slowAmount = 0.3f;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;

    [Header("Unity Setup Fields")]
    public Transform firePoint;
    public Transform partToRotate;
    public string enemyTag = "Enemy";
    public float turnSpeed = 15f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        fireCountDown -= Time.deltaTime;

        if (target == null)
        {
            if (UseLaser)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
            }
            return;
        }

        this.LockOnTarget();

        if (this.UseLaser)
        {
            ShootLaser();
        }
        else
        {
            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1 / fireRate;
            }
        }
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void ShootLaser()
    {
        enemyTarget.TakeDamage(damageOverTime * Time.deltaTime);
        enemyTarget.Slow(slowAmount);

        if (lineRenderer.enabled == false)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        impactEffect.transform.position = target.position + dir.normalized;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            enemyTarget = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    private void Shoot()
    {
        GameObject bulletCreated = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletCreated.GetComponent<Bullet>();

        if (bullet == null)
        {
            return;
        }

        bullet.SetTarget(this.target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
