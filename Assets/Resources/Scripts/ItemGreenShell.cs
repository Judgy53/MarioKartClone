using UnityEngine;
using System.Collections;

public class ItemGreenShell : Item {
	
	public override Item StartUse(CarItemHandler car, bool useBehind)
	{
		GameObject prefab = Resources.Load ("prefabs/GreenShell") as GameObject;

		float dir = useBehind ? -0.75f : 1f;

		Vector3 translation = car.transform.position + car.transform.forward * 6f * dir;

		Quaternion rotation = car.transform.rotation;
		if (useBehind)
			rotation = Quaternion.LookRotation (-car.transform.forward);

		GameObject GaO = Object.Instantiate(prefab, translation, rotation) as GameObject;

		GaO.transform.Translate(0f, GaO.GetComponent<BoxCollider> ().bounds.extents.y, 0f);


		return null;
	}

	public override Item StopUse(CarItemHandler car, bool useBehind)
	{
		return this;
	}
}
