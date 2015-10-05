using UnityEngine;
using System.Collections;

public class GreenShell : MonoBehaviour, IItemCollision {

	private Rigidbody body = null;

	[SerializeField]
	private float Speed = 40f;
	[SerializeField]
	private float GravityModifier = -5f;

	public bool Updatable = true;
	
	void Start () {
		body = GetComponent<Rigidbody> ();

		if (Updatable)
			body.AddForce (transform.forward, ForceMode.Impulse);
	}

	void FixedUpdate () {
		if (Updatable) 
		{
			body.velocity = body.velocity.normalized * Speed;
			body.AddForce (0f, GravityModifier, 0f, ForceMode.Force);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		IItemCollision col = collision.gameObject.GetComponent<IItemCollision> ();

		if (col != null) 
		{
			col.OnHit(gameObject);

			Destroy (gameObject);
		}
	}

	public void OnHit(GameObject GaO)
	{
		Destroy (gameObject);
	}

	private void NeverGonnaGiveYouUp()
	{
		Destroy (gameObject);
	}
}
