using UnityEngine;
using System.Collections;

public class GameMgr : MonoSingleton<GameMgr> {

    public enum GameState : int
    {
        MainMenu = 0,
        LevelSelector = 1,
        Highscores = 2,
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

    public string PlayerName { get { return NameOfPlayer; } }



    void Awake()
    {
        if (InstanceExists())
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
        //state = 0; should be there.
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
