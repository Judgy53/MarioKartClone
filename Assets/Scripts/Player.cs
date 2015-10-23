using UnityEngine;
using System.Collections;

public class Player : CarItemHandler {

    private Record record = null;

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
		instance = this;

        record = new Record();

        char[] nameChars = GameMgr.Instance.PlayerName.ToCharArray();

        string trueName = string.Empty;

        for (int ite = 0; ite < nameChars.Length; ++ite)
        {
            if (nameChars[ite] != '#')
                trueName += nameChars[ite];
        }
        
        record.SetHolderName(trueName);
	}

    void LapEnded(int laps)
    {
        RaceUIMgr.Instance.EndOfLapDisplay();
        record.AddLapTime(LevelMgr.Instance.raceClock.LocalTime - GetComponent<CarWaypointHandler>().TimeAtLastLap);

        if (laps == LevelMgr.Instance.LapsToDo)
        {
            GameMgr.Instance.EndRace();

            gameObject.GetComponent<CarAutoDrive>().enabled = true;
            gameObject.SendMessage("SetTarget", GetComponent<CarWaypointHandler>().LastWp.NextWp.transform);
            gameObject.tag = "Bot";

            RecordKeeper.Instance.SubmitHighScore(record);
        }
    }
}