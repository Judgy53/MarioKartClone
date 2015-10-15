using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shhh : MonoBehaviour {

    Shadow[] shadows;
    public int length = 5;

	// Use this for initialization
	void Start () {
        shadows = new Shadow[length];
        for (int i = 0; i < shadows.Length; ++i)
        {
            shadows[i] = gameObject.AddComponent<Shadow>() as Shadow;
            shadows[i].useGraphicAlpha = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 vec = new Vector2();

        for (int i = 0; i < shadows.Length; ++i)
        {
            vec.x = (float)i + 2f *Mathf.Cos(2f * Time.realtimeSinceStartup + (float)i / 2f);
            vec.y = (float)i - 2f *Mathf.Cos(2f * Time.realtimeSinceStartup + (float)i / 2f);
            shadows[i].effectDistance = vec;
            shadows[i].effectColor = new Color(Random.value, Random.value, Random.value);
        }
	}
}
