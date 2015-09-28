using UnityEngine;
using System.Collections;

public class WayPointScript : MonoBehaviour {

    [SerializeField]
    private Transform NextWayPoint = null;

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
            collider.gameObject.SendMessage("SetTarget", NextWayPoint);
        }

        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Bot")
        {
            collider.gameObject.SendMessage("SetLastWayPoint", transform);
        }
    }
}
