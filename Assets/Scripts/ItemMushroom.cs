using UnityEngine;
using System.Collections;

public class ItemMushroom : Item {

	[SerializeField]
	private float SpeedBoost = 25f;

	public override Item use(CarItemHandler car)
	{
		Rigidbody body = car.Controller.GetComponent<Rigidbody> ();
		body.velocity += SpeedBoost * body.velocity.normalized;

		car.Controller.SendMessage ("CapSpeed");
		return null;
	}
}
