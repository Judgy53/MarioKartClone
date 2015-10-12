using UnityEngine;
using System.Collections;

public class MainMenuInputMgr : MonoSingleton<MainMenuInputMgr> {

    [SerializeField]
    private int nbrOfTracks = 0;

    private int choice = 0;

    public int Choice { get { return choice; } }

	// Use this for initialization
	void Start () {
        if (nbrOfTracks == 0)
            Debug.Log("Please set a number of tracks in the level selector input manager.");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow) && GameMgr.Instance.state == GameMgr.GameState.LevelSelector)
        {
            SelectNextCircuit();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && GameMgr.Instance.state == GameMgr.GameState.LevelSelector)
        {
            SelectPreviousCircuit();
        }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) && GameMgr.Instance.state == GameMgr.GameState.LevelSelector)
        {
            GameMgr.Instance.LaunchLevel(choice);
        }



        if (Input.GetKeyDown("w"))
        {
            FileTranslator.WriteShit("SP", "AAAAAAAAA\nAAAAAAAAAAAAAAAAAAAAAAA\nAA\nAAAAAAAAAAAAAAAAAAAAAAA\nAAA\nAA\nA\nAA\nAAA\nAAAAA\nACE");
        }

        if (Input.GetKeyDown("r"))
        {
            Debug.Log(FileTranslator.ReadShit("SP"));
        }
    }

    public void SelectNextCircuit()
    {
        CircuitShowdown.Instance.NextCircuit();
        choice = (choice + 1) % nbrOfTracks;
    }

    public void SelectPreviousCircuit()
    {
        CircuitShowdown.Instance.PreviousCircuit();
        choice = choice == 0 ? nbrOfTracks - 1 : choice - 1;
    }
}
