using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Record {

    private string holder;
    private float totalTime = 0;
    private List<float> lapTimes = new List<float>();

    public string Holder { get { return holder; } }
    public float TotalTime { get { return totalTime; } }
    public List<float> LapTimes { get { return lapTimes; } }

    public void SetHolderName(string name)
    {
        holder = name;
    }

    public void AddLapTime(float lapTime)
    {
        lapTimes.Add(lapTime);
        totalTime += lapTime;
    }

}
