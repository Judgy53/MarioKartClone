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
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ResumeGame()
    {
        UIMgr.Instance.Resume();
    }
}
