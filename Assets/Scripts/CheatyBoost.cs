using UnityEngine;
using System.Collections;

public class CheatyBoost : MonoBehaviour {

    private Rigidbody rb = null;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("q"))
            rb.velocity += rb.transform.forward;
	}
}
