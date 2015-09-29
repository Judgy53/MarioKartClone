using UnityEngine;
using System.Collections;

public class ItemGreenShell : Item {
	
	public override Item use(CarItemHandler car, bool useBehind)
	{
		GameObject Gao = Resources.Load ("prefabs/GreenShell") as GameObject;

		Gao.transform.position = car.transform.position + car.transform.forward;

		float dir = useBehind ? -1f : 1f;
		Vector3 translation = car.transform.position + car.transform.forward * 5f * dir;
		Quaternion rotation = car.transform.rotation;

		if (useBehind)
			rotation = Quaternion.LookRotation (-car.transform.forward);

		Object.Instantiate(Gao, translation, rotation);

		return null;
	}
}
