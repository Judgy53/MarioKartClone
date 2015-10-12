using UnityEngine;
using System.Collections;

public class LevelSelectorFunctions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoBack()
    {
        MainMenuUIMgr.Instance.Menu();
    }

    public void PlayLevel()
    {
        GameMgr.Instance.LaunchLevel(MainMenuInputMgr.Instance.Choice);
    }

}
