using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomKickLine : MonoBehaviour {

    private Text text = null;
    private string[] lines = null;

	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();

        TextAsset txtAsset = Resources.Load("KickLines") as TextAsset;
        lines = txtAsset.text.Split(new char[]{'#'});
	}

    void OnEnable()
    {
        text.text = lines[Random.Range(0, lines.Length)];
    }
}
