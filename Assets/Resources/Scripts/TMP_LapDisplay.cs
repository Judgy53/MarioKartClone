using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TMP_LapDisplay : MonoBehaviour {

    private Text text = null;
    private CarWaypointHandler carWpHandler = null;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        carWpHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<CarWaypointHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Lap: " + ((int)carWpHandler.Laps + 1).ToString() + "/999";
    }
}
