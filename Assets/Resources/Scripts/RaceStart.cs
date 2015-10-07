using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaceStart : MonoBehaviour {

    List<Rigidbody> carBodies;

	// Use this for initialization
	void Start () { // Getting the rigidbodies to block then launching coroutine.
        carBodies = new List<Rigidbody>();

        carBodies.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>());

        GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");

        foreach (GameObject bot in bots)
            carBodies.Add(bot.GetComponent<Rigidbody>());

        StartCoroutine("CountDown");
	}

    private void FreezeCars()
    {
        foreach (Rigidbody body in carBodies)
            body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    private void ReleaseCars()
    {
        foreach (Rigidbody body in carBodies)
            body.constraints = RigidbodyConstraints.None;
    }

    private IEnumerator CountDown()
    {
        FreezeCars();

        StartCoroutine( UIMgr.Instance.RaceCountDownDisplay() );

        yield return new WaitForSeconds(4f);

        LevelMgr.Instance.raceClock.Start();
        ReleaseCars();

        GameMgr.Instance.state = GameMgr.GameState.Racing;
    }
}
