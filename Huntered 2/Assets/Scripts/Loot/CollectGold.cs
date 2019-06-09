using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGold : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            // Formula for gold gain
            int addGold = Mathf.RoundToInt(GameSettings.baseGoldGain + (ReputationManager.currentRepLevel * GameSettings.goldMultiplier));
            other.GetComponent<PlayerSheet>().currentGold += addGold;

            Destroy(this.gameObject);
        }
    }

}
