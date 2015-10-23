using UnityEngine;
using System.Collections;

public class LevelMgr : MonoSingleton<LevelMgr> {

    [SerializeField]
    private GameObject Waypoints = null;
    private Waypoint startingLine = null;

    [SerializeField]
    private int MaxLaps = 3;

    [SerializeField]
    private GameObject PlayerCarPrefab = null;
    [SerializeField]
    private GameObject[] BotCarPrefab = null;

    private int totalOfCars;

    public int LapsToDo { get { return MaxLaps; } }

    public Clock raceClock;


    void Awake()
    {
        Waypoint[] waypointArray = Waypoints.GetComponentsInChildren<Waypoint>();

        WaypointOrganizer.Organize(waypointArray);

        startingLine = waypointArray[0];

        startingLine.SendMessage("Awake"); // Fuck unity.

        totalOfCars = BotCarPrefab.Length + 1;

        //// MUST NOT STAY THERE !!!
        GameMgr.Instance.state = GameMgr.GameState.StartOfRace;
        ////

        GameObject Cars = new GameObject("Cars");

        Vector3 pos = new Vector3();

        for (int count = 0; count < totalOfCars - 1; ++count)
        {
            pos = startingLine.Floor;
            pos += startingLine.transform.right * ((count % 4) * 6f - 9f);
            pos -= startingLine.transform.forward * (count * 4f + 5f);
            pos.y += 3f;

            GameObject newBotCar = Instantiate(BotCarPrefab[count], pos, startingLine.transform.rotation) as GameObject;
            newBotCar.transform.parent = Cars.transform;
            newBotCar.name = "BotCar" + (count+1).ToString();
            newBotCar.tag = "Bot";

            newBotCar.AddComponent<CarAutoDrive>();
			newBotCar.AddComponent<AIItemHandler>();
        }

        pos = startingLine.Floor;
        pos += startingLine.transform.right * (((totalOfCars - 1) % 4) * 6f - 9f);
        pos -= startingLine.transform.forward * ((totalOfCars-1) * 4f + 5f);
        pos.y += 3f;

        GameObject playerCar = Instantiate(PlayerCarPrefab, pos, startingLine.transform.rotation) as GameObject;
        playerCar.transform.parent = Cars.transform;
        playerCar.name = "PlayerCar";
        playerCar.tag = "Player";
        playerCar.AddComponent<Player>();
        playerCar.AddComponent<PlayerCarPreController>();
        //playerCar.AddComponent<CheatyBoost>();
        CarAutoDrive auto = playerCar.AddComponent<CarAutoDrive>();
        auto.enabled = false;

        GameObject ranker = new GameObject("Ranker");
        ranker.AddComponent<Ranker>();


        Cars.BroadcastMessage("SetTarget", startingLine.NextWp.transform);
        Cars.BroadcastMessage("SetStartingWaypoint", startingLine);

        raceClock = new Clock();
    }

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}
