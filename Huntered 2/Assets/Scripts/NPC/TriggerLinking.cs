using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLinking : MonoBehaviour {

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            if (other.GetComponent<PlayerController>().interactBtn) {
                if (!other.GetComponent<PlayerSheet>().LinkingMenuUI) {
                    other.GetComponent<PlayerController>().interactBtn = false;
                    other.GetComponent<PlayerController>().OpenLinkingMenu();
                }
            }
        }
    }

}
