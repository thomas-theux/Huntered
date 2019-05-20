using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCLifeHandler : MonoBehaviour {

    public GameObject healthGO;
    public Slider healthBar;

    public float currentHealth = 0;
    public float maxHealth = 0;

    private bool healthBarActive = false;


    private void Awake() {
        maxHealth = GameSettings.baseNPCHealth;
        currentHealth = maxHealth;
    }


    private void Update() {
        // Show health bar once they are close
        if (currentHealth < maxHealth && !healthBarActive) {
            healthBarActive = true;
            healthGO.gameObject.SetActive(true);
        }
        
        // Update value / health of slider
        if (healthBarActive) {
            healthBar.value = currentHealth / maxHealth;
        }

        // Limit current health
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

        // Kill when health is below 0
        if (currentHealth <= 0) {
            GetComponent<GainRep>().SubtractRep();
            Destroy(this.gameObject);
        }
    }

}
