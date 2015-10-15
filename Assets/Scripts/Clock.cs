using UnityEngine;
using System.Collections;

public class Clock {

    private float startTime;

    private bool stopped = true;

    private float timeBeforeStopped = 0f;

    public float LocalTime { get { return stopped ? timeBeforeStopped : timeBeforeStopped + Time.time - startTime; } }

	public void Start()
    {
        startTime = Time.time;
        stopped = false;
    }

    public void Stop()
    {
        timeBeforeStopped = LocalTime;
        stopped = true;
    }

    public void Reset()
    {
        startTime = Time.time;
        timeBeforeStopped = 0f;
    }
}
