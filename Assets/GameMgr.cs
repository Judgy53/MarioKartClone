using UnityEngine;
using System.Collections;

public class GameMgr : MonoSingleton<GameMgr> {

    public enum GameState : int
    {
        Start = 0,
        Racing = 1,
        End = 2
    }

    public GameState state = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {

    }
}
