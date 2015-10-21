using UnityEngine;
using System.Collections;

public class MainMenuInputMgr : MonoSingleton<MainMenuInputMgr> {

    private int nbrOfTracks = 0;

    private int choice = 0;

    public int Choice { get { return choice; } }

	// Use this for initialization
	void Start () {
        nbrOfTracks = GameMgr.Instance.LevelNames.Length;
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

        if (GameMgr.Instance.state == GameMgr.GameState.MainMenu)
        {
            if (Input.GetKeyDown("t"))
                RandomKickLine.Instance.ChangeLine();
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
        choice = (choice == 0) ? nbrOfTracks - 1 : choice - 1;
    }
}
