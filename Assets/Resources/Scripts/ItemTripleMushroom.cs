using UnityEngine;
using System.Collections;

public class ItemTripleMushroom : ItemMushroom {

	private int UseLeft = 3;

	public override Item StartUse(CarItemHandler car, bool useBehind)
	{
		base.StartUse (car, useBehind);
		UseLeft--;

		if (UseLeft > 0)
			return this;

		return null;
	}

	public override Item StopUse(CarItemHandler car, bool useBehind)
	{
		return this;
	}
}
