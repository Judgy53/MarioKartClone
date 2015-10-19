using UnityEngine;
using System.Collections;

public class WeightTriggerNormal : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.SendMessage("SetDownForce", 100, SendMessageOptions.DontRequireReceiver);
    }
}
