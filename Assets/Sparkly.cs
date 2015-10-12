using UnityEngine;
using System.Collections;

public class Sparkly : MonoBehaviour {

    [SerializeField]
    private GameObject prefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Instantiate(prefab, transform.position, transform.rotation);
	}
}
