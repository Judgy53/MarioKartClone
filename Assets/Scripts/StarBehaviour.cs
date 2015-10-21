using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Car;

public class StarBehaviour : MonoBehaviour {
	
	private Dictionary<GameObject, Material[]> MeshBaseColor = new Dictionary<GameObject, Material[]>();

	public CarItemHandler car;
	
	private CarController controller;

	private BoxCollider starCollider;

	[SerializeField]
	private float speedMultiplier = 2.5f;

	// Use this for initialization
	void Start () {

		List<GameObject> meshes = FindMeshes (transform);

		foreach (GameObject gao in meshes) 
		{
			if(gao == null)
				return;

			Renderer rend = gao.GetComponent<Renderer>();
			
			if(rend != null)
				MeshBaseColor.Add (gao, rend.materials);
		}

		controller = car.GetComponent<UnityStandardAssets.Vehicles.Car.CarController> ();
		controller.MultiplyAcceleration (speedMultiplier);

		starCollider = car.gameObject.AddComponent<BoxCollider> ();
		starCollider.isTrigger = true;
		starCollider.size = new Vector3 (4f, 2f, 15f);

		//last 10 seconds
		Destroy (this, 10f);
	}

	private List<GameObject> FindMeshes(Transform parent, List<GameObject> meshList = null)
	{
		if (meshList == null) 
			meshList = new List<GameObject> ();

		foreach (Transform child in parent) 
		{
			if(child.CompareTag("Colorable"))
				meshList.Add (child.gameObject);

			FindMeshes(child, meshList);
		}

		return meshList;
	}
	
	// Update is called once per frame
	void Update () {

        Color randColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

		foreach (KeyValuePair<GameObject, Material[]> kvp in MeshBaseColor) 
		{
			GameObject gao = kvp.Key;

			if(gao == null) 
				return;

			Renderer rend = gao.GetComponent<Renderer>();

			if(rend != null)
			{
				Material[] mats = new Material[rend.materials.Length];

				for(int i = 0; i < mats.Length; i++)
				{
					Material mat = new Material(rend.materials[i]);
					mat.SetColor ("_Color", randColor);

					mats[i] = mat;
				}

				rend.materials = mats;
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		IItemCollision col = collider.GetComponent<IItemCollision> ();

		if (col != null && col != car) 
		{
			col.OnHit(car.gameObject);
		}
	}

    void OnDestroy()
    {
		foreach (KeyValuePair<GameObject, Material[]> kvp in MeshBaseColor) 
		{
			GameObject gao = kvp.Key;
			
			if(gao == null) 
				return;
			
			Renderer rend = gao.GetComponent<Renderer>();
			
			if(rend != null)
			{
				rend.materials = kvp.Value;
			}
		}


		controller.MultiplyAcceleration (1f/speedMultiplier);

		Destroy (starCollider);
    }
}
