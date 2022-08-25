using System;
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

        if (this.transform.position == this.nextWayPoint.transform.position)
        {
            this.GetNextWayPoint();
        }

        transform.forward = Vector3.RotateTowards(transform.forward, nextWayPoint.position - transform.position, 150 * Time.deltaTime, 0.0f);

        transform.position = Vector3.MoveTowards(transform.position, nextWayPoint.position, enemy.GetSpeed() * Time.deltaTime);

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
        PlayerStats.lives -= enemy.livesToRemove;
        Destroy(gameObject);
    }

    public int GetWaypointIndex()
    {
        return this.wayPointIndex;
    }

    public float GetDistanceToNextWaypoint()
    {
        try
        {
            return Vector3.Distance(enemy.transform.position, this.nextWayPoint.transform.position);
        }
        catch (Exception)
        {
            return 0.0f;
        }
    }
}
