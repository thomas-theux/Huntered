using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSwing : MonoBehaviour {

    private int charID = 0;


    private void Awake() {
        charID = this.gameObject.transform.parent.GetComponent<CharacterMovement>().charID;
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Environment") {
            if (other.GetComponent<CharacterMovement>().charID != charID) {
                Destroy(other.gameObject);
            }
        }
    }
}
