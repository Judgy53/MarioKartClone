using UnityEngine;
using System.Collections;

public class ItemBanana : Item, IItemUpdatable {

	public Banana banana = null;
	public bool Summoned = false;

	protected const float OrigDistFromCar = 5.0f;
	protected float DistFromCar;

	private GameObject prefab = null;

	public ItemBanana()
	{
		prefab = Resources.Load ("prefabs/Banana") as GameObject;
		DistFromCar = OrigDistFromCar;
	}

	public override Item StartUse(CarItemHandler car, bool useBehind)
	{
		Vector3 translation = car.transform.position + car.transform.forward * DistFromCar * -1;
		
		Quaternion rotation = car.transform.rotation;

		banana = SummonBanana (translation, rotation);

		Summoned = true;
		
		return this;
	}

	public override Item StopUse(CarItemHandler car, bool useBehind)
	{
		if (banana == null)
			return null;
		
		if (useBehind) 
		{
			banana.transform.position = car.transform.position + car.transform.forward * DistFromCar * -1;
			banana.transform.rotation = Quaternion.LookRotation (-car.transform.forward);
		}
		else 
		{
			banana.transform.rotation = Quaternion.LookRotation(car.transform.forward);
			banana.transform.position = car.transform.position + car.transform.forward * DistFromCar;
		}
		
		ReleaseBanana (banana);

		if (!useBehind)
			banana.GetComponent<Rigidbody> ().AddForce (banana.transform.forward * 70f + Vector3.up * 20f, ForceMode.Impulse);
		
		return null;
	}

	protected Banana SummonBanana(Vector3 position, Quaternion rotation)
	{
		GameObject GaO = Object.Instantiate(prefab, position, rotation) as GameObject;
		
		GaO.transform.Translate(0f, GaO.GetComponent<BoxCollider> ().bounds.extents.y, 0f);
		
		return GaO.GetComponent<Banana> ();
	}
	
	protected void ReleaseBanana(Banana banana)
	{
		banana.Updatable = true;
		banana.transform.Translate(0f, banana.GetComponent<BoxCollider> ().bounds.extents.y, 0f);
	}
	
	protected void ReleaseBanana(Banana banana, Vector3 position, Quaternion rotation)
	{
		banana.transform.position = position;
		
		banana.transform.rotation = rotation;
		
		ReleaseBanana (banana);
	}

	public virtual bool Update(CarItemHandler car)
	{
		if (!Summoned)
			return true;
		else if (banana == null)
			return false;
		else 
		{
			banana.transform.position = car.transform.position + car.transform.forward * DistFromCar * -1;
			banana.transform.rotation = car.transform.rotation;
		}
		
		return true;
	}
}
