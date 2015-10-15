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

    //// BROKEN.
    /*private IEnumerator SlideToNextCircuit (bool toRight)
    {
        while (toRight ? displayedCircuit.transform.position.x > -900 : displayedCircuit.transform.position.x < 900)
        {
            displayedCircuit.transform.position = Vector3.Lerp(displayedCircuit.transform.position, (toRight ? Vector3.left : Vector3.right) * 1000f, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }

        Destroy(displayedCircuit, 0f);

        if (toRight)
            circuitCursor = (circuitCursor + 1) % circuitPrefabs.Length;
        else
            circuitCursor = circuitCursor == 0 ? circuitPrefabs.Length - 1 : circuitCursor - 1;

        displayedCircuit = Instantiate(circuitPrefabs[circuitCursor], (toRight ? Vector3.right : Vector3.left) * 1000f, Quaternion.AngleAxis(0f, Vector3.up)) as GameObject;

        while (toRight ? displayedCircuit.transform.position.x > 0.1f : displayedCircuit.transform.position.x < -0.1f)
        {
            displayedCircuit.transform.position = Vector3.Lerp(displayedCircuit.transform.position, Vector3.zero, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log(Mathf.Approximately(displayedCircuit.transform.position.x, 0f).ToString());

        yield return 0;
    }*/
}
