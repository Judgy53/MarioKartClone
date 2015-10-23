using UnityEngine;
using System.Collections;

public class ItemTrappedCube : Item, IItemUpdatable {

	public bool Summoned = false;
	protected ItemBoxFake box = null;

	private float StartDistFromCar = 4.0f;
	private float DistFromCar = 7.0f;
	
	private GameObject prefab = null;

	public ItemTrappedCube()
	{
		prefab = Resources.Load ("prefabs/ItemBoxFake") as GameObject;
	}
	
	public override Item StartUse(CarItemHandler car, bool useBehind)
	{		
		Vector3 translation = car.transform.position + car.transform.forward * StartDistFromCar * -1;
		
		Quaternion rotation = car.transform.rotation;
		
		box = SummonBox (translation, rotation);

		box.Updatable = false;
		
		Summoned = true;
		
		return this;
	}

	public override Item StopUse(CarItemHandler car, bool useBehind)
	{
		if (box == null)
			return null;
		
		if (useBehind) 
		{
			box.transform.position = car.transform.position + car.transform.forward * DistFromCar * -1;
			box.transform.rotation = Quaternion.LookRotation (-car.transform.forward);
		}
		else 
		{
			box.transform.rotation = Quaternion.LookRotation(car.transform.forward);
			box.transform.position = car.transform.position + car.transform.forward * DistFromCar;
		}
		
		ReleaseBox (box);
		
		if (!useBehind)
			box.GetComponent<Rigidbody> ().AddForce (box.transform.forward * 70f + Vector3.up * 15f, ForceMode.Impulse);
		
		return null;
	}

	protected ItemBoxFake SummonBox(Vector3 position, Quaternion rotation)
	{
		GameObject GaO = Object.Instantiate(prefab, position, rotation) as GameObject;
		
		GaO.transform.Translate(0f, GaO.GetComponent<BoxCollider> ().bounds.extents.y, 0f);
		
		return GaO.GetComponent<ItemBoxFake> ();
	}
	
	protected void ReleaseBox(ItemBoxFake box)
	{
		box.Updatable = true;
		box.transform.Translate(0f, box.GetComponent<BoxCollider> ().bounds.extents.y, 0f);
	}
	
	protected void ReleaseBox(ItemBoxFake box, Vector3 position, Quaternion rotation)
	{
		box.transform.position = position;
		
		box.transform.rotation = rotation;
		
		ReleaseBox (box);
	}
	
	public bool Update(CarItemHandler car)
	{
		if (!Summoned)
			return true;
		else if (box == null)
			return false;
		else 
		{
			box.transform.position = car.transform.position + car.transform.forward * StartDistFromCar * -1;
			box.transform.rotation = car.transform.rotation;

			box.transform.Translate(0f, box.GetComponent<BoxCollider> ().bounds.extents.y, 0f);
		}
		
		return true;
	}

	public override SpriteIndex GetSpriteIndex()
	{
		return SpriteIndex.TrappedCube;
	}
}
