using UnityEngine;
using System.Collections;

public class GameMgr : MonoSingleton<GameMgr> {

    //// I dunno about this...
    public enum GameState : int
    {
        Start = 0,
        Racing = 1,
        End = 2
    }

    public GameState state = 0;

    [SerializeField]
    private string[] Levels;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {

    }

    public void LaunchLevel(int level)
    {
        Application.LoadLevel(Levels[level]);
    }
}
