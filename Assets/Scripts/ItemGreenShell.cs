using UnityEngine;
using System.Collections;

public class ItemGreenShell : Item, IItemUpdatable {

	public bool Summoned = false;
	protected GreenShell Shell = null;

	private float DistFromCar = 5.0f;

	protected GameObject prefab = null;

	public ItemGreenShell()
	{
		prefab = Resources.Load ("prefabs/GreenShell") as GameObject;
	}
	
	public override Item StartUse(CarItemHandler car, bool useBehind)
	{
		Vector3 translation = car.transform.position + car.transform.forward * DistFromCar * -1;

		Quaternion rotation = car.transform.rotation;

		Shell = SummonShell(translation, rotation);

		Shell.Updatable = false;

		Summoned = true;

		return this;
	}

	public override Item StopUse(CarItemHandler car, bool useBehind)
	{
		if (Shell == null)
			return null;

		if (useBehind) 
		{
			Shell.transform.position = car.transform.position + car.transform.forward * DistFromCar * -1;
			Shell.transform.rotation = Quaternion.LookRotation (-car.transform.forward);
		}
		else 
		{
			Shell.transform.rotation = Quaternion.LookRotation(car.transform.forward);
			Shell.transform.position = car.transform.position + car.transform.forward * DistFromCar;
		}

		Shell.transform.Rotate (Vector3.forward.x - Shell.transform.eulerAngles.x, 0f, 0f);

		ReleaseShell (Shell);

		return null;
	}

	public virtual bool Update(CarItemHandler car)
	{
		if (!Summoned)
			return true;
		else if (Shell == null)
			return false;
		else 
		{
			Shell.transform.position = car.transform.position + car.transform.forward * DistFromCar * -1;
			Shell.transform.rotation = car.transform.rotation;
		}

		return true;
	}

	protected GreenShell SummonShell(Vector3 position, Quaternion rotation)
	{
		GameObject GaO = Object.Instantiate(prefab, position, rotation) as GameObject;
		
		GaO.transform.Translate(0f, GaO.GetComponent<BoxCollider> ().bounds.extents.y, 0f);

		return GaO.GetComponent<GreenShell> ();
	}

	protected void ReleaseShell(GreenShell shell)
	{
		shell.Updatable = true;
		shell.Init ();
	}

	protected void ReleaseShell(GreenShell shell, Vector3 position, Quaternion rotation)
	{
		shell.transform.position = position;
	
		shell.transform.rotation = rotation;

		ReleaseShell (shell);
	}

	public override SpriteIndex GetSpriteIndex()
	{
		return SpriteIndex.GreenShell;
	}

}
