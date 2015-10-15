using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoresDisplay : MonoSingleton<HighScoresDisplay> {

    Text[] name = new Text[10];
    Text[] totalTime = new Text[10];
    Text[] lapTimes = new Text[10];

    Image[] panels = new Image[10];


	void Awake () {

        Image[] unorderedPanels = GetComponentsInChildren<Image>();    // There should be 10.

        for (int ite = 0; ite < 10; ++ite)
        {
            int rank = int.Parse(unorderedPanels[ite].gameObject.name) - 1;  // Actually not rank but rank - 1.

            panels[rank] = unorderedPanels[ite];

            Text[] texts = unorderedPanels[rank].gameObject.GetComponentsInChildren<Text>();     // There should be 3.

            for (int ite2 = 0; ite2 < 3; ++ite2)
            {
                if (texts[ite2].gameObject.name == "RankName")
                    name[rank] = texts[ite2];

                else if (texts[ite2].gameObject.name == "Total")
                    totalTime[rank] = texts[ite2];

                else if (texts[ite2].gameObject.name == "Laps")
                    lapTimes[rank] = texts[ite2];
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnEnable()
    {
        LoadLeaderboard(0);
    }

    public void LoadLeaderboard(int level)
    {
        int recordsCount = RecordKeeper.Instance.HighScores[level].Count;

        for (int ite = 0; ite < 10; ++ite)
        {
            if (ite < recordsCount)
                panels[ite].gameObject.SetActive(true);

            else
                panels[ite].gameObject.SetActive(false);
        }

        for (int rank = 0; rank < recordsCount; ++rank)   // Not actually rank but rank -1;
        {
            Record beingWrit = RecordKeeper.Instance.HighScores[level][rank];
            name[rank].text = (rank + 1).ToString() + "- " + beingWrit.Holder;

            int[] digits;
            ClockDisplay.TimeToDigits(beingWrit.TotalTime, out digits);
            totalTime[rank].text = "Total:\n" + ClockDisplay.DigitsToString(digits);

            lapTimes[rank].text = string.Empty;

            for (int lap = 0; lap < beingWrit.LapTimes.Count; ++lap)
            {
                if (lap != 0)
                    lapTimes[rank].text += "\n";

                ClockDisplay.TimeToDigits(beingWrit.LapTimes[lap], out digits);
                lapTimes[rank].text += "Lap" + (lap + 1).ToString() + ": " + ClockDisplay.DigitsToString(digits);
            }
        }
    }
}