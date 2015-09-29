using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMgr : MonoSingleton<GameMgr> {

    public enum GameState : int
    {
        Start = 0,
        Racing = 1,
        End = 2
    }

    public GameState state = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine("RaceStart");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {

    }

    private IEnumerator RaceStart()
    {
        List<Rigidbody> carBodies = new List<Rigidbody>();

        carBodies.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>());

        GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");

        foreach (GameObject bot in bots)
            carBodies.Add(bot.GetComponent<Rigidbody>());


        foreach (Rigidbody body in carBodies)
            body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;


        yield return new WaitForSeconds(3f);


        foreach (Rigidbody body in carBodies)
            body.constraints = RigidbodyConstraints.None;
    }
}
