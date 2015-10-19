using UnityEngine;
using System.Collections;

public abstract class Item {

	public enum ItemType
	{
		None = 0,

		Mushroom,
		TripleMushroom,
		GreenShell,
		TripleGreenShell,
		RedShell,
		TripleRedShell,
		BobOmb,
		Banana,
		TripleBanana,
		TrappedCube,
		Star,

		Count
	}

	static public Item RandomItem()
	{
		int typeId = Random.Range((int)ItemType.None + 1, (int)ItemType.Count);

		switch ((ItemType)typeId) 
		{
		case ItemType.Mushroom:
			return new ItemMushroom();
		case ItemType.TripleMushroom:
			return new ItemTripleMushroom();
		case ItemType.GreenShell:
			return new ItemGreenShell();
		case ItemType.TripleGreenShell:
			return new ItemTripleGreenShell();
		case ItemType.RedShell:
			return new ItemRedShell();
		case ItemType.TripleRedShell:
			return new ItemTripleRedShell();
		case ItemType.BobOmb:
			return new ItemBobOmb();
		case ItemType.Banana:
			return new ItemBanana();
		case ItemType.TripleBanana:
			return new ItemTripleBanana();
		case ItemType.TrappedCube:
			return new ItemTrappedCube();
		case ItemType.Star:
			return new ItemStar();
		default: 
			Debug.Log ("RandomItem null (id:" + typeId + ")");
			return null;
		}
	}

	public abstract Item StartUse(CarItemHandler car, bool useBehind);
	public abstract Item StopUse(CarItemHandler car, bool useBehind);

	public abstract SpriteIndex GetSpriteIndex();
}
