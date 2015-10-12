using UnityEngine;
using System.Collections;

public class GreenShell : MonoBehaviour, IItemCollision {

	private Rigidbody body = null;

	[SerializeField]
	private float Speed = 70f;
	[SerializeField]
	private float GravityModifier = -7f;

	public bool Updatable = true;
	
	void Start () {
		body = GetComponent<Rigidbody> ();
	}

	public void Init()
	{
		if (Updatable)
			body.velocity = transform.forward;
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
