using UnityEngine;

public class WayPoints : MonoBehaviour
{

    public static Transform[] points;

    void Awake()
    {
        int numberOfPoints = transform.childCount;
        points = new Transform[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
