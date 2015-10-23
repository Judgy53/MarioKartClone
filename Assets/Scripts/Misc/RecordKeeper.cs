using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecordKeeper : MonoSingleton<RecordKeeper> {

    [SerializeField]

    private List<Record>[] highScores;  // Array of leaderboards (one leaderboard per level)

    public List<Record>[] HighScores { get { return highScores; } }

    void Awake()
    {
        if (InstanceExists())
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);


        highScores = new List<Record>[GameMgr.Instance.LevelNames.Length];

        for (int lvl = 0; lvl < GameMgr.Instance.LevelNames.Length; ++lvl)
        {
            highScores[lvl] = LeaderboardFromString(FileTranslator.ReadFile(GameMgr.Instance.LevelNames[lvl] + ".sav"));
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SubmitHighScore(Record newRecord)
    {
        int currLvl = GameMgr.Instance.CurrentLevel;

        highScores[currLvl].Add(newRecord);
        highScores[currLvl].Sort(new RecordComparer());

        if (highScores[currLvl].Count > 10)
        {
            highScores[currLvl].RemoveAt(10);
        }

        FileTranslator.WriteFile(GameMgr.Instance.LevelNames[currLvl] + ".sav", LeaderboardToString());
    }

    private class RecordComparer : IComparer<Record>
    {
        public int Compare(Record rec1, Record rec2)
        {
            return rec1.TotalTime.CompareTo(rec2.TotalTime);
        }
    }

    public string LeaderboardToString()
    {
        int currLvl = GameMgr.Instance.CurrentLevel;

        string result = string.Empty;

        for (int i = 0; i < highScores[currLvl].Count; ++i)
        {
            result += highScores[currLvl][i].RecordToString();

            result += "#";
        }

        return result;
    }

    public List<Record> LeaderboardFromString(string str)
    {
        List<Record> leaderboard = new List<Record>();

        string[] subStr = str.Split(new char[]{'#'});

        for (int ite = 0; ite < subStr.Length - 1; ++ite)
        {
            leaderboard.Add(Record.RecordFromString(subStr[ite]));
        }

        return leaderboard;
    }
}
