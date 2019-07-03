using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionTest : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerSheet>().currentHealth += 20;
            Destroy(this.gameObject);
        }
    }

}
