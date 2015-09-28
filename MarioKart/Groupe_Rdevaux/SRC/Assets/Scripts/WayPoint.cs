using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour {

    [SerializeField]
    private WayPoint NextWayPoint = null;

    public WayPoint Next { get { return NextWayPoint; } }

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
            collider.gameObject.SendMessage("SetTarget", NextWayPoint.transform);
        }

        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Bot")
        {
            collider.gameObject.SendMessage("SetLastWayPoint", this);
        }
    }
}
