using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIHandler : MonoBehaviour {

    public TMP_Text currentGoldText;
    public GameObject BasicsInterface;

    private PlayerSheet playerSheetScript;

    private bool initialized = false;


    public void InitializeUI() {
        playerSheetScript = GetComponent<PlayerSheet>();

        if (playerSheetScript.playerID == 1) {
            BasicsInterface.GetComponent<Image>().rectTransform.anchorMin = new Vector2(1, 0);
            BasicsInterface.GetComponent<Image>().rectTransform.anchorMax = new Vector2(1, 1);
            BasicsInterface.GetComponent<Image>().rectTransform.pivot = new Vector2(1, 0.5f);
        }

        initialized = true;
    }


    private void Update() {
        if (initialized) {
            currentGoldText.text = playerSheetScript.currentGold + "";
        }
    }

}
