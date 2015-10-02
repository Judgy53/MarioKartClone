﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ranker : MonoSingleton<Ranker> {

    [SerializeField]
    private List<CarWaypointHandler> AllTheCars;
    private CarPosComparer carPosComparer;

	// Use this for initialization
	void Start () {
        carPosComparer = new CarPosComparer();

        AllTheCars = new List<CarWaypointHandler>();

        AllTheCars.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<CarWaypointHandler>());

        GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");

        foreach (GameObject bot in bots)
            AllTheCars.Add(bot.GetComponent<CarWaypointHandler>());
	}
	
	// Update is called once per frame
	void Update () {
        AllTheCars.Sort(carPosComparer);

        int rank = AllTheCars.Count;

        foreach (CarWaypointHandler car in AllTheCars)
        {
            car.rank = rank;
            rank--;
        }
	}

    public CarWaypointHandler AtRank(int rank)
    {
        return AllTheCars[AllTheCars.Count - rank];
    }


    private class CarPosComparer : IComparer<CarWaypointHandler>
    {
        public int Compare(CarWaypointHandler car1, CarWaypointHandler car2)
        {
            if (car1.WaypointCount > car2.WaypointCount)
                return 1;   // Car1 is in front of car2.

            else if (car1.WaypointCount < car2.WaypointCount)
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
