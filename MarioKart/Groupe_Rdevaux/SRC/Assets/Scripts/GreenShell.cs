using UnityEngine;
using System.Collections;

public class GreenShell : MonoBehaviour {

	private Rigidbody body = null;

	[SerializeField]
	private float Speed = 30f;
	[SerializeField]
	private float GravityModifier = -5f;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody> ();

		body.AddForce (transform.forward, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody body = GetComponent<Rigidbody> ();
		body.velocity = body.velocity.normalized * Speed;

		body.AddForce (0f, GravityModifier, 0f, ForceMode.Force);
	}

	private void NeverGonnaGiveYouUp()
	{
		Destroy (gameObject);
	}
}
