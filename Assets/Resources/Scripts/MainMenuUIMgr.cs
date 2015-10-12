using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUIMgr : MonoSingleton<MainMenuUIMgr> {

    [SerializeField]
    private Canvas MainMenu = null;
    [SerializeField]
    private Canvas LevelSelector = null;

	// Use this for initialization
	void Start () {
        if (MainMenu == null)
            Debug.Log("Please bind a main menu canvas to the UI manager.");

        if (LevelSelector == null)
            Debug.Log("Please bind a level selector canvas to the UI manager.");

        Menu();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Menu()
    {
        LevelSelector.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
    }

    public void LevelSelection()
    {
        MainMenu.gameObject.SetActive(false);
        LevelSelector.gameObject.SetActive(true);
    }
}
