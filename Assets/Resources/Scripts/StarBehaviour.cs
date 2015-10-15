using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class StarBehaviour : MonoBehaviour {

    Renderer rendy = null;

	public CarItemHandler car;
	public Rigidbody body;

	private Color baseColor;
	private CarController controller;

	private BoxCollider starCollider;

	// Use this for initialization
	void Start () {
        rendy = transform.Find("SkyCar/SkyCarBody").GetComponent<Renderer>();
		baseColor = rendy.material.color;
		controller = car.GetComponent<UnityStandardAssets.Vehicles.Car.CarController> ();

		controller.MultiplyAcceleration (2.5f);

		body = car.GetComponent<Rigidbody> ();

		starCollider = car.gameObject.AddComponent<BoxCollider> ();
		starCollider.isTrigger = true;
		starCollider.size = new Vector3 (4f, 2f, 15f);

		//last 10 seconds
		Destroy (this, 10f);
	}
	
	// Update is called once per frame
	void Update () {

        Color randColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        rendy.material.SetColor("_Color", randColor);
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
		rendy.material.SetColor ("_Color", baseColor);
		controller.MultiplyAcceleration (0.4f);

		Destroy (starCollider);
    }
}
