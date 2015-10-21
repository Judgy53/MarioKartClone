using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class CarWaypointHandler : MonoBehaviour {

    [SerializeField]
    private Waypoint StartingWaypoint = null;
    [SerializeField]
    private Waypoint LastWaypoint = null;
    private Waypoint RespawnWaypoint = null;

    private CarController carController = null;
    private Rigidbody carRigidbody = null;

    private int laps = 0;
    private int waypointCount = 0;
    private float timeAtLastLap = 0;

    private bool finished = false;

    private bool isCheckingForBlocked = false;

    public Waypoint LastWp { get { return LastWaypoint; } }

    public int Laps { get { return laps; } }
    public int WaypointCount { get { return waypointCount; } }
    public float TimeAtLastLap { get { return timeAtLastLap; } }

    public bool HasFinished { get { return finished; } }
    public int rank = 0;

    // Use this for initialization
    void Start()
    {
        carController = GetComponent<CarController>();
        carRigidbody = GetComponent<Rigidbody>();

        LastWaypoint = StartingWaypoint;
        RespawnWaypoint = StartingWaypoint;
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

    private void SetLastWaypoint(Waypoint waypoint)
    {
        if (waypoint == LastWaypoint.NextWp)
        {
            LastWaypoint = waypoint;
            ++waypointCount;

            if (waypoint.CanRespawnHere)
            {
                RespawnWaypoint = waypoint;
            }

            if (waypoint == StartingWaypoint && laps < LevelMgr.Instance.LapsToDo)
            {
                ++laps;

                gameObject.SendMessage("LapEnded", laps, SendMessageOptions.DontRequireReceiver);

                timeAtLastLap = LevelMgr.Instance.raceClock.LocalTime;

                if (laps == LevelMgr.Instance.LapsToDo)
                {
                    Ranker.Instance.LockFirstNotLocked();
                    finished = true;
                }
            }
        }
    }

	private void OnOutOfMap()
	{
		TeleportToLastWaypoint();
	}

    public void TeleportToLastWaypoint()
    {
        Vector3 newCarPos = RespawnWaypoint.Floor + Vector3.up * 10f;
        carRigidbody.position = newCarPos;
        carRigidbody.rotation = RespawnWaypoint.transform.rotation;
        carRigidbody.velocity = Vector3.zero;
        carRigidbody.angularVelocity = Vector3.zero;

        LastWaypoint = RespawnWaypoint;
        gameObject.SendMessage("SetTarget", LastWaypoint.NextWp.transform);
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
