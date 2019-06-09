using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIHandler : MonoBehaviour {

    public TMP_Text currentGoldText;
    public TMP_Text currentRepText;
    public TMP_Text currentRepLevel;
    public GameObject playerGO;

    private PlayerSheet playerSheetScript;


    private void Awake() {
        playerSheetScript = playerGO.GetComponent<PlayerSheet>();
    }


    private void Update() {
        currentGoldText.text = playerSheetScript.currentGold + "";
        currentRepText.text = ReputationManager.currentRep.ToString("F0") + " / " + ReputationManager.neededRepArr[ReputationManager.currentRepLevel].ToString("F0");
        currentRepLevel.text = ReputationManager.currentRepLevel + "";
    }

}
