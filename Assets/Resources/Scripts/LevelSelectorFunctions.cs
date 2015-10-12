using UnityEngine;
using System.Collections;

public class LevelSelectorFunctions : MonoSingleton<LevelSelectorFunctions> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoBack()
    {
        MainMenuUIMgr.Instance.Menu();
        GameMgr.Instance.state = GameMgr.GameState.MainMenu;
    }

    public void PlayLevel()
    {
        GameMgr.Instance.LaunchLevel(MainMenuInputMgr.Instance.Choice);
    }

    public void NextCircuit()
    {
        MainMenuInputMgr.Instance.SelectNextCircuit();
    }

    public void PreviousCircuit()
    {
        MainMenuInputMgr.Instance.SelectPreviousCircuit();
    }

}
