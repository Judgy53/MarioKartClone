﻿using UnityEngine;
using System.Collections;

public abstract class Item {

	public enum ItemType
	{
		None = 0,
		Mushroom,
		TripleMushroom,
		//GreenShell,
		//TripleGreenShell,
		//RedShell,
		//TripleRedShell,
		//BobOmb,
		//Banana,
		//TripleBanana,
		//TrappedCube,
		//FuckYourEyesStar,

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
		default: 
			return null;
		}
	}

	public abstract Item use(CarItemHandler car);
}
