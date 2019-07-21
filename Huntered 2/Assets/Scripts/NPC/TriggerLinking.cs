using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLinking : MonoBehaviour {

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            if (other.GetComponent<PlayerController>().interactBtn) {
                other.GetComponent<PlayerController>().OpenLinkingMenu();
            }
        }
    }

}
