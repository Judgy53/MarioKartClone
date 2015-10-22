using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class CarAutoDrive : MonoBehaviour {

    private CarController carController = null;

    private Vector3 target;


	// Use this for initialization
	void Start () {
	    carController = GetComponent<CarController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Quaternion quat = Quaternion.FromToRotation(Vector3.forward, (transform.InverseTransformPoint(target) + Vector3.forward) / 2f);

        float angle = quat.eulerAngles.y;
        float angleb = -360f + angle;

        angle = (angle < Mathf.Abs(angleb)) ? angle : angleb;

        angle += Random.Range(-15f, 15f);

        float steer = Mathf.Clamp((angle / 180f), -90, 90);

        float accel = 1f;
        
        if ((angle / 180f) - steer > 0f)
            steer -= (angle / 180f) - steer;

        carController.Move(steer, accel, accel, 0f);
	}

    void SetTarget(Transform TargetTrf)
    {
        target = TargetTrf.position;
    }
}
