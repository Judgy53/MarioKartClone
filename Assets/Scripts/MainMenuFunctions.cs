using UnityEngine;
using System.Collections;

public class MainMenuFunctions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Quit()
    {
        GameMgr.Instance.QuitGame();
    }

    public void SelectLevel()
    {
        MainMenuUIMgr.Instance.LevelSelection();
        GameMgr.Instance.state = GameMgr.GameState.LevelSelector;
    }

    public void BeholdHighscores()
    {
        MainMenuUIMgr.Instance.ViewHighScores();
        GameMgr.Instance.state = GameMgr.GameState.HighScores;
    }
}
