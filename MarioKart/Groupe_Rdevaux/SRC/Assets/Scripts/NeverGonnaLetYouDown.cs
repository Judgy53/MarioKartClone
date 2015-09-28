using UnityEngine;
using System.Collections;

public class NeverGonnaLetYouDown : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Bot")
            collider.gameObject.SendMessage("TeleportToLastWayPoint", transform);
    }
}
