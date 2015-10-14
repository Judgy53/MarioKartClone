using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecordKeeper : MonoSingleton<RecordKeeper> {

    private List<Record> highScores = new List<Record>();

    public List<Record> HighScores { get { return highScores; } }

    void awake()
    {
        if (InstanceExists())
            Destroy(gameObject, 0f);
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SubmitHighScore(Record newRecord)
    {
        highScores.Add(newRecord);
        highScores.Sort(new RecordComparer());

        if (highScores.Count > 10)
        {
            highScores.RemoveAt(10);
        }

        FileTranslator.WriteShit("sav.sav", HighScoresToString());
    }

    private class RecordComparer : IComparer<Record>
    {
        public int Compare(Record rec1, Record rec2)
        {
            return rec1.TotalTime.CompareTo(rec2.TotalTime);
        }
    }

    public string HighScoresToString()
    {
        string result = string.Empty;

        for (int i = 0; i < highScores.Count; ++i)
        {
            result += (i+1).ToString() + "\n";
            result += highScores[i].Holder + "\n";
            result += highScores[i].TotalTime.ToString() + "\n";
            
            for (int j = 0; j < highScores[i].LapTimes.Count; ++j)
                result += highScores[i].LapTimes[j] + "\n";

            result += "-----\n-----\n";
        }

        return result;
    }
}
