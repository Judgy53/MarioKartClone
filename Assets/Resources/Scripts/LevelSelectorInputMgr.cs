﻿using UnityEngine;
using System.Collections;

public class LevelSelectorInputMgr : MonoBehaviour {

    [SerializeField]
    private int nbrOfTracks = 0;

    private int choice = 0;

	// Use this for initialization
	void Start () {
        if (nbrOfTracks == 0)
            Debug.Log("Please set a number of tracks in the level selector input manager.");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CircuitShowdown.Instance.NextCircuit();
            choice = (choice + 1) % nbrOfTracks;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CircuitShowdown.Instance.PreviousCircuit();
            choice = choice == 0 ? nbrOfTracks - 1 : choice - 1;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameMgr.Instance.LaunchLevel(choice);
        }
    }
}
