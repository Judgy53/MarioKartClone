using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(CarController))]
public class CarItemHandler : MonoBehaviour {

	protected Item currentItem = null;
	public Item CurrentItem { get { return currentItem; } }

	private CarController carCtrl = null;
	public CarController Controller { get { return carCtrl; } }

	//TODO TripleShell Around Player

	public bool OnPickItemBox(Item item)
	{
		if (currentItem != null)
			return false;

		currentItem = item;

		carCtrl = GetComponent<CarController> ();

		return true;
	}

	protected void useItem(bool useBehind)
	{
		if (currentItem != null)
			currentItem = currentItem.use (this, useBehind);
	}
}
