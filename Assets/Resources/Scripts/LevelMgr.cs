using UnityEngine;
using System.Collections;

public class LevelMgr : MonoSingleton<LevelMgr> {

    [SerializeField]
    Waypoint StartingLine = null;

    [SerializeField]
    private int TotalOfCars = 8;

    [SerializeField]
    private int MaxLaps = 3;

    [SerializeField]
    private GameObject PlayerCarPrefab = null;
    [SerializeField]
    private GameObject BotCarPrefab = null;

    public int LapsToDo { get { return MaxLaps; } }

    public Clock raceClock;

    void Awake()
    {
        StartingLine.SendMessage("Awake"); // Fuck unity.

        //// MUST NOT STAY THERE !!!
        GameMgr.Instance.state = GameMgr.GameState.StartOfRace;
        ////

        GameObject Cars = new GameObject("Cars");

        Vector3 pos = new Vector3();

        for (int count = 0; count < TotalOfCars - 1; ++count)
        {
            pos = StartingLine.Floor;
            pos.x += (count % 4) * 6f - 9f;
            pos.z -= count * 3f + 5f;
            pos.y += 3f;

            GameObject newBotCar = Instantiate(BotCarPrefab, pos, StartingLine.transform.rotation) as GameObject;
            newBotCar.transform.parent = Cars.transform;
            newBotCar.name = "BotCar" + (count+1).ToString();
        }

        pos = StartingLine.Floor;
        pos.x += ((TotalOfCars-1) % 4) * 6f - 9f;
        pos.z -= (TotalOfCars-1) * 3f + 5f;
        pos.y += 3f;

        GameObject playerCar = Instantiate(PlayerCarPrefab, pos, StartingLine.transform.rotation) as GameObject;
        playerCar.transform.parent = Cars.transform;
        playerCar.name = "PlayerCar";

        GameObject ranker = new GameObject("Ranker");
        ranker.AddComponent<Ranker>();


        Cars.BroadcastMessage("SetTarget", StartingLine.NextWp.transform);
        Cars.BroadcastMessage("SetStartingWaypoint", StartingLine);

        raceClock = new Clock();
    }

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}
