using UnityEngine;
using System.Collections;

public class RankingDisplayFunctions : MonoBehaviour {

    public void BackToMainMenu()
    {
        GameMgr.Instance.LaunchMenu();
    }

    public void Retry()
    {
        GameMgr.Instance.LaunchLevel(GameMgr.Instance.CurrentLevel);
    }
}
