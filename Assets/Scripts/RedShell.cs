using UnityEngine;
using System.Collections;
using System.Linq;

public class RedShell : GreenShell {

	[HideInInspector]
	public CarWaypointHandler TargetCar = null;

	[HideInInspector]
	public Waypoint LastWaypoint = null;

	protected override void FixedUpdate () {

		if (!Updatable)
			return;

		if (TargetCar == null)  // act as GreenShell
		{
			base.FixedUpdate();
			return;
		}

		Vector3 target;

		if (TargetCar.LastWp == LastWaypoint)
			target = TargetCar.transform.position;
		else
			target = LastWaypoint.NextWp.Floor;


		transform.LookAt (target);

		Vector3 start = transform.position;
		
		RaycastHit[] hits = Physics.RaycastAll(start, -transform.up, Mathf.Infinity).OrderBy(h=>h.distance).ToArray();
		
		if(hits.Length > 0)
		{
			foreach(RaycastHit hit in hits)
			{
				if(hit.collider.isTrigger)
					continue;

				Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.normal);

				transform.rotation = new Quaternion(rot.x, transform.rotation.y, rot.z, transform.rotation.w);

				break;
			}
		}

		Vector3 vel = transform.forward;
		vel *= Speed/50f; // arbritrary value to have same speed as greenshell

		transform.position += vel;

		LastPosition = transform.position;
	}

	public void SetLastWaypoint(Waypoint wp)
	{
		LastWaypoint = wp;
	}

	public void FindTarget(CarItemHandler car, bool useBehind)
	{
		CarWaypointHandler carWaypoint = car.GetComponent<CarWaypointHandler> ();
		
		if (carWaypoint == null) 
		{
			Debug.LogError ("Red Shell launched by a non Waypoint based car");
			return;
		}
		
		if (carWaypoint.rank > 1 && !useBehind) 
		{
			TargetCar = Ranker.Instance.AtRank (carWaypoint.rank - 1);
			LastWaypoint = carWaypoint.LastWp;
		}
	}


	private bool OnGround(int xOffset = 0)
	{
		Vector3 offset = new Vector3(xOffset, 0f, 0f);
		return Physics.Raycast (transform.position + offset, -transform.up, 2.7f);
	}
}
