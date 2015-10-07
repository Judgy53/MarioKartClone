using UnityEngine;
using System.Collections;

public class Player : CarItemHandler {

	void Awake()
	{
		currentItem = new ItemTripleGreenShell();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			//OnHit ();
			useItem(Input.GetKey(KeyCode.DownArrow));
		}
	}

    void LapEnded(int laps)
    {
        UIMgr.Instance.EndOfLapDisplay();

        if (laps == LevelMgr.Instance.LapsToDo)
        {
            GameMgr.Instance.EndRace();

            gameObject.GetComponent<UnityStandardAssets.Vehicles.Car.CarAIControl>().enabled = true;
            gameObject.SendMessage("SetTarget", GetComponent<CarWaypointHandler>().LastWp.NextWp.transform);
            gameObject.tag = "Bot";
        }
    }
}
