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

    [SerializeField]
    private float shakeFactor = 0.1f;
    private Vector3 lastShake = Vector3.zero;
    private float lastShakeTime = 0f;

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

        float speedRatio = carController.CurrentSpeed / carController.MaxSpeed;

        cam.fieldOfView = speedRatio * (FoVMax - FoVMin) + FoVMin;

        Shake(speedRatio*speedRatio*speedRatio);
	}

    private Vector3 FindRigidPos()
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

    private Quaternion FindRigidRot()
    {
        Vector3 centering = carTrf.position;
        centering.y += ZOffSet;
        return Quaternion.LookRotation(centering - transform.position, Vector3.up);
    }

    private void Shake(float power)
    {
        if (Time.fixedTime - lastShakeTime > 0.030)
        {
            lastShakeTime = Time.fixedTime;

            transform.position -= lastShake;

            float shakeAngle = Random.Range(0f, 6.28f);

            Vector3 newShake = new Vector3(Mathf.Cos(shakeAngle), Mathf.Sin(shakeAngle), 0f);

            newShake *= power * shakeFactor;

            transform.position += newShake;

            lastShake = newShake;
        }
    }

    public void EnterRotatingMode()
    {
        rotatingAroundCar = true;
        rotateStartTime = Time.time;
    }
}
