using UnityEngine;
using System.Collections;

public class GameMgr : MonoSingleton<GameMgr> {

    //// I dunno about this...
    public enum GameState : int
    {
        Menu = 0,
        StartOfRace = 1, // The countdown part.
        Racing = 2,
        EndOfRace = 3
    }

    public GameState state = 0;

    [SerializeField]
    private string[] Levels;

	// Use this for initialization
	void Start () {
        state = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void LaunchLevel(int level)
    {
        Application.LoadLevel(Levels[level]);
        state = GameState.StartOfRace;
    }

    public void EndRace()
    {
        UIMgr.Instance.DisplayRanking();
        state = GameState.EndOfRace;
    }
}
