using UnityEngine;
using System.Collections;

public class WeightTriggerHeavier : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" || collider.tag == "Bot")
        {
            collider.gameObject.SendMessage("SetDownForce", 500, SendMessageOptions.DontRequireReceiver);
            collider.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
