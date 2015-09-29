using UnityEngine;
using System.Collections;

public class NeverGonnaLetYouDown : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
		collider.gameObject.SendMessage("NeverGonnaGiveYouUp");
    }
}
