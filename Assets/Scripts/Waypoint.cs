using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> NextWaypoints;

    public Waypoint GetNextWaypoint()
    {
        if (NextWaypoints.Count < 1)
        {
            return null;
        }

        return NextWaypoints[Random.Range(0, NextWaypoints.Count)];
    }
    void OnDrawGizmos()
    {
        foreach (Waypoint waypoint in NextWaypoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, waypoint.transform.position);
        }
    }
}
