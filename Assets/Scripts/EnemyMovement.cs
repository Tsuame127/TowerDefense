using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private int wayPointIndex = 0;
    private Transform target;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = WayPoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = this.target.position - transform.position;

        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(this.transform.position, this.target.transform.position) <= 0.4f)
        {
            this.GetNextWayPoint();
        }

        enemy.speed = enemy.defaultSpeed;
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
            this.target = WayPoints.points[wayPointIndex];
        }
    }

    private void OnEndReached()
    {
        PlayerStats.lives--;
        Destroy(gameObject);
    }
}
