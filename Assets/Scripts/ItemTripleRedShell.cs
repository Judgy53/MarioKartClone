using UnityEngine;
using System.Collections;

public class ItemTripleRedShell : ItemTripleGreenShell {

	public ItemTripleRedShell() : base()
	{
		Prefab = Resources.Load ("Prefabs/RedShell") as GameObject;
	}

	protected override void LaunchShell(CarItemHandler car, bool useBehind)
	{
		base.LaunchShell (car, useBehind);

		Shell.GetComponent<RedShell>().FindTarget (car, useBehind);
	}

	protected override GreenShell GetShellComponent(GameObject gao)
	{
		return gao.GetComponent<RedShell> ();
	}
}
