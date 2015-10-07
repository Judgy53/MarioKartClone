using UnityEngine;
using System.Collections;
using System;

public class ItemTripleGreenShell : ItemGreenShell, IItemUpdatable {

	private bool Summoned = false;

	private const int MAXSHELLS = 3;
	private GreenShell[] Shells = new GreenShell[MAXSHELLS];

	private int NbLaunchedShells = 0;

	private const float DistFromCar = 5f;
	private float AngleBetweenShells = 360f / (float)MAXSHELLS;

	private const float RotationSpeed = 1f;

	protected GameObject Prefab;
	private float ShellHeight;

	private float rotationTicks = 0f;

	public ItemTripleGreenShell()
	{
		Prefab = Resources.Load ("Prefabs/GreenShell") as GameObject;
		
		//get collider height by creating a temp shell
		GameObject temp = GameObject.Instantiate (Prefab) as GameObject;
		ShellHeight = temp.GetComponent<Collider> ().bounds.extents.y;
		GameObject.Destroy (temp);
	}

	public override Item use(CarItemHandler car, bool useBehind)
	{
		if (!Summoned) 
		{
			SummonShells(car);
			return this;
		}

		LaunchShell (car, useBehind);

		if (NbLaunchedShells < MAXSHELLS)
			return this;

		return null;
	}

	private void SummonShells(CarItemHandler car)
	{
		for (int i = 0; i < MAXSHELLS; i++) 
		{
			Vector3 aheadPos = car.transform.position + car.transform.forward * DistFromCar;

			GameObject gao = GameObject.Instantiate(Prefab, aheadPos, Quaternion.identity) as GameObject;

			gao.transform.RotateAround(car.transform.position, Vector3.up, (float)i * AngleBetweenShells);
			gao.transform.LookAt(car.transform.position);

			gao.transform.Translate(0f, ShellHeight, 0f);

			GreenShell shell = gao.GetComponent<GreenShell>();

			gao.GetComponent<Rigidbody>().isKinematic = true;

			shell.Updatable = false;

			Shells[i] = shell;
		}

		Summoned = true;
	}

	private void LaunchShell(CarItemHandler car, bool useBehind)
	{
		base.use (car, useBehind);

		float minShellDist = -1;
		int shellToRemove = -1;

		for (int i = 0; i < MAXSHELLS; i++) 
		{
			GreenShell shell = Shells[i];

			if(shell == null || shell.Updatable)
				continue;

			Vector3 target = car.transform.position + car.transform.forward * DistFromCar * (useBehind ? -1 : 1);

			float shellDist = Vector3.Distance(shell.transform.position, target);

			if(shellToRemove == -1 || shellDist < minShellDist)
			{
				minShellDist = shellDist;
				shellToRemove = i;
			}
		}

		GameObject.Destroy (Shells [shellToRemove].gameObject);
		Shells [shellToRemove] = null;

		NbLaunchedShells++;
	}

	public bool Update(CarItemHandler car)
	{
		if (!Summoned)
			return true;

		rotationTicks = (rotationTicks + RotationSpeed) % 360f;

		Vector3 target = car.transform.position + new Vector3 (0f, ShellHeight, 0f);

		bool updated = false;

		for (int i = 0; i < MAXSHELLS; i++) 
		{
			GreenShell shell = Shells[i];

			if(shell == null || shell.Updatable)
				continue;

			updated = true;

			shell.transform.position = target + car.transform.forward * DistFromCar;
			shell.transform.RotateAround(target, car.transform.up, rotationTicks + (float)i * AngleBetweenShells);
			shell.transform.LookAt(target);
		}

		return updated;
	}
}
