using UnityEngine;
using System.Collections;

public class ItemStar : Item {

	public override Item StartUse(CarItemHandler car, bool useBehind)
	{
		StarBehaviour star = car.gameObject.AddComponent<StarBehaviour> ();

		star.car = car;

		return null;
	}

	public override Item StopUse(CarItemHandler car, bool useBehind)
	{
		return null;
	}

	public override SpriteIndex GetSpriteIndex()
	{
		return SpriteIndex.Star;
	}
}
