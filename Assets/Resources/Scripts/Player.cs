using UnityEngine;
using System.Collections;

public class Player : CarItemHandler {

	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			//OnHit ();
			useItem(Input.GetKey(KeyCode.DownArrow));

			//if(currentItem == null)
			//	currentItem = new ItemTripleBanana();
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
