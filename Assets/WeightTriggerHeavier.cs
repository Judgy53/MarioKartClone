using UnityEngine;
using System.Collections;

public class WeightTriggerHeavier : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.SendMessage("SetDownForce", 500, SendMessageOptions.DontRequireReceiver);
    }
}
