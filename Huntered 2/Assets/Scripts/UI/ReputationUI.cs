using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReputationUI : MonoBehaviour {

    public TMP_Text currentRepCount;
    public TMP_Text currentRepLevel;


    private void Update() {
        currentRepCount.text = ReputationManager.currentRep.ToString("F0") + " / " + ReputationManager.neededRepArr[ReputationManager.currentRepLevel].ToString("F0");
        currentRepLevel.text = ReputationManager.currentRepLevel + "";
    }
}
