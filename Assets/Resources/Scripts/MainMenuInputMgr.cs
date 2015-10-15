using UnityEngine;
using System.Collections;

public class MainMenuInputMgr : MonoSingleton<MainMenuInputMgr> {

    [SerializeField]
    private int nbrOfTracks = 0;

    private int choice = 0;

    public int Choice { get { return choice; } }

	// Use this for initialization
	void Start () {
        if (nbrOfTracks == 0)
            Debug.Log("Please set a number of tracks in the level selector input manager.");
	}
	
	// Update is called once per frame
	void Update () {
        if (GameMgr.Instance.state == GameMgr.GameState.LevelSelector)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SelectNextCircuit();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SelectPreviousCircuit();
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                GameMgr.Instance.LaunchLevel(choice);
            }
        }

        if (GameMgr.Instance.state == GameMgr.GameState.HighScores)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SelectNextCircuit();
                HighScoresDisplay.Instance.LoadLeaderboard(choice);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SelectPreviousCircuit();
                HighScoresDisplay.Instance.LoadLeaderboard(choice);
            }
        }
    }

    public void SelectNextCircuit()
    {
        CircuitShowdown.Instance.NextCircuit();
        choice = (choice + 1) % nbrOfTracks;
    }

    public void SelectPreviousCircuit()
    {
        CircuitShowdown.Instance.PreviousCircuit();
        choice = choice == 0 ? nbrOfTracks - 1 : choice - 1;
    }
}
