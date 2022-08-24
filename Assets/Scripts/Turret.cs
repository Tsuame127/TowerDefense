using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public enum TurretStatus { Placing, Active, Inactive }

public class Turret : MonoBehaviour
{
    [Header("General")]
    public float range = 15f;
    private Transform target;
    private Enemy enemyTarget;
    [SerializeField]
    private GameObject turretRangeCylinder;

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

    private BuildManager buildManager;
    public TurretBluePrint blueprint;
    
    public bool isUpgraded = false;
    private TurretStatus turretStatus;


    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.1f);

        turretRangeCylinder.transform.localScale = new Vector3(range * 2, 1, range * 2);
        turretRangeCylinder.SetActive(false);

        buildManager = BuildManager.instance;
    }


    void Update()
    {
        if (turretStatus == TurretStatus.Placing)
        {
            ShowRange();
            return;
        }

        if (turretRangeCylinder.activeSelf)
        {
            turretRangeCylinder.transform.localScale = new Vector3(range * 2, 1, range * 2);
        }

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
        impactEffect.transform.SetPositionAndRotation(target.position + dir.normalized, Quaternion.LookRotation(dir));
    }

    void UpdateTarget()
    {

        List<GameObject> enemies = GameObject.FindGameObjectsWithTag(enemyTag).ToList<GameObject>();

        GameObject firstEnemy = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (Vector3.Distance(transform.position, enemies.ElementAt(i).transform.position) <= range)
            {

                if (firstEnemy == null)
                {
                    firstEnemy = enemies.ElementAt(i);
                    continue;
                }

                if (firstEnemy.GetComponent<EnemyMovement>().GetWaypointIndex() < enemies.ElementAt(i).GetComponent<EnemyMovement>().GetWaypointIndex())
                {
                    firstEnemy = enemies.ElementAt(i);
                }
                else if(firstEnemy.GetComponent<EnemyMovement>().GetWaypointIndex() == enemies.ElementAt(i).GetComponent<EnemyMovement>().GetWaypointIndex())
                {
                    if (firstEnemy.GetComponent<EnemyMovement>().GetDistanceToNextWaypoint() > enemies.ElementAt(i).GetComponent<EnemyMovement>().GetDistanceToNextWaypoint())
                    {
                        firstEnemy = enemies.ElementAt(i);
                    }
                }
            }
        }

        if (firstEnemy != null)
        {
            target = firstEnemy.transform;
            enemyTarget = target.GetComponent<Enemy>();
        }
        else
        {
            this.target = null;
        }
    }

    private void Shoot()
    {
        GameObject bulletCreated = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        bulletCreated.TryGetComponent<Bullet>(out Bullet bullet);

        if (bullet == null)
        {
            return;
        }

        bullet.SetTarget(this.target);
    }


    public void HideRange() { turretRangeCylinder.SetActive(false); }
    public void ShowRange() { turretRangeCylinder.SetActive(true); }


    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        buildManager.upgradeSellUI.SetTarget(this);
    }

    public void SetBlueprint(TurretBluePrint _blueprint)
    {
        blueprint = _blueprint;
    }


    public void SetTurretStatus(TurretStatus _turretStatus)
    {
        this.turretStatus = _turretStatus;
    }



    //RemoveAfterRelease
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
