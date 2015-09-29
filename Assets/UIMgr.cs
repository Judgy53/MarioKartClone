using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMgr : MonoSingleton<UIMgr> {

    public Canvas pauseMenu = null;
    public Canvas inGameInterface = null;

	// Use this for initialization
	void Start () {
        if (pauseMenu == null)
            Debug.Log("Please bind a pause menu to the UI manager.");

        if (inGameInterface == null)
            Debug.Log("Please bind an in-game interface to the UI manager.");

        Resume();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Pause()
    {
        inGameInterface.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        inGameInterface.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }
}
