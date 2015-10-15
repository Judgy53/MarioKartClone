using UnityEngine;
using System.Collections;

public class ItemRedShell : ItemGreenShell {

	public ItemRedShell()
	{
		prefab = Resources.Load ("prefabs/RedShell") as GameObject;
	}
	
	public override Item StopUse(CarItemHandler car, bool useBehind)
	{
		base.StopUse (car, useBehind);

		Shell.GetComponent<RedShell>().FindTarget (car, useBehind);
		
		return null;
	}
}
