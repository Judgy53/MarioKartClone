using UnityEngine;
using System.Collections;

public class WeightTriggerNormal : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" || collider.tag == "Bot")
        {
            collider.gameObject.SendMessage("SetDownForce", 100, SendMessageOptions.DontRequireReceiver);
            collider.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
