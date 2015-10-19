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
        //angle = Vector3.Angle(transform.forward, target.forward);


        Quaternion quat = Quaternion.FromToRotation(transform.forward, ((target - transform.position) + transform.forward)/2f);

        float angle = quat.eulerAngles.y;
        float angleb = -360f + angle;

        angle = (angle < Mathf.Abs(angleb)) ? angle : angleb;

        angle += Random.Range(-15f, 15f);

        float speedCautionCoef = (carController.MaxSpeed - carController.CurrentSpeed)/carController.MaxSpeed * 3f;

        float steer = Mathf.Clamp((angle / 180f), -speedCautionCoef, speedCautionCoef);

        float accel = 1f;
        
        if ((angle / 180f) - steer > 0f)
            steer -= (angle / 180f) - steer;

        accel -= Random.Range(0f, 0.2f);

        carController.Move(steer, accel, accel, 0f);
	}

    void SetTarget(Transform TargetTrf)
    {
        target = TargetTrf.position;
    }
}
