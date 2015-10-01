using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

    private float startTime;

    private bool hasBeenStarted = false;

    public float LocalTime { get { return hasBeenStarted ? Time.time - startTime : 0f; } }

    public bool HasBeenStarted { get { return hasBeenStarted; } } 

	public void StartClock()
    {
        startTime = Time.time;
        hasBeenStarted = true;
    }
}
