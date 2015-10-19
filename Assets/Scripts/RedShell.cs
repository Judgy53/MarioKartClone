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
			body.velocity = transform.forward;
	}

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

		Vector3 vel = new Vector3 (transform.forward.x, 0f, transform.forward.z);

		transform.position += vel;
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
