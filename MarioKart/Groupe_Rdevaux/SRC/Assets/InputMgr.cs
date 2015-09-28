using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class InputMgr : MonoBehaviour {

    private CarController carController; // Controller of player's car.
    private CarWaypointHandler wpHandler; // Waypoint handler of player's car.

	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        carController = player.GetComponent<CarController>();
        wpHandler = player.GetComponent<CarWaypointHandler>();
	}
	
	void FixedUpdate () {
	    float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float handbrake = Input.GetAxis("Jump");

        carController.Move(h, v, v, handbrake);

        if (Input.GetButtonDown("Fire1"))
            wpHandler.TeleportToLastWayPoint();
    }
}
