using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterUI : MonoBehaviour {

    public TMP_Text currentGoldText;
    public Slider healthBar;
    public GameObject BasicsInterface;
    public Image CharacterIndicator;

    private PlayerSheet playerSheetScript;

    private bool initialized = false;


    public void InitializeUI() {
        playerSheetScript = GetComponent<PlayerSheet>();

        CharacterIndicator.color = ColorManager.PlayerOne;

        // Set the canvas of the second player to the left
        if (playerSheetScript.playerID == 1) {
            BasicsInterface.GetComponent<Image>().rectTransform.anchorMin = new Vector2(1, 0);
            BasicsInterface.GetComponent<Image>().rectTransform.anchorMax = new Vector2(1, 1);
            BasicsInterface.GetComponent<Image>().rectTransform.pivot = new Vector2(1, 0.5f);

            CharacterIndicator.color = ColorManager.PlayerTwo;
        }

        initialized = true;
    }


    private void Update() {
        if (initialized) {
            UpdateGold();
            UpdateHealth();
        }
    }


    private void UpdateGold() {
        currentGoldText.text = playerSheetScript.currentGold + "";
    }


    private void UpdateHealth() {
        // Update value / health of slider
        healthBar.value = playerSheetScript.currentHealth / playerSheetScript.maxHealth;

        // Limit current health
        if (playerSheetScript.currentHealth > playerSheetScript.maxHealth) {
            playerSheetScript.currentHealth = playerSheetScript.maxHealth;
        }

        // Kill when health is below 0
        if (playerSheetScript.currentHealth <= 0) {
            // Player dies
        }
    }

}
