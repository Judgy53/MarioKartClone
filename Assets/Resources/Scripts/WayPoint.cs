using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    [SerializeField]
    private Waypoint NextWaypoint = null;

    public Waypoint NextWp { get { return NextWaypoint; } }

	// Use this for initialization
	void Start () {
	
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
}
