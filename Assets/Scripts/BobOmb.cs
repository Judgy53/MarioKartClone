using UnityEngine;
using System.Collections;

public class BobOmb : MonoBehaviour {
	
	public bool Set = false;

	public bool Updatable = true;

	private float radius = 0f;

	private bool Exploding = false;

	private Color baseColor = Color.black;
	private Color flashColor = Color.white;

	private GameObject ExplosionPrefab = null;

	void Start () {
		radius = transform.localScale.y;

		ExplosionPrefab = Resources.Load ("prefabs/Explosion") as GameObject;
	}

	void Update () {
		if (!Updatable)
			return;

		if(!IsGrounded())
			GetComponent<Rigidbody>().AddForce (0f, -10f, 0f, ForceMode.Force);

		if (!Set)
			Setup ();
	}
	
	void OnTriggerEnter(Collider collid)
	{
		if (!Updatable)
			return;
		
		CarItemHandler car = collid.GetComponent<CarItemHandler> ();
		
		if (car != null) 
			Exploding = true;
	}

	void Setup()
	{
		StartCoroutine ("StartExploding");
		Set = true;
	}

	IEnumerator StartExploding()
	{
		bool state = false;
		for (int i = 0; i < 20 && !Exploding; i++) 
		{
			GetComponent<Renderer>().material.color = state ? baseColor : flashColor;
			state = !state;
			yield return new WaitForSeconds (0.5f - (float)i * 0.025f);
		}

		GetComponent<Renderer>().material.color = state ? baseColor : flashColor;
		StartCoroutine ("Explode");
	}

	IEnumerator Explode()
	{
		Exploding = true;

		yield return new WaitForSeconds (0.2f);

		Instantiate (ExplosionPrefab, transform.position, transform.rotation);

		Destroy (gameObject);
	}
	
	bool IsGrounded ()
	{
		return Physics.Raycast (transform.position, - Vector3.up, radius + 0.1f);
	}
	
	private void OnOutOfMap()
	{
		Destroy (gameObject);
	}
}
