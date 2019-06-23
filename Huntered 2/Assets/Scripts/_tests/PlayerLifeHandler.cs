using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeHandler : MonoBehaviour {

    private PlayerSheet playerSheetScript;
    public Slider healthBar;


    private void Awake() {
        playerSheetScript = GetComponent<PlayerSheet>();
    }


    private void Update() {
        // Update value / health of slider
        healthBar.value = playerSheetScript.currentHealth / playerSheetScript.maxHealth;

        // Limit current health
        HealthCap();

        // Kill when health is below 0
        if (playerSheetScript.currentHealth <= 0) {
            // Player dies
        }
    }


    private void HealthCap() {
        if (playerSheetScript.currentHealth > playerSheetScript.maxHealth) {
            playerSheetScript.currentHealth = playerSheetScript.maxHealth;
        }
    }

}
