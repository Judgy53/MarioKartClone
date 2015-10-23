using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    [SerializeField]
    private bool DontRespawnHere = false;

    private Waypoint nextWaypoint = null;

    private Vector3 floor;

    public Waypoint NextWp { get { return nextWaypoint; } }
    public Vector3 Floor { get { return floor; } }

    public bool CanRespawnHere { get { return !DontRespawnHere; } }

	// Use this for initialization
	void Awake () {
        floor = FindFloor();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetNextWaypoint(Waypoint wp)
    {
        nextWaypoint = wp;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bot")
        {
            collider.gameObject.SendMessage("SetTarget", nextWaypoint.transform);
        }

		if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Bot" || collider.gameObject.tag == "RedShell")
        {
            collider.gameObject.SendMessage("SetLastWaypoint", this);
        }
    }


    private Vector3 FindFloor()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit found;

        if (Physics.Raycast(ray, out found))
        {
            return found.point;
        }

        else
        {
            Debug.Log("No floor was found under waypoint.");
            return transform.position;
        }
    }
}
