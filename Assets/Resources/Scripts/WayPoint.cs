using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    [SerializeField]
    private Waypoint NextWaypoint = null;

    private Vector3 floor;

    public Waypoint NextWp { get { return NextWaypoint; } }
    public Vector3 Floor { get { return floor; } }

	// Use this for initialization
	void Awake () {
        floor = FindFloor();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bot")
        {
            collider.gameObject.SendMessage("SetTarget", NextWaypoint.transform);
        }

        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Bot")
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
