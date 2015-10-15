using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RankingDisplay : MonoBehaviour {

    [SerializeField]
    private List<Text> Texts;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        for (int ite = 0; ite < Texts.Count; ++ite)
        {
            Texts[ite].text = (ite+1).ToString();

            if (ite == 0)
                Texts[ite].text += "st";
            else if (ite == 1)
                Texts[ite].text += "nd";
            else if (ite == 2)
                Texts[ite].text += "rd";
            else
                Texts[ite].text += "th";

            Texts[ite].text += " - ";


            Texts[ite].text += Ranker.Instance.AtRank(ite+1).gameObject.name;
        }
	}
}
