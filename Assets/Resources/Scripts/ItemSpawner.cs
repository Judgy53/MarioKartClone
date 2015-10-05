using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	[SerializeField]
	private float ItemSpawnTimer = 5f;
	private float LastItemSpawn = 5f;

	private GameObject boxSpawned = null;

	private GameObject ItemBoxPrefab = null;

	// Use this for initialization
	void Start () {
		LastItemSpawn = ItemSpawnTimer;

		ItemBoxPrefab = Resources.Load ("Prefabs/ItemBox") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (boxSpawned == null)
		{
			LastItemSpawn += Mathf.Min (Time.deltaTime, ItemSpawnTimer - LastItemSpawn);

			if (LastItemSpawn >= ItemSpawnTimer) {
				Vector3 boxPos = transform.position;

				boxSpawned = Instantiate (ItemBoxPrefab) as GameObject;
				boxSpawned.transform.position = boxPos;
				boxSpawned.transform.rotation = Quaternion.identity;

				LastItemSpawn = 0f;
			}
		}
	}

	void OnTriggerStay(Collider collider)
	{
		CarItemHandler picker = collider.GetComponent<CarItemHandler> ();
		
		if (picker != null && boxSpawned != null) 
		{
			picker.OnPickItemBox(Item.RandomItem());

			boxSpawned.SendMessage("Explode");
			
			Destroy (boxSpawned);
			boxSpawned = null;
		}
	}
}
