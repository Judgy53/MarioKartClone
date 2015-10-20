using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(CarController))]
public class CarItemHandler : MonoBehaviour, IItemCollision {

	private Item currentItem = null;
	public Item CurrentItem { get { return currentItem; } }

	protected Item pickedItem = null; // used for random display
	protected bool RandomDisplaying = true;

	private CarController carCtrl = null;
	public CarController Controller { get { return carCtrl; } }

	private Rigidbody body = null;

	[SerializeField]
	private float OnHitTurns = 2f;

	private DrivingCamera cam = null;

	private bool Hitted = false;

	private bool UseStarted = false;

	void Start()
	{
		body = GetComponent<Rigidbody> ();
		carCtrl = GetComponent<CarController> ();
        cam = Camera.main.GetComponent<DrivingCamera>();
	}

	void FixedUpdate()
	{
		if (currentItem is IItemUpdatable && pickedItem == null) // should not update while random picking 
		{
			bool success = ((IItemUpdatable)currentItem).Update(this);

			if(!success)
				currentItem = null;
		}
	}

	public bool OnPickItemBox(Item item)
	{
		if (currentItem != null || pickedItem != null)
			return false;

		pickedItem = item;

		StartCoroutine ("RandomItemDisplay");

		return true;
	}

	private IEnumerator RandomItemDisplay()
	{
		for (int i = 0; i < 30 && RandomDisplaying; i++) 
		{
			Item newItem = Item.RandomItem();

			while(newItem == null || (currentItem != null && (newItem.ToString() == currentItem.ToString())))
				newItem = Item.RandomItem();
			currentItem = newItem;
			yield return new WaitForSeconds(0.1f);
		}

		currentItem = pickedItem;
		pickedItem = null;

		RandomDisplaying = true;
	}

	public void StartUseItem(bool useBehind)
	{
		if (CanUseItem ()) 
		{
			UseStarted = true;
			currentItem = currentItem.StartUse (this, useBehind);
		}
	}

	public void StopUseItem(bool useBehind)
	{
		if(CanUseItem() && UseStarted)
			currentItem = currentItem.StopUse (this, useBehind);

		UseStarted = false;
	}

	private bool CanUseItem()
	{
		if (Hitted) //can't use item while hit animation 
			return false;
		
		if (pickedItem != null) // random displaying
			RandomDisplaying = false;
		else if (currentItem != null) // normal use
			return true;

		return false;
	}
	
	public void OnHit(GameObject GaO)
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

			//Hard Slow on impact
			body.velocity /= 2f;
			brakeSpeed /= 2f;

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
