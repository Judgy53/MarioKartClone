using UnityEngine;
using System.Collections;

public class ItemBoxFake : ItemBox {

	public bool Set = false;

	private Collider col = null;

	public bool Updatable = true;

	// Use this for initialization
	protected override void Start () {
		col = GetComponent<Collider> ();

		Color c = GetColor ();
		c.r = 1f;
		c.g = 0f;
		c.b = 0f;
		SetColor (c);
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (!Updatable)
			return;

		if (!Set) 
		{
			GetComponent<Rigidbody>().AddForce (0f, -10f, 0f, ForceMode.Force);
		} 
		else 
		{
			base.Update();

			Color c = GetColor ();
			c.r = (128f - ColorCursor) / 128f;
			c.g = 0f;
			c.b = ColorCursor / 128f;
			SetColor (c);
		}
	}

	void OnTriggerEnter(Collider collid)
	{
		if (!Updatable)
			return;

		CarItemHandler car = collid.GetComponent<CarItemHandler> ();

		if (car != null) 
		{
			car.OnHit(gameObject);
			Explode();
			Destroy (gameObject);
		}
	}

	void OnCollisionStay(Collision collision)
	{
		if (!Updatable)
			return;

		if (IsGrounded())
		{
			Destroy (GetComponent<Rigidbody> ());
			col.isTrigger = true;

			transform.Translate(0f, -0.5f, 0f);

			Setup ();
			
			Set = true;
		}
	}
	
	bool IsGrounded ()
	{
		int layerMask = ~(1 << LayerMask.NameToLayer("FakeBox")); // Dont check for collisions with another fakebox
		return Physics.Raycast (transform.position, - Vector3.up, col.bounds.extents.y + 0.6f, layerMask);
	}

	private void NeverGonnaGiveYouUp()
	{
		Destroy (gameObject);
	}
}
