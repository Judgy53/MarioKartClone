using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class WaypointOrganizer {

    //private WaypointComparer wpComparer = new WaypointComparer();

    public static void Organize(Waypoint[] waypoints)
    {
        Array.Sort(waypoints, new WaypointComparer());

        for (int i = 0; i < waypoints.Length - 1; ++i)
        {
            waypoints[i].SetNextWaypoint(waypoints[i+1]);
        }

        waypoints[waypoints.Length - 1].SetNextWaypoint(waypoints[0]);
    }

    private class WaypointComparer : IComparer<Waypoint>
    {
        public int Compare(Waypoint wp1, Waypoint wp2)
        {
            return wp1.gameObject.name.CompareTo(wp2.gameObject.name);
        }
    } 
}
