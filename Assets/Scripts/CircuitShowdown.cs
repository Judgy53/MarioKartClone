using UnityEngine;
using System.Collections;

public class CircuitShowdown : MonoSingleton<CircuitShowdown> {

    [SerializeField]
    GameObject[] circuitPrefabs = null;

    private int circuitCursor = 0;
    private GameObject displayedCircuit = null;

	// Use this for initialization
	void Start () {
        if (circuitPrefabs.Length == 0)
            Debug.Log("Please have at least one circuit in the menu circuit showdown array.");

        Camera.main.transform.position = new Vector3(0f, 350f, 0f);
        Camera.main.transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);

        displayedCircuit = Instantiate(circuitPrefabs[0], Vector3.zero, Quaternion.AngleAxis(0f, Vector3.up)) as GameObject;

        Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        displayedCircuit.transform.rotation = Quaternion.AngleAxis(Time.time * 10f, Vector3.up);
	}

    void FixedUpdate()
    {
        displayedCircuit.transform.position = Vector3.Lerp(displayedCircuit.transform.position, Vector3.zero, 0.1f);
    }

    public void NextCircuit ()
    {
        Destroy(displayedCircuit, 0f);

        circuitCursor = (circuitCursor + 1) % circuitPrefabs.Length;

        displayedCircuit = Instantiate(circuitPrefabs[circuitCursor], Vector3.right * 1000f, Quaternion.AngleAxis(0f, Vector3.up)) as GameObject;
    }

    public void PreviousCircuit()
    {
        Destroy(displayedCircuit, 0f);

        circuitCursor = circuitCursor == 0 ? circuitPrefabs.Length - 1 : circuitCursor - 1;

        displayedCircuit = Instantiate(circuitPrefabs[circuitCursor], Vector3.left * 1000f, Quaternion.AngleAxis(0f, Vector3.up)) as GameObject;
    }
}
