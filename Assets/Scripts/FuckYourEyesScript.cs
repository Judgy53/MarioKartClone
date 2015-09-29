using UnityEngine;
using System.Collections;

public class FuckYourEyesScript : MonoBehaviour {

    Renderer rendy = null;
    GameObject lightObject = null;
    Light light = null;

	// Use this for initialization
	void Start () {
        rendy = GetComponent<Renderer>();
        lightObject = new GameObject("Funky Light");
        light = lightObject.AddComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        Color randColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        rendy.material.SetColor("_Color", randColor);
        light.color = randColor;
        lightObject.transform.position = transform.position;
	}

    void OnDestroy()
    {
        Destroy(lightObject, 0f);
    }
}
