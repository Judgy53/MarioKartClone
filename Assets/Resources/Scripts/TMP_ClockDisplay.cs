using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TMP_ClockDisplay : MonoBehaviour {

    private Text text = null;
    private CarWaypointHandler carWpHandler = null;

    private bool endOfLapAnimRun = false;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        carWpHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<CarWaypointHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!endOfLapAnimRun)
        {
            int[] digits;

            TimeToDigits(LevelMgr.Instance.raceClock.LocalTime, out digits);

            SetDigitsToDisplay(digits);
        }
    }

    public void EndOfLap()
    {
        StartCoroutine("EndOfLapCoroutine");
    }

    public IEnumerator EndOfLapCoroutine()
    {
        endOfLapAnimRun = true;

        float lapTime = LevelMgr.Instance.raceClock.LocalTime - carWpHandler.TimeAtLastLap;

        int[] digits;

        TimeToDigits(lapTime, out digits);

        for (int ite = 0; ite < 10; ++ite)
        {
            SetDigitsToDisplay(digits);

            yield return new WaitForSeconds(0.1f);

            text.text = string.Empty;

            yield return new WaitForSeconds(0.1f);
        }

        endOfLapAnimRun = false;
    }

    private void TimeToDigits(float time, out int[] digits)
    {
        int timeAsMs = (int)(time*1000f);

        digits = new int[7];

        for (int ite = 6; ite >= 0; --ite)
        {
            if (ite == 2)
            {
                digits[ite] = timeAsMs % 6;
                timeAsMs = (timeAsMs - digits[ite]) / 6;
        }
            else
            {
                digits[ite] = timeAsMs % 10;
                timeAsMs = (timeAsMs - digits[ite]) / 10;
            }
        }
    }

    private void SetDigitsToDisplay(int[] digits) // There should be 7 digits, no less;
    {
        if (digits.Length != 7)
        {
            Debug.Log("The array passed to SetDigitsToDisplay is " + digits.Length.ToString() + ", not 7!");
        }

        text.text = string.Empty;

        int ite;

        for (ite = 0; ite < 2; ++ite)
            text.text += digits[ite].ToString();

        text.text += "'";

        for (ite = 2; ite < 4; ++ite)
            text.text += digits[ite].ToString();

        text.text += "\"";

        for (ite = 4; ite < 7; ++ite)
            text.text += digits[ite].ToString();
    }
}