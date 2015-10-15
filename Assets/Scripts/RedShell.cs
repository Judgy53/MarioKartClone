using UnityEngine;
using System.Collections;

public class RedShell : GreenShell {

	[HideInInspector]
	public CarWaypointHandler TargetCar = null;

	[HideInInspector]
	public Waypoint LastWaypoint = null;

	public override void Init()
	{
		if (Updatable)
			body.velocity = new Vector3(transform.forward.x, 0f, transform.forward.z);
	}

	protected override void FixedUpdate () {

		if (!Updatable)
			return;

		if (TargetCar == null) 
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

		body.velocity = new Vector3 (transform.forward.x, -0.1f, transform.forward.z);

		base.FixedUpdate ();
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
			Debug.Log ("Red Shell launched by a non Waypoint based car");
			return;
		}
		
		if (carWaypoint.rank > 1 && !useBehind) 
		{
			TargetCar = Ranker.Instance.AtRank (carWaypoint.rank - 1);
			LastWaypoint = carWaypoint.LastWp;
		}
	}

}
