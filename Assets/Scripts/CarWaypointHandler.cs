using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class CarWaypointHandler : MonoBehaviour {

    [SerializeField]
    private Waypoint StartingWaypoint = null;
    [SerializeField]
    private Waypoint LastWaypoint = null;

    private CarController carController = null;
    private Rigidbody carRigidbody = null;

    private int laps = 0;
    private int waypointCount = 0;
    private float timeAtLastLap = 0;

    private bool isCheckingForBlocked = false;

    public Waypoint LastWp { get { return LastWaypoint; } }

    public int Laps { get { return laps; } }
    public int WaypointCount { get { return waypointCount; } }
    public float TimeAtLastLap { get { return timeAtLastLap; } }

    public int rank = 0;
    public bool rankIsLocked = false;

    // Use this for initialization
    void Start()
    {
        carController = GetComponent<CarController>();
        carRigidbody = GetComponent<Rigidbody>();

        LastWaypoint = StartingWaypoint;
    }

    void Update()
    {
        if (carController.CurrentSpeed < 5f && !isCheckingForBlocked && GameMgr.Instance.state != GameMgr.GameState.StartOfRace)
        {
            StartCoroutine("CheckForBlocked");
        }
    }

    private void SetStartingWaypoint(Waypoint startWp)  // To call only on level initialization.
    {
        StartingWaypoint = startWp;
    }

    private void SetLastWaypoint(Waypoint Waypoint)
    {
        if (Waypoint == LastWaypoint.NextWp)
        {
            LastWaypoint = Waypoint;
            ++waypointCount;

            if (Waypoint == StartingWaypoint && laps < LevelMgr.Instance.LapsToDo)
            {
                ++laps;

                gameObject.SendMessage("LapEnded", laps, SendMessageOptions.DontRequireReceiver);

                timeAtLastLap = LevelMgr.Instance.raceClock.LocalTime;

                if (laps == LevelMgr.Instance.LapsToDo)
                    Ranker.Instance.LockFirstNotLocked();
            }
        }
    }

	private void OnOutOfMap()
	{
		TeleportToLastWaypoint();
	}

    public void TeleportToLastWaypoint()
    {
        Vector3 newCarPos = LastWaypoint.Floor + Vector3.up * 10f;
        carRigidbody.position = newCarPos;
        carRigidbody.rotation = LastWaypoint.transform.rotation;
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
            TeleportToLastWaypoint();

        isCheckingForBlocked = false;
    }
}
