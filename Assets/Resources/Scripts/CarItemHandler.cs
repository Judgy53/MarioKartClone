using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(CarController))]
public class CarItemHandler : MonoBehaviour {

	protected Item currentItem = null;
	public Item CurrentItem { get { return currentItem; } }

	private CarController carCtrl = null;
	public CarController Controller { get { return carCtrl; } }

	private Rigidbody body = null;

	[SerializeField]
	private float OnHitTurns = 2f;
	[SerializeField]
	private float OnHitSpeedRatio = 0.1f;

	[SerializeField]
	private DrivingCamera cam = null;

	private bool Hitted = false;

	void Start()
	{
		body = GetComponent<Rigidbody> ();
	}

	public bool OnPickItemBox(Item item)
	{
		if (currentItem != null)
			return false;

		currentItem = item;

		carCtrl = GetComponent<CarController> ();

		return true;
	}

	protected void useItem(bool useBehind)
	{
		if (currentItem != null)
			currentItem = currentItem.use (this, useBehind);
	}
	
	public void OnHit()
	{
		StartCoroutine ("ApplyHit");
	}

	private IEnumerator ApplyHit()
	{
		if (!Hitted) 
		{
			Hitted = true;

			float steps = OnHitTurns * 36;
			Vector3 brakeSpeed = body.velocity * 0.9f / steps;
			
			body.AddForce (0f, OnHitTurns * 3.5f * body.mass, 0f, ForceMode.Impulse);

			if(gameObject.tag == "Player")
				cam.FollowCar = false;
			
			for(int i = 0; i < OnHitTurns * 36; i++)
			{
				body.velocity -= brakeSpeed;
				transform.Rotate(0f, 10f, 0f);
				yield return new WaitForSeconds(0.01f); 
			}

			if(gameObject.tag == "Player")
				cam.FollowCar = true;

			Hitted = false;
		}
	}
}
