using UnityEngine;
using System.Collections;

public class PauseMenuFunctions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RestartLevel()
    {
        GameMgr.Instance.LaunchLevel(GameMgr.Instance.CurrentLevel);
    }

    public void ResumeGame()
    {
        RaceUIMgr.Instance.Resume();
    }

    public void MainMenu()
    {
        GameMgr.Instance.LaunchMenu();
    }
}
