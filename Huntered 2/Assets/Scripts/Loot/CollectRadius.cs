using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRadius : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Gold") {
            other.GetComponent<CollectGold>().SetPlayerTarget(this.transform.parent.GetComponent<Collider>());
        }
    }

}
