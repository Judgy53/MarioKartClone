using UnityEngine;
using System.Collections;

public class GameMgr : MonoSingleton<GameMgr> {

    public enum GameState : int
    {
        MainMenu = 0,
        LevelSelector = 1,
        HighScores = 2,
        StartOfRace = 3, // The countdown part.
        Racing = 4,
        EndOfRace = 5
    }

    public GameState state = 0;

    [SerializeField]
    private string NameOfPlayer;

    [SerializeField]
    private string MainMenu;

    [SerializeField]
    private string[] Levels;

    private int currentLevel = 0;

    public string PlayerName { get { return NameOfPlayer; } }

    public string[] LevelNames { get { return Levels; } }

    public int CurrentLevel { get { return currentLevel; } }



    void Awake()
    {
        if (InstanceExists())
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }


	// Use this for initialization
	void Start () {
		state = GameState.MainMenu;
	}
	
	// Update is called once per frame
	void Update () {

	}


    public void LaunchLevel(int level)
    {
        Application.LoadLevel(Levels[level]);
        state = GameState.StartOfRace;
        currentLevel = level;
    }

    public void RestartLevel()
    {
        LaunchLevel(CurrentLevel);
    }

    public void LaunchMenu()
    {
        Application.LoadLevel(MainMenu);
        state = GameState.MainMenu;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EndRace()
    {
        Camera.main.GetComponent<DrivingCamera>().EnterRotatingMode();
        RaceUIMgr.Instance.DisplayRanking();
        LevelMgr.Instance.raceClock.Stop();
        state = GameState.EndOfRace;

        StartCoroutine("DelayedBackToMenu");
    }

    IEnumerator DelayedBackToMenu()
    {
        yield return new WaitForSeconds(10f);
        LaunchMenu();
    }
}
