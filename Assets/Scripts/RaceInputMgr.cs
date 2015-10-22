using UnityEngine;
using System.Collections;

public class RaceInputMgr : MonoSingleton<RaceInputMgr> {

    private CarWaypointHandler wpHandler; // Waypoint handler of player's car.

	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        wpHandler = player.GetComponent<CarWaypointHandler>();
	}

    void Update () {
        if (Input.GetButtonDown("Fire1"))
            wpHandler.TeleportToLastWaypoint();

        if (Input.GetButtonDown("Cancel"))
            RaceUIMgr.Instance.Pause();

		if (Input.GetKeyDown(KeyCode.Space) && GameMgr.Instance.state != GameMgr.GameState.EndOfRace)
			Player.Instance.StartUseItem(Input.GetKey(KeyCode.DownArrow));
		
		if (Input.GetKeyUp(KeyCode.Space) && GameMgr.Instance.state != GameMgr.GameState.EndOfRace)
			Player.Instance.StopUseItem(Input.GetKey(KeyCode.DownArrow));
    }

    public Vector2 GetXYAxes()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
