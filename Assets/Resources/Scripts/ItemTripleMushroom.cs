using UnityEngine;
using System.Collections;

public class ItemTripleMushroom : ItemMushroom {

	private int UseLeft = 3;

	public override Item use(CarItemHandler car, bool useBehind)
	{
		base.use (car, useBehind);
		UseLeft--;

		if (UseLeft > 0)
			return this;

		return null;
	}
}
