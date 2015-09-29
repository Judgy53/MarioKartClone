using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ranker : MonoBehaviour {

    [SerializeField]
    private List<CarWaypointHandler> AllTheCars;
    private AdvComparer advComparer;

	// Use this for initialization
	void Start () {
        advComparer = new AdvComparer();
	}
	
	// Update is called once per frame
	void Update () {

        AllTheCars.Sort(advComparer);

        int rank = AllTheCars.Count;

        foreach (CarWaypointHandler car in AllTheCars)
        {
            car.rank = rank;
            rank--;
        }
	
	}


    private class AdvComparer : IComparer<CarWaypointHandler>
    {
        public int Compare(CarWaypointHandler car1, CarWaypointHandler car2)
        {
            if (car1.WayPointCount > car2.WayPointCount)
                return 1;   // Car1 is in front of car2.

            else if (car1.WayPointCount < car2.WayPointCount)
                return -1;  // Car2 is in front of car1;

            else
            {
                float distFromNextWp1 = (car1.LastWp.NextWp.transform.position - car1.transform.position).magnitude;
                float distFromNextWp2 = (car2.LastWp.NextWp.transform.position - car2.transform.position).magnitude;

                if (distFromNextWp1 < distFromNextWp2)
                    return 1;   // Car1 is in front of car2.

                else if (distFromNextWp1 > distFromNextWp2)
                    return -1;  // Car2 is in front of car1.

                else
                    return 0;   // Car1 and car2 are at the same point.
            }
        }
    }
}
