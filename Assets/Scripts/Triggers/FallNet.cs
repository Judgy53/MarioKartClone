using UnityEngine;
using System.Collections;

public class FallNet : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
		collider.gameObject.SendMessage("OnOutOfMap", SendMessageOptions.DontRequireReceiver);
    }
}
