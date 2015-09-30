using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	[SerializeField]
	private float ItemSpawnTimer = 5f;
	private float LastItemSpawn = 5f;

	[SerializeField]
	private GameObject ItemBoxPrefab = null;

	private GameObject boxSpawned = null;

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
		if (boxSpawned == null)
		{
			LastItemSpawn += Mathf.Min (Time.deltaTime, ItemSpawnTimer - LastItemSpawn);

			if (LastItemSpawn >= ItemSpawnTimer) {
				Vector3 boxPos = transform.position;
				boxPos.y += 1.5f;

				boxSpawned = Instantiate (ItemBoxPrefab) as GameObject;
				boxSpawned.transform.position = boxPos;
				boxSpawned.transform.rotation = Quaternion.identity;

				LastItemSpawn = 0f;
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		CarItemHandler picker = collider.GetComponent<CarItemHandler> ();
		
		if (picker != null && boxSpawned != null) 
		{
			picker.OnPickItemBox(Item.RandomItem());
			
			Destroy (boxSpawned);
			boxSpawned = null;
		}
	}
}
