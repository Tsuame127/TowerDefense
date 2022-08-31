using System.Collections;
using UnityEngine;


public class MainMenuTurret : MonoBehaviour
{
    [Header("General")]
    private Vector3 mouseWorldPos;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;

    [Header("Unity Setup Fields")]
    public Transform firePoint;
    public Transform partToRotate;
    public float turnSpeed = 15f;


    void Update()
    {

        this.LockOnTarget();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void LockOnTarget()
    {
        Vector3 screenpos;

        screenpos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenpos);

        if (Physics.Raycast(ray, out RaycastHit rayData, 1000))
        {
            mouseWorldPos = rayData.point;
            mouseWorldPos.y = 1.75f;

            Vector3 dir = mouseWorldPos - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    private void Shoot()
    {
        GameObject bulletCreated = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        GameObject target = new GameObject("EmptyTarget");
        target.transform.position = mouseWorldPos;

        bulletCreated.TryGetComponent<Bullet>(out Bullet bullet);

        if (bullet == null)
        {
            return;
        }

        bullet.SetTarget(target.transform);

        StartCoroutine(DestroyEmptyAfterHit(target, bulletCreated));
    }

    IEnumerator DestroyEmptyAfterHit(GameObject target, GameObject bullet)
    {
        float timeToTarget = Vector3.Distance(target.transform.position, this.firePoint.transform.position) / bullet.GetComponent<Bullet>().GetSpeed();

        yield return new WaitForSeconds(timeToTarget);

        Destroy(target);
    }
}
