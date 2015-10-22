using UnityEngine;
using System.Collections;
using System.Linq;

public class GreenShell : MonoBehaviour, IItemCollision {

	protected Rigidbody body = null;

	[SerializeField]
	protected float Speed = 100f;
	[SerializeField]
	protected float GravityModifier = -7f;

	[HideInInspector]
	public bool Updatable = true;
	
	void Start () {
		body = GetComponent<Rigidbody> ();

		body.centerOfMass = new Vector3 (0f, -2f, 0f);
	}

	public virtual void Init()
	{
		if (Updatable)
			body.velocity = transform.forward;
	}

	protected virtual void FixedUpdate () {
		if (Updatable) 
		{
			Vector3 start = transform.position + transform.forward;

			RaycastHit[] hits = Physics.RaycastAll(start, -transform.up, Mathf.Infinity).OrderBy(h=>h.distance).ToArray();

			if(hits.Length > 0)
			{
				foreach(RaycastHit hit in hits)
				{
					if(hit.collider.isTrigger)
						continue;

					body.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

					break;
				}
			}

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
