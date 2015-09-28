using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class CarWaypointHandler : MonoBehaviour {

    [SerializeField]
    private Transform LastWayPoint = null;

    private CarController carController = null;

    private bool isCheckingForBlocked = false;

    // Use this for initialization
    void Start()
    {
        carController = GetComponent<CarController>();
    }

    void Update()
    {
        if (carController.CurrentSpeed < 5 && !isCheckingForBlocked)
        {
            StartCoroutine("CheckForBlocked");
        }
    }

    private void SetLastWayPoint(Transform WayPoint)
    {
        LastWayPoint = WayPoint;
    }

    public void TeleportToLastWayPoint()
    {
        transform.position = LastWayPoint.position;
        transform.rotation = LastWayPoint.rotation;
    }


    private IEnumerator CheckForBlocked()
    {
        isCheckingForBlocked = true;

        for (int ite = 0; ite < 30 && carController.CurrentSpeed < 5; ++ite)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (carController.CurrentSpeed < 5f)
            TeleportToLastWayPoint();

        isCheckingForBlocked = false;
    }
}
