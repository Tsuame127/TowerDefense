using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private int wayPointIndex = 0;
    private Enemy enemy;
    private Transform nextWayPoint;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        nextWayPoint = WayPoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = this.nextWayPoint.position - transform.position;

        transform.Translate(enemy.GetSpeed() * Time.deltaTime * dir.normalized, Space.World);

        if (Vector3.Distance(this.transform.position, this.nextWayPoint.transform.position) <= 0.4f)
        {
            this.GetNextWayPoint();
        }

        enemy.SetSpeed(enemy.GetDefaultSpeed());
    }

    private void GetNextWayPoint()
    {
        if (wayPointIndex >= WayPoints.points.Length - 1)
        {
            this.OnEndReached();
        }
        else
        {
            this.wayPointIndex++;
            this.nextWayPoint = WayPoints.points[wayPointIndex];
        }
    }

    private void OnEndReached()
    {
        PlayerStats.lives--;
        Destroy(gameObject);
    }
}
