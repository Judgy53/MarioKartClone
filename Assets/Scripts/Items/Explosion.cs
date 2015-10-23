using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	[SerializeField]
	private float ExplosionRadiusRatio = 2.0f;

	[SerializeField]
	private float TimeToExplode = 0.10f;

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
		for (int i = 0; i < 100 * TimeToExplode; i++) 
		{
			transform.localScale += new Vector3(ExplosionRadiusRatio, ExplosionRadiusRatio, ExplosionRadiusRatio);
			yield return new WaitForSeconds (0.01f);
		}
		
		Destroy (gameObject);
	}
}
