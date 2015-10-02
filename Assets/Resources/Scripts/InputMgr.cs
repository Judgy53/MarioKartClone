using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class InputMgr : MonoSingleton<InputMgr> {

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
    }

	void FixedUpdate () {
	    float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float handbrake = Input.GetAxis("Jump");

        carController.Move(h, v, v, handbrake);
    }
}
