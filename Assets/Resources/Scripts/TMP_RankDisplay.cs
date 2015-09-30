using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TMP_RankDisplay : MonoBehaviour
{

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
        text.text = "Rank: ";

        int rank = carWpHandler.rank;

        text.text += rank.ToString();

        switch (rank)
        {
            case 1:
                text.text += "st";
                break;
            case 2:
                text.text += "nd";
                break;
            case 3:
                text.text += "rd";
                break;
            default:
                text.text += "th";
                break;
        }
    }
}
