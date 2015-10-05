using UnityEngine;
using System.Collections;

public class ItemBanana : Item {

	public override Item use(CarItemHandler car, bool useBehind)
	{
		GameObject prefab = Resources.Load ("prefabs/Banana") as GameObject;
		
		float dir = useBehind ? -0.75f : 1f; // 0.75 to spawn near the car to be able to see it
		
		Vector3 translation = car.transform.position + car.transform.forward * 7f * dir;
		
		Quaternion rotation = car.transform.rotation;
		if (useBehind)
			rotation = Quaternion.LookRotation (-car.transform.forward);

		rotation.x = 0f;
		
		GameObject GaO = Object.Instantiate(prefab, translation, rotation) as GameObject;
		
		GaO.transform.Translate(0f, GaO.GetComponent<BoxCollider> ().bounds.extents.y + 0.5f, 0f);
		
		if (!useBehind) 
			GaO.GetComponent<Rigidbody>().AddForce(GaO.transform.forward * 70f + Vector3.up * 20f, ForceMode.Impulse);
		
		
		return null;
	}
}
