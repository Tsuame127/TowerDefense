using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float speed = 70f;
    [SerializeField]
    private float explosionRadius = 0f;
    [SerializeField]
    private int damage = 50;

    [Header("UI")]
    public GameObject impactEffect;

    private Transform target;


    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject particlesInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(particlesInstance, 0.5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target, damage * 2/3);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in collidersInRange)
        {
            if (collider.CompareTag("Enemy"))
            {
                Damage(collider.transform, damage);
            }
        }
    }

    void Damage(Transform target, int _damage)
    {
        target.TryGetComponent<Enemy>(out Enemy enemy);

        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
        }
        else
        {
            Debug.LogError("No enemy component");
        }
    }

    //Accessors
    public void SetTarget(Transform _target) { target = _target; }

    //RemoveAfterRelease
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
