using UnityEngine;
using System.Collections;

public class WeightTriggerLighter : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.SendMessage("SetDownForce", -100, SendMessageOptions.DontRequireReceiver);
    }
}
