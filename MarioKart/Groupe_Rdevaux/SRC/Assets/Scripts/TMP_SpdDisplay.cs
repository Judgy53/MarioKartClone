using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class TMP_SpdDisplay : MonoBehaviour {

    private Text text = null;
    private CarController carController = null;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Speed: " + ((int)carController.CurrentSpeed*2f).ToString() + " Km/h";
	}
}
