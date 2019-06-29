using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainRep : MonoBehaviour {

    private int enemyLevel;

    private void Awake() {
        if (this.tag == "Enemy") {
            enemyLevel = GetComponent<EnemySheet>().enemyLevel;
        }
    }


    public void AddRep() {
        float getRep = ReputationManager.repGainArr[ReputationManager.currentRepLevel];
        ReputationManager.currentRep += getRep + getRep * enemyLevel;
        ReputationManager.AddReputation();
    }


    public void SubtractRep() {
        ReputationManager.currentRep -= ReputationManager.repGainArr[ReputationManager.currentRepLevel] * GameSettings.NPCKillMultiplier;
        if (ReputationManager.currentRep < 0) {
            ReputationManager.currentRep = 0;
        }
        ReputationManager.SubtractReputation();
    }

}
