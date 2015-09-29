using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class CarWaypointHandler : MonoBehaviour {

    [SerializeField]
    private WayPoint StartingWayPoint = null;
    private WayPoint LastWayPoint = null;

    private CarController carController = null;
    private Rigidbody carRigidbody = null;

    private int laps = 0;
    private int wayPointCount = 0;

    private bool isCheckingForBlocked = false;

    public WayPoint LastWp { get { return LastWayPoint; } }

    public int Laps { get { return laps; } }
    public int WayPointCount { get { return wayPointCount; } }

    public int rank;

    // Use this for initialization
    void Start()
    {
        carController = GetComponent<CarController>();
        carRigidbody = GetComponent<Rigidbody>();

        LastWayPoint = StartingWayPoint;
    }

    void Update()
    {
        if (carController.CurrentSpeed < 5f && !isCheckingForBlocked)
        {
            StartCoroutine("CheckForBlocked");
        }
    }

    private void SetLastWayPoint(WayPoint wayPoint)
    {
        if (wayPoint == LastWayPoint.NextWp)
        {
            LastWayPoint = wayPoint;
            ++wayPointCount;

            if (wayPoint == StartingWayPoint)
                ++laps;
        }
    }

	private void NeverGonnaGiveYouUp()
	{
		TeleportToLastWayPoint ();
	}

    public void TeleportToLastWayPoint()
    {
        carRigidbody.position = LastWayPoint.transform.position;
        carRigidbody.rotation = LastWayPoint.transform.rotation;
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
