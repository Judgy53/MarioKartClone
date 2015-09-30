using UnityEngine;
using System.Collections;

public class ItemBox : MonoBehaviour {

	[SerializeField]
	private float RotSpeed = 20f;

	[SerializeField]
	private float movingMaxHeight = 1.5f;
	[SerializeField]
	private float movingMinHeight = 0f;
	[SerializeField]
	private float movingSpeed = 0.5f;

	private float origHeight;

	private bool movingUp = true;

	private float ColorCursor = 0f;
	private float ColorDir = 1f;

	private ItemSpawner spawner = null;

	void Start () {
		origHeight = transform.position.y;
		ColorCursor = Random.Range (0, 128);
		ColorDir = Random.Range (0, 2);

		movingUp = Random.Range (0, 2) == 1 ? true : false;

		transform.Rotate(Random.value * 45f, Random.value * 360.0f, Random.value * 45f);
		transform.Translate(0f, Random.Range (movingMinHeight, movingMaxHeight), 0f);
	}

	void Update () {
		if(Mathf.Abs(transform.position.x) < 1f && Mathf.Abs(transform.position.x) < 1f)
		{
			Debug.Log ("I'm Wrong !?!?!?!?");
			Debug.Log ("My Spawner : " + spawner.ToString());
			Debug.Log ("X: " + spawner.transform.position.x);
			Debug.Log ("Z: " + spawner.transform.position.z);
			Debug.Break();
		}

		transform.Rotate (0f, RotSpeed * Time.deltaTime, 0f);

		Vector3 pos = transform.position;
		if (movingUp) 
			pos.y = Mathf.MoveTowards(pos.y, origHeight + movingMaxHeight, movingSpeed);
		else
			pos.y = Mathf.MoveTowards(pos.y, origHeight + movingMinHeight, movingSpeed);

		transform.position = pos;

		if (Mathf.Approximately(pos.y, origHeight + movingMaxHeight) || Mathf.Approximately(pos.y, origHeight + movingMinHeight)) 
		{
			movingUp = !movingUp;
		}

		Color c = GetComponent<Renderer> ().material.color;
		c.b = (128f + ColorCursor) / 256f;
		c.g = (256f - ColorCursor) / 256f;

		ColorCursor += ColorDir;

		if (ColorCursor >= 128f || ColorCursor <= 0f) 
			ColorDir *= -1f;

		GetComponent<Renderer> ().material.color = c;
	}

	void SetSpawner(ItemSpawner _spawner)
	{
		spawner = _spawner;
	}

	void Explode()
	{
		//GameObject prefabExplode = Resources.Load ("Prefabs/ItemBoxExplode") as GameObject;

		//ItemBoxExplode box = Instantiate (prefabExplode, transform.position, transform.rotation) as ItemBoxExplode;

		//box.Setup (this);
	}
}
