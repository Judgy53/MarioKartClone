using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LapDisplay : MonoBehaviour {

    private Text text = null;
    private CarWaypointHandler carWpHandler = null;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        carWpHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<CarWaypointHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        string manyLaps;

        if (GameMgr.Instance.state == GameMgr.GameState.EndOfRace)
            manyLaps = LevelMgr.Instance.LapsToDo.ToString();   // Yea... Nobody's gonna see the difference.
        else
            manyLaps = ((int)carWpHandler.Laps + 1).ToString();

            text.text = manyLaps + "/" + LevelMgr.Instance.LapsToDo.ToString() + " Laps";
    }
}
