using UnityEngine;
using System.Collections;

public class Player : CarItemHandler {

	private static Player instance = null;
	public static Player Instance {
		get
		{
			if(instance == null)
			{
				Debug.Log ("Trying to get an unknown Player Instance. Undefined Behaviour may happen");
				instance = new Player();
			}

			return instance;
		}
	}

	void Awake()
	{
		currentItem = new ItemGreenShell();
		instance = this;
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
