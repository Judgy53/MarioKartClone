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
    private string LevelSelector;

    [SerializeField]
    private string[] Levels;

	// Use this for initialization
	void Start () {
        //state = 0; should be there.
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void LaunchLevel(int level)
    {
        Application.LoadLevel(Levels[level]);
        state = GameState.StartOfRace;
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LaunchLevelSelector()
    {
        Application.LoadLevel(LevelSelector);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EndRace()
    {
        Camera.main.GetComponent<DrivingCamera>().EnterRotatingMode();
        UIMgr.Instance.DisplayRanking();
        LevelMgr.Instance.raceClock.Stop();
        state = GameState.EndOfRace;

        StartCoroutine("DelayedBackToSelector");
    }

    IEnumerator DelayedBackToSelector()
    {
        yield return new WaitForSeconds(10f);
        LaunchLevelSelector();
    }
}
