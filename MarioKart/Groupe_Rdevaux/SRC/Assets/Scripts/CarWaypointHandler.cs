using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class CarWaypointHandler : MonoBehaviour {

    [SerializeField]
    private Transform LastWayPoint = null;

    private CarController carController = null;
    private Rigidbody carRigidbody = null;

    private bool isCheckingForBlocked = false;

    // Use this for initialization
    void Start()
    {
        carController = GetComponent<CarController>();
        carRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (carController.CurrentSpeed < 5 && !isCheckingForBlocked)
        {
            StartCoroutine("CheckForBlocked");
        }
    }

    private void SetLastWayPoint(Transform WayPoint)
    {
        LastWayPoint = WayPoint;
    }

	private void NeverGonnaGiveYouUp()
	{
		TeleportToLastWayPoint ();
	}

    public void TeleportToLastWayPoint()
    {
        carRigidbody.position = LastWayPoint.position;
        carRigidbody.rotation = LastWayPoint.rotation;
        carRigidbody.velocity = Vector3.zero;
        carRigidbody.angularVelocity = Vector3.zero;
    }


    private IEnumerator CheckForBlocked()
    {
        isCheckingForBlocked = true;

        for (int ite = 0; ite < 30 && carController.CurrentSpeed < 5; ++ite)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (carController.CurrentSpeed < 5f)
            TeleportToLastWayPoint();

        isCheckingForBlocked = false;
    }
}
