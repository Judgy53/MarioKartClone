using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(CarController))]
public class CarItemHandler : MonoBehaviour {

	protected Item currentItem = null;

	private CarController carCtrl = null;
	public CarController Controller { get { return carCtrl; } }

	public bool OnPickItemBox(Item item)
	{
		if (currentItem != null)
			return false;
		currentItem = item;

		carCtrl = GetComponent<CarController> ();

		return true;
	}

	protected void useItem()
	{
		if (currentItem != null)
			currentItem = currentItem.use (this);
	}
}
