using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

    [SerializeField]
    private Button[] Choices;

    private int choice = -1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("Vertical") > 0f)
            OnNext();

        if (Input.GetAxis("Vertical") < 0f)
            OnPrevious();
	}

    void OnNext()
    {
        choice = (choice + 1) % Choices.Length;

        Choices[choice].Select();
    }

    void OnPrevious()
    {
        --choice;
        if (choice < 0)
            choice = Choices.Length;

        Choices[choice].Select();
    }
}
