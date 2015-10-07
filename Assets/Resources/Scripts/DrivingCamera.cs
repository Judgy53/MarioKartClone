using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class DrivingCamera : MonoBehaviour {

    private Transform carTrf = null;
    private CarController carController = null;
    private Camera cam;

    [SerializeField]
    private float ZOffSet = 3f;
    [SerializeField]
    private float DistanceFromCar = 7f;
    [SerializeField]
    private float AngleDown = 0.8f;

    [SerializeField]
    private float PositionSmooth = 0.2f;
    [SerializeField]
    private float RotationSmooth = 0.2f;

    [SerializeField]
    private float FoVMin = 80f;
    [SerializeField]
    private float FoVMax = 120f;

    private bool rotatingAroundCar = false;
    private float rotateStartTime;

	public bool FollowCar = true;

	// Use this for initialization
	void Start () {
        GameObject car = GameObject.FindGameObjectWithTag("Player");
        carTrf = car.transform;
        carController = car.GetComponent<CarController>();

        cam = GetComponent<Camera>();

        transform.position = FindRigidPos();
        transform.rotation = FindRigidRot();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (FollowCar) 
		{
			transform.position = Vector3.Lerp (transform.position, FindRigidPos (), PositionSmooth);
			transform.rotation = Quaternion.Slerp (transform.rotation, FindRigidRot (), RotationSmooth);
		}
		else 
		{
			Vector3 pos = carTrf.position-transform.position;
			Quaternion newRot = Quaternion.LookRotation(pos);
			transform.rotation = Quaternion.Lerp(transform.rotation, newRot, RotationSmooth);
		}

        cam.fieldOfView = (carController.CurrentSpeed / carController.MaxSpeed) * (FoVMax - FoVMin) + FoVMin;
	}

    Vector3 FindRigidPos()
    {
        Vector3 pos;
        if (rotatingAroundCar)
        {
            pos = new Vector3((DistanceFromCar * -Mathf.Cos(AngleDown)) * Mathf.Sin(Time.time - rotateStartTime), DistanceFromCar * Mathf.Sin(AngleDown), (DistanceFromCar * -Mathf.Cos(AngleDown)) * Mathf.Cos(Time.time - rotateStartTime));
            pos = carTrf.TransformPoint(pos);
        }
        else
        {
            pos = new Vector3(0f, DistanceFromCar * Mathf.Sin(AngleDown), DistanceFromCar * -Mathf.Cos(AngleDown));
            pos = carTrf.TransformPoint(pos);
        }
        return pos;
    }

    Quaternion FindRigidRot()
    {
        Vector3 centering = carTrf.position;
        centering.y += ZOffSet;
        return Quaternion.LookRotation(centering - transform.position, Vector3.up);
    }

    public void EnterRotatingMode()
    {
        rotatingAroundCar = true;
        rotateStartTime = Time.time;
    }
}
