using UnityEngine;
using System.Collections;

public class GreenShell : MonoBehaviour, IItemCollision {

	protected Rigidbody body = null;

	[SerializeField]
	protected float Speed = 70f;
	[SerializeField]
	protected float GravityModifier = -7f;

	[HideInInspector]
	public bool Updatable = true;
	
	void Start () {
		body = GetComponent<Rigidbody> ();
	}

	public virtual void Init()
	{
		if (Updatable)
			body.velocity = transform.forward;
	}

	protected virtual void FixedUpdate () {
		if (Updatable) 
		{
			Vector3 vel = body.velocity.normalized * Speed;
			vel.y = body.velocity.y;

			body.velocity = vel;

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

	private void OnOutOfMap()
	{
		Destroy (gameObject);
	}
}
