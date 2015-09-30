using UnityEngine;
using System.Collections;

public class Player : CarItemHandler {

	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			//OnHit ();
			useItem(Input.GetKey(KeyCode.DownArrow));
		}
	}
}
