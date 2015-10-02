using UnityEngine;
using System.Collections;

public class LevelMgr : MonoSingleton<LevelMgr> {

    [SerializeField]
    Waypoint StartingLine = null;

    [SerializeField]
    private int TotalOfCars = 8;

    [SerializeField]
    private GameObject PlayerCarPrefab = null;
    [SerializeField]
    private GameObject BotCarPrefab = null;

    public Clock raceClock;

    void Awake()
    {
        GameObject Cars = new GameObject("Cars");

        Vector3 pos = new Vector3();

        for (int count = 0; count < TotalOfCars - 1; ++count)
        {
            pos = StartingLine.transform.position;
            pos.x += (count % 4) * 6 - 9;
            pos.z -= count * 3 + 5;

            GameObject newBotCar = Instantiate(BotCarPrefab, pos, StartingLine.transform.rotation) as GameObject;
            newBotCar.transform.parent = Cars.transform;
            newBotCar.name = "BotCar" + (count+1).ToString();
        }

        pos = StartingLine.transform.position;
        pos.x += ((TotalOfCars-1) % 4) * 6 - 9;
        pos.z -= (TotalOfCars-1) * 3 + 5;

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
