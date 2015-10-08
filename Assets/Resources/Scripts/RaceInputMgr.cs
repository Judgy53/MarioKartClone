using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class RaceInputMgr : MonoSingleton<RaceInputMgr> {

    private CarController carController; // Controller of player's car.
    private CarWaypointHandler wpHandler; // Waypoint handler of player's car.

	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        carController = player.GetComponent<CarController>();
        wpHandler = player.GetComponent<CarWaypointHandler>();
	}

    void Update () {
        if (Input.GetButtonDown("Fire1"))
            wpHandler.TeleportToLastWaypoint();

        if (Input.GetButtonDown("Cancel"))
            UIMgr.Instance.Pause();

        if (Input.GetKeyDown(KeyCode.Tab))
            UIMgr.Instance.DisplayRanking();

        if (Input.GetKeyUp(KeyCode.Tab))
            UIMgr.Instance.DisplayRankingNot();

		if (Input.GetKeyDown(KeyCode.Space))
			Player.Instance.StartUseItem(Input.GetKey(KeyCode.DownArrow));
		
		if (Input.GetKeyUp(KeyCode.Space))
			Player.Instance.StopUseItem(Input.GetKey(KeyCode.DownArrow));
    }

	void FixedUpdate () {
	    float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float handbrake = Input.GetAxis("Jump");

        carController.Move(h, v, v, handbrake);
    }
}
