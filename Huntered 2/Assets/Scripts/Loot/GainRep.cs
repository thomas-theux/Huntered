using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainRep : MonoBehaviour {

    public void AddRep() {
        GameManager.currentRep += GameManager.repGainArr[GameManager.currentRepLevel];
        GameManager.AddReputation();
    }


    public void SubtractRep() {
        GameManager.currentRep -= GameManager.repGainArr[GameManager.currentRepLevel] * GameSettings.NPCKillMultiplier;
        if (GameManager.currentRep < 0) {
            GameManager.currentRep = 0;
        }
        GameManager.SubtractReputation();
    }

}
