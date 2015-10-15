using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClockDisplay : MonoBehaviour {

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

            text.text = DigitsToString(digits);
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
            text.text = DigitsToString(digits);

            yield return new WaitForSeconds(0.1f);

            text.text = string.Empty;

            yield return new WaitForSeconds(0.1f);
        }

        endOfLapAnimRun = false;
    }

    public static void TimeToDigits(float time, out int[] digits)
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

    public static string DigitsToString(int[] digits) // There should be 7 digits, no less;
    {
        string str = string.Empty;

        if (digits.Length != 7)
        {
            Debug.Log("The array passed to SetDigitsToDisplay is " + digits.Length.ToString() + ", not 7!");
            return str;
        }

        int ite;

        for (ite = 0; ite < 2; ++ite)
            str += digits[ite].ToString();

        str += "'";

        for (ite = 2; ite < 4; ++ite)
            str += digits[ite].ToString();

        str += "\"";

        for (ite = 4; ite < 7; ++ite)
            str += digits[ite].ToString();

        return str;
    }
}