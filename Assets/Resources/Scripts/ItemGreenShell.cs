using UnityEngine;
using System.Collections;

public class ItemGreenShell : Item {
	
	public override Item use(CarItemHandler car, bool useBehind)
	{
		GameObject prefab = Resources.Load ("prefabs/GreenShell") as GameObject;

		prefab.transform.position = car.transform.position + car.transform.forward;

		float dir = useBehind ? -1f : 1f;

		Vector3 translation = car.transform.position + car.transform.forward * 5f * dir;
		//Debug.Log (prefab.GetComponent<Collider> ().bounds.extents.y);
		//translation.y += prefab.GetComponent<BoxCollider> ().bounds.extents.y;

		Quaternion rotation = car.transform.rotation;
		if (useBehind)
			rotation = Quaternion.LookRotation (-car.transform.forward);

		GameObject GaO = Object.Instantiate(prefab, translation, rotation) as GameObject;

		GaO.transform.Translate(0f, GaO.GetComponent<BoxCollider> ().bounds.extents.y, 0f);


		return null;
	}
}
