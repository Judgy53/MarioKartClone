using UnityEngine;
using System.Collections;

public class ItemBoxExplode : ItemBox {

	// Use this for initialization
	protected override void Start () {
		ItemBox[] boxes = GetComponentsInChildren<ItemBox> ();

		foreach (ItemBox box in boxes) 
		{
			box.SetColor(GetColor());
			box.transform.Rotate(Random.value * 90f, Random.value * 360.0f, Random.value * 90f);

			Rigidbody body = box.GetComponent<Rigidbody>();

			if(body != null)
				body.AddForce(box.transform.up * 5f, ForceMode.Impulse);
		}

		Destroy (gameObject, 2f);
	}

	protected override void  Update () { 
		// override the allow empty behaviour 
	}
}
