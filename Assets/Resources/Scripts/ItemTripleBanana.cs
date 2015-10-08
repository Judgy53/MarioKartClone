using UnityEngine;
using System.Collections;

public class ItemTripleBanana : ItemBanana, IItemUpdatable {

	private bool summoned = false;
	private int launched = 0;

	private const int BananaCount = 3;
	private Banana[] bananas = new Banana[BananaCount];

	private float BaseOffset = 3f;
	private float DistBetweenBananas = 2f;

	private GameObject prefab;
	private float height;

	public ItemTripleBanana()
	{
		prefab = Resources.Load ("Prefabs/Banana") as GameObject;

		//get collider height by creating a temp banana
		GameObject temp = GameObject.Instantiate (prefab) as GameObject;
		height = temp.GetComponent<Collider> ().bounds.extents.y;
		GameObject.Destroy (temp);
	}

	public override Item StartUse(CarItemHandler car, bool useBehind)
	{
		if (!summoned) 
		{
			SummonBananas(car);
			summoned = true;
			return this;
		}

		LaunchBanana (car, useBehind);

		if (launched < BananaCount) 
			return this;

		return null;
	}

	public override Item StopUse(CarItemHandler car, bool useBehind)
	{
		return this;
	}

	void SummonBananas(CarItemHandler car)
	{
		for (int i = 0; i < BananaCount; i++) 
		{
			Vector3 position = car.transform.position - (car.transform.forward * BaseOffset + car.transform.forward * DistBetweenBananas * (float)i);
			position += new Vector3(0f, height, 0f);

			Quaternion rotation = car.transform.rotation;

			GameObject gao = Object.Instantiate(prefab) as GameObject;

			gao.transform.position = position;
			gao.transform.rotation = rotation;

			Banana banana = gao.GetComponent<Banana>();
			banana.Updatable = false;
			banana.owner = car.gameObject;

			bananas[i] = banana;
		}
	}

	void LaunchBanana(CarItemHandler car, bool useBehind)
	{
		int bananaPos = -1;

		if (useBehind)
			bananaPos = BananaCount - 1;
		else
			bananaPos = 0;

		while ((bananas[bananaPos] == null || bananas[bananaPos].owner == null) && bananaPos >= 0 && bananaPos < BananaCount)
			bananaPos += useBehind ? -1 : 1;

		Banana currentBanana = bananas [bananaPos];

		if (currentBanana == null)
			return;

		if (!useBehind) 
		{
			Object.Destroy (currentBanana.gameObject);
			base.StartUse (car, useBehind);
		} 
		else 
		{
			Quaternion rot = currentBanana.transform.rotation;
			rot.x = 0f;
			rot.z = 0f;

			currentBanana.transform.rotation = rot;
		}

		currentBanana.Updatable = true;
		currentBanana.owner = null;

		bananas [bananaPos] = null; // set it to null because we don't want to interact with it anymore
		//currentBanana.gameObject.SetActive(false); // desactivate because we don't want to interact with it anymore


		launched++;
	}

	public bool Update (CarItemHandler car) {
		if (!summoned)
			return true;

		Transform target = car.transform;

		bool updated = false;

		for (int i = 0; i < BananaCount; i++) 
		{
			Vector3 targetPos = car.transform.position - (target.forward * BaseOffset + target.forward * DistBetweenBananas * (float)i);
			targetPos += new Vector3(0f, height, 0f);
			
			Quaternion targetRot = target.rotation;

			int bananaId = i;

			Banana currentBanana = bananas [bananaId];
			
			while((currentBanana == null || currentBanana.owner == null) && bananaId < BananaCount - 1)
			{
				bananaId++;
				currentBanana = bananas [bananaId];
			}
			
			if(currentBanana == null)
				continue;

			//currentBanana.gameObject.transform.position = targetPos;
			//currentBanana.gameObject.transform.rotation = targetRot;
			currentBanana.gameObject.transform.position = Vector3.Lerp(currentBanana.gameObject.transform.position, targetPos, 0.25f);
			currentBanana.gameObject.transform.rotation = Quaternion.Lerp(currentBanana.gameObject.transform.rotation, targetRot, 0.25f);


			target = currentBanana.transform;

			updated = true;
		}

		return updated;
	}
}
