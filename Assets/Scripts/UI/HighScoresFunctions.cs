using UnityEngine;
using System.Collections;

public class HighScoresFunctions : MonoBehaviour {

    public void GoBack()
    {
        MainMenuUIMgr.Instance.Menu();
        GameMgr.Instance.state = GameMgr.GameState.MainMenu;
    }

    public void NextCircuit()
    {
        MainMenuInputMgr.Instance.SelectNextCircuit();
        HighScoresDisplay.Instance.LoadLeaderboard(MainMenuInputMgr.Instance.Choice);
    }

    public void PreviousCircuit()
    {
        MainMenuInputMgr.Instance.SelectPreviousCircuit();
        HighScoresDisplay.Instance.LoadLeaderboard(MainMenuInputMgr.Instance.Choice);
    }
}
