using UnityEngine;
using System.Collections;

public class Banana : MonoBehaviour {

	private Collider col = null;

	public bool Updatable = true;
	public GameObject owner = null;
	
	// Use this for initialization
	void Start () {
		col = GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!IsGrounded()) 
			GetComponent<Rigidbody>().AddForce (0f, -10f, 0f, ForceMode.Force);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		IItemCollision collider = collision.gameObject.GetComponent<IItemCollision> ();

		Banana bCollider = collision.gameObject.GetComponent<Banana> ();

		if (bCollider != null && !bCollider.Updatable) // don't collide with other bananas from the triple bonus
			return;

		if (owner != null && owner == collision.gameObject) // don't collide with owner
			return;

		if (collider != null) 
		{
			collider.OnHit (gameObject);
			Destroy (gameObject);
		}
	}
	
	bool IsGrounded ()
	{
		int layerMask = ~(1 << LayerMask.NameToLayer("Banana")); // Dont check for collisions with another banana
		return Physics.Raycast (transform.position, - Vector3.up, col.bounds.extents.y + 0.6f, layerMask);
	}

	public void OnHit(GameObject GaO)
	{
		Destroy (gameObject);
		//gameObject.SetActive (false);
	}
	
	private void OnOutOfMap()
	{
		Destroy (gameObject);
	}

	public void SetColor(Color color)
	{
		GetComponent<Renderer> ().material.color = color;
	}
}
