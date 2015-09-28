using UnityEngine;
using System.Collections;

public class Player : CarItemHandler {


	void Start () {
	
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			useItem();
		}
	}
}
