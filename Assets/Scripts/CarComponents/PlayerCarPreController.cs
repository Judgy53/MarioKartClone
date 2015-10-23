using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class PlayerCarPreController : MonoBehaviour {

    private CarController carController = null;

	// Use this for initialization
	void Start () {
        carController = GetComponent<CarController>();
	}
	
	void FixedUpdate () {
        Vector2 Axes = RaceInputMgr.Instance.GetXYAxes();

        carController.Move(Axes.x, Axes.y, Axes.y, 0f); // Didn't make that function... Just mimicking here.
	}
}
