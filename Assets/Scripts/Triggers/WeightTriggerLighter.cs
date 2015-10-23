using UnityEngine;
using System.Collections;

public class WeightTriggerLighter : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" || collider.tag == "Bot")
        {
            collider.gameObject.SendMessage("SetDownForce", 50, SendMessageOptions.DontRequireReceiver);
            collider.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
