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

    public string RecordToString()
    {
        string str = string.Empty;

        str += Holder + "\n";

        for (int j = 0; j < LapTimes.Count; ++j)
            str += LapTimes[j].ToString() + "\n";

        return str;
    }

    static public Record RecordFromString(string str)
    {
        Record newRecord = new Record();

        string[] subStr = str.Split(new char[]{'\n'});

        //char[] preSubStr;
        //string subStr;

        //int EOLindex = str.IndexOf('\n');

        //preSubStr = new char[EOLindex];
        //str.CopyTo(0, preSubStr, 0, EOLindex);
        //subStr = new string(preSubStr);
        //str.Remove(0, EOLindex+1);

        newRecord.SetHolderName(subStr[0]);

        //preSubStr = new char[EOLindex];
        //str.CopyTo(0, preSubStr, 0, EOLindex);
        //subStr = new string(preSubStr);
        //str.Remove(0, EOLindex + 1);

        for (int ite = 1; ite < subStr.Length - 1; ++ite)
        {
            newRecord.AddLapTime(float.Parse(subStr[ite]));
        }

        return newRecord;
    }



}
