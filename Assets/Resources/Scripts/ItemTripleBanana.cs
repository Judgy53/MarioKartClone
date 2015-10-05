using UnityEngine;
using System.Collections;

public class ItemTripleBanana : ItemBanana, IItemUpdatable {

	private bool summoned = false;
	private int launched = 0;

	private const int BananaCount = 3;
	private Banana[] bananas = new Banana[BananaCount];

	private float BaseOffset = 5f;
	private float DistBetweenBananas = 3f;

	public override Item use(CarItemHandler car, bool useBehind)
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

	void SummonBananas(CarItemHandler car)
	{
		GameObject prefab = Resources.Load ("Prefabs/Banana") as GameObject;

		for (int i = 0; i < BananaCount; i++) 
		{
			Vector3 position = car.transform.position - (car.transform.forward * BaseOffset + car.transform.forward * DistBetweenBananas * (float)i);
			position += new Vector3(0f, 2f, 0f);

			Quaternion rotation = car.transform.rotation;
			rotation.x = 0f;
			rotation.z = 0f;

			GameObject gao = Object.Instantiate(prefab) as GameObject;

			gao.transform.position = position;
			gao.transform.rotation = rotation;

			Banana banana = gao.GetComponent<Banana>();
			banana.Updatable = false;

			bananas[i] = banana;
		}
	}

	void LaunchBanana(CarItemHandler car, bool useBehind)
	{
		launched++;

		Banana currentBanana = bananas [BananaCount - launched];

		while(currentBanana == null && launched < BananaCount)
		{
			launched++;
			currentBanana = bananas [BananaCount - launched];
		}

		if(currentBanana == null)
			return;

		if (!useBehind)
		{
			Object.Destroy(currentBanana.gameObject);
			base.use (car, useBehind);
		}

		currentBanana.Updatable = true;


		bananas [BananaCount - launched] = null; // set it to null because we don't want to interact with it anymore
	}

	public void Update (CarItemHandler car) {
		Transform target = car.transform;

		for (int i = 0; i < BananaCount; i++) 
		{
			Vector3 targetPos = car.transform.position - (target.forward * BaseOffset + target.forward * DistBetweenBananas * (float)i);
			targetPos += new Vector3(0f, 2f, 0f);
			
			Quaternion targetRot = target.rotation;
			targetRot.x = 0f;
			targetRot.z = 0f;

			int bananaId = i;

			Banana currentBanana = bananas [bananaId];
			
			while(currentBanana == null && bananaId < BananaCount - 1)
			{
				bananaId++;
				currentBanana = bananas [bananaId];
			}
			
			if(currentBanana == null)
				return;

			
			currentBanana.gameObject.transform.position = Vector3.Lerp(currentBanana.gameObject.transform.position, targetPos, 0.25f);
			currentBanana.gameObject.transform.rotation = Quaternion.Lerp(currentBanana.gameObject.transform.rotation, targetRot, 0.25f);


			target = currentBanana.transform;
		}
	}
}
