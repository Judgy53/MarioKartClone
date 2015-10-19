using UnityEngine;
using System.Collections;

public class ItemBobOmb : Item, IItemUpdatable {

	private bool Summoned = false;
	private BobOmb bomb = null;
	
	private float DistFromCar = 5.0f;
	
	private GameObject prefab = null;
	
	public ItemBobOmb()
	{
		prefab = Resources.Load ("prefabs/BobOmb") as GameObject;
	}
	
	public override Item StartUse(CarItemHandler car, bool useBehind)
	{		
		Vector3 translation = car.transform.position + car.transform.forward * DistFromCar * -1;
		
		Quaternion rotation = car.transform.rotation;
		
		bomb = SummonBomb (translation, rotation);
		
		bomb.Updatable = false;
		
		Summoned = true;
		
		return this;
	}
	
	public override Item StopUse(CarItemHandler car, bool useBehind)
	{
		if (bomb == null)
			return null;
		
		if (useBehind) 
		{
			bomb.transform.position = car.transform.position + car.transform.forward * DistFromCar * -1;
			bomb.transform.rotation = Quaternion.LookRotation (-car.transform.forward);
		}
		else 
		{
			bomb.transform.rotation = Quaternion.LookRotation(car.transform.forward);
			bomb.transform.position = car.transform.position + car.transform.forward * DistFromCar;
		}
		
		ReleaseBomb (bomb);
		
		if (!useBehind)
			bomb.GetComponent<Rigidbody> ().AddForce (bomb.transform.forward * 70f + Vector3.up * 20f, ForceMode.Impulse);
		
		return null;
	}
	
	protected BobOmb SummonBomb(Vector3 position, Quaternion rotation)
	{
		GameObject GaO = Object.Instantiate(prefab, position, rotation) as GameObject;
		
		GaO.transform.Translate(0f, 1.5f, 0f);
		
		return GaO.GetComponent<BobOmb> ();
	}
	
	protected void ReleaseBomb(BobOmb bomb)
	{
		bomb.Updatable = true;
		bomb.transform.Translate(0f, 1.5f, 0f);
	}
	
	protected void ReleaseBomb(BobOmb bomb, Vector3 position, Quaternion rotation)
	{
		bomb.transform.position = position;
		
		bomb.transform.rotation = rotation;
		
		ReleaseBomb (bomb);
	}
	
	public bool Update(CarItemHandler car)
	{
		if (!Summoned)
			return true;
		else if (bomb == null)
			return false;
		else 
		{
			bomb.transform.position = car.transform.position + car.transform.forward * DistFromCar * -1;
			bomb.transform.rotation = car.transform.rotation;
			
			bomb.transform.Translate(0f, 1.5f, 0f);
		}

		return true;
	}

	public override SpriteIndex GetSpriteIndex()
	{
		return SpriteIndex.BobOmb;
	}
}
