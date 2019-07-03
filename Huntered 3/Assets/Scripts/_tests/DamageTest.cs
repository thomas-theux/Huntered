using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour {

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerSheet>().currentHealth -= 0.1f;
        }
    }

}
