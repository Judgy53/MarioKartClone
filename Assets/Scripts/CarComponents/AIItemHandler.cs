using UnityEngine;
using System.Collections;

public class AIItemHandler : CarItemHandler {

	void Update()
	{
		if (pickedItem == null && CurrentItem != null) //No update when random display
			TryUseItem ();
		else if (pickedItem != null) // Random Displaying; Act as random Timer here
		{
			bool StopRandom = Random.Range (0, 10) == 0; // 10% chance to stop timer
			if(StopRandom)
				RandomDisplaying = false;
		}
	}
	
	void TryUseItem()
	{
		if (CurrentItem is ItemTripleBanana) // Triple First (extends Base)
			TryUseTripleBanana ();
		else if (CurrentItem is ItemTripleGreenShell)
			TryUseTripleGreenShell ();
		else if (CurrentItem is ItemTripleMushroom)
			TryUseTripleMushroom ();
		else if (CurrentItem is ItemTripleRedShell)
			TryUseTripleRedshell ();
		else if (CurrentItem is ItemBanana)
			TryUseBanana ();
		else if (CurrentItem is ItemBobOmb)
			TryUseBobOmb ();
		else if (CurrentItem is ItemRedShell) // Red Shell first (extends GreenShell)
			TryUseRedshell ();
		else if (CurrentItem is ItemGreenShell)
			TryUseGreenShell ();
		else if (CurrentItem is ItemMushroom)
			TryUseMushroom ();
		else if (CurrentItem is ItemStar)
			TryUseStar ();
		else if (CurrentItem is ItemTrappedCube)
			TryUseTrappedCube ();
		else
			Debug.Log ("Bot is trying to use " + CurrentItem.GetType().ToString() + " but no behaviour has been found :/");
	}

	void TryUseBanana()
	{
		ItemBanana item = CurrentItem as ItemBanana;

		if (item.Summoned == false)
			StartUseItem (false);
		else 
		{
			bool use = Random.Range (0, 100) < 1; // 1% chance to launch each frame

			if(use)
			{
				bool dir = Random.Range(0, 2) == 1; // 50% chance behind
				StopUseItem (dir);
			}
		}
	}

	void TryUseBobOmb()
	{
		ItemBobOmb item = CurrentItem as ItemBobOmb;

		if (item.Summoned == false)
			StartUseItem (false);
		else 
		{
			bool use = Random.Range (0, 100) < 1; // 1% chance to launch each frame
			
			if(use)
			{
				bool dir = Random.Range(0, 2) == 1; // 50% chance behind
				StopUseItem (dir);
			}
		}
	}

	void TryUseGreenShell()
	{
		ItemGreenShell item = CurrentItem as ItemGreenShell;

		if (item.Summoned == false)
			StartUseItem (false);
		else 
		{
			bool use = Random.Range (0, 100) < 1; // 1% chance to launch each frame
			
			if(use)
			{
				bool dir = Random.Range(0, 2) == 1; // 50% chance behind
				StopUseItem (dir);
			}
		}
	}

	void TryUseMushroom()
	{
		StartUseItem (false);
		StopUseItem (false);
	}

	void TryUseRedshell()
	{
		ItemRedShell item = CurrentItem as ItemRedShell;

		if (item.Summoned == false)
			StartUseItem (false);
		else 
		{
			bool use = Random.Range (0, 100) < 1; // 1% chance to launch each frame
			
			if(use)
			{
				bool dir = GetComponent<CarWaypointHandler>().rank == 1; // launch behind if first
				StopUseItem (dir);
			}
		}
	}

	void TryUseStar()
	{
		StartUseItem (false);
		StopUseItem (false);
	}

	void TryUseTrappedCube()
	{
		ItemTrappedCube item = CurrentItem as ItemTrappedCube;

		if (item.Summoned == false)
			StartUseItem (false);
		else 
		{
			bool use = Random.Range (0, 100) < 1; // 1% chance to launch each frame
			
			if(use)
			{
				bool dir = Random.Range(0, 2) == 1; // 50% chance behind
				StopUseItem (dir);
			}
		}
	}

	void TryUseTripleBanana()
	{
		ItemTripleBanana item = CurrentItem as ItemTripleBanana;
		
		if (item.Summoned == false) 
		{
			StartUseItem (false);
			StopUseItem (false);
		}
		else 
		{
			bool use = Random.Range (0, 100) < 5; // 5% chance to launch each frame
			
			if(use)
			{
				bool dir = Random.Range(0, 2) == 1; // 50% chance behind
				StartUseItem (dir);
				StopUseItem (dir);
			}
		}
	}

	void TryUseTripleGreenShell()
	{
		ItemTripleGreenShell item = CurrentItem as ItemTripleGreenShell;

		if (item.Summoned == false) 
		{
			StartUseItem (false);
			StopUseItem (false);
		}
		else 
		{
			bool use = Random.Range (0, 100) < 5; // 5% chance to launch each frame
			
			if(use)
			{
				bool dir = Random.Range(0, 2) == 1; // 50% chance behind
				StartUseItem (dir);
				StopUseItem (dir);
			}
		}
	}

	void TryUseTripleMushroom()
	{
		ItemTripleMushroom item = CurrentItem as ItemTripleMushroom;

		bool use = Random.Range (0, 1000) < 5 * (Mathf.Pow(item.UseLeft, 3)); // 
		
		if(use)
		{
			StartUseItem (false);
			StopUseItem (false);
		}
	}
	
	void TryUseTripleRedshell()
	{
		ItemTripleRedShell item = CurrentItem as ItemTripleRedShell;

		if (item.Summoned == false) 
		{
			StartUseItem (false);
			StopUseItem (false);
		}
		else 
		{
			bool use = Random.Range (0, 100) < 5; // 5% chance to launch each frame
			
			if(use)
			{
				bool dir = GetComponent<CarWaypointHandler>().rank == 1;
				StartUseItem (dir);
				StopUseItem (dir);
			}
		}
	}
}
