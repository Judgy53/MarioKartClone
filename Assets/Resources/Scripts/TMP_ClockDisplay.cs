using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TMP_ClockDisplay : MonoBehaviour {

    private Text text = null;
    private Clock raceClock = null;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        raceClock = GameObject.FindGameObjectWithTag("RaceClock").GetComponent<Clock>();
    }

    // Update is called once per frame
    void Update()
    {
        int time = (int)raceClock.LocalTimeAsMs;
        
        int[] digits = new int[7];

        int ite;

        for (ite = 6; ite >= 0; --ite)
        {
            if (ite == 2)
            {
                digits[ite] = time % 6;
                time = (time - digits[ite]) / 6;
            }
            else
            {
                digits[ite] = time % 10;
                time = (time - digits[ite]) / 10;
            }
        }

        text.text = string.Empty;

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