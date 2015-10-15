using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	void Start()
	{
		StartCoroutine ("Boom");
	}

	void OnTriggerEnter(Collider collider)
	{
		IItemCollision col = collider.GetComponent<IItemCollision> ();

		if (col != null)
			col.OnHit (gameObject);
	}

	IEnumerator Boom()
	{
		for (int i = 0; i < 25; i++) 
		{
			transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
			yield return new WaitForSeconds (0.01f);
		}
		
		Destroy (gameObject);
	}
}
