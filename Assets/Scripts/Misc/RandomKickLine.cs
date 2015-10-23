using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomKickLine : MonoSingleton<RandomKickLine> {

    private Text text = null;
    private string[] lines = null;

    private bool wasSeenOnce = false;

	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();

        TextAsset txtAsset = Resources.Load("KickLines") as TextAsset;
        lines = txtAsset.text.Split(new char[]{'#'});
	}

    void OnEnable()
    {
        if (!wasSeenOnce)
        {
            wasSeenOnce = true;
            text.text = "Mario Kart";
        }
        else
            ChangeLine();
    }

    public void ChangeLine()
    {
        text.text = lines[Random.Range(0, lines.Length)];
    }
}
