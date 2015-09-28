using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	[SerializeField]
	private float ItemSpawnTimer = 5f;
	private float LastItemSpawn = 5f;

	[SerializeField]
	private GameObject ItemBoxPrefab = null;

	private bool boxSpawned = false;

	// Use this for initialization
	void Start () {
		LastItemSpawn = ItemSpawnTimer;

		if (ItemBoxPrefab == null) 
		{
			Debug.LogError("Please bind the ItemBox Prefab");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (boxSpawned == false)
		{
			LastItemSpawn += Mathf.Min (Time.deltaTime, ItemSpawnTimer - LastItemSpawn);

			if (LastItemSpawn >= ItemSpawnTimer) {
				Vector3 boxPos = transform.position;
				boxPos.y += 1.5f;

				GameObject go = Instantiate (ItemBoxPrefab, boxPos, ItemBoxPrefab.transform.rotation) as GameObject;
				go.SendMessage("SetSpawner", this);

				boxSpawned = true;
				LastItemSpawn = 0f;
			}
		}
	}

	void OnBoxDestroy()
	{
		boxSpawned = false;
	}
}
