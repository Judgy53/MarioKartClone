using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class DrivingCamera : MonoBehaviour {

    private Transform carTrf = null;
    private CarController carController = null;
    private Camera camera;

    [SerializeField]
    private float ZOffSet = 1f;
    [SerializeField]
    private float DistanceFromCar = 10f;
    [SerializeField]
    private float AngleDown = 0.01f;

    [SerializeField]
    private float PositionSmooth = 0.2f;
    [SerializeField]
    private float RotationSmooth = 0.2f;

    [SerializeField]
    private float FoVMin = 60f;
    [SerializeField]
    private float FoVMax = 120f;

	public bool FollowCar = true;

	// Use this for initialization
	void Start () {
        GameObject car = GameObject.FindGameObjectWithTag("Player");
        carTrf = car.transform;
        carController = car.GetComponent<CarController>();

        camera = GetComponent<Camera>();

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

        camera.fieldOfView = (carController.CurrentSpeed / carController.MaxSpeed) * (FoVMax - FoVMin) + FoVMin;

	}

    Vector3 FindRigidPos()
    {
        Vector3 pos = new Vector3(0f, DistanceFromCar * Mathf.Sin(AngleDown), DistanceFromCar * -Mathf.Cos(AngleDown));
        pos = carTrf.TransformPoint(pos);
        return pos;
    }

    Quaternion FindRigidRot()
    {
        Vector3 centering = carTrf.position;
        centering.y += ZOffSet;
        return Quaternion.LookRotation(centering - transform.position, Vector3.up);
    }
}
