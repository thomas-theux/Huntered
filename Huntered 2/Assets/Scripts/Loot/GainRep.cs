using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainRep : MonoBehaviour {

    public void AddRep() {
        ReputationManager.currentRep += ReputationManager.repGainArr[ReputationManager.currentRepLevel];
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
