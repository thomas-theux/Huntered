using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeHandler : MonoBehaviour {

    private EnemySheet enemyControllerScript;

    public GameObject healthGO;
    public Slider healthBar;

    public float currentHealth = 0;
    public float maxHealth = 0;
    private float calculatedHealth;

    private bool healthBarActive = false;


    private void Awake() {
        enemyControllerScript = GetComponent<EnemySheet>();

        calculatedHealth = GameSettings.enemyBaseHealth;

        for (int i = 1; i < enemyControllerScript.enemyLevel; i++) {
            calculatedHealth *= GameSettings.enemyHealthMultiplier;
        }

        maxHealth = calculatedHealth;
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
            GetComponent<DropLoot>().DropGold();
            GetComponent<DropLoot>().DropGhosts();

            GetComponent<GainRep>().AddRep();

            for (int i = 0; i < GameManager.AllPlayers.Count; i++) {
                // print(transform.Find("Aggro Radius").gameObject);
                Collider aggroRadius = transform.Find("Aggro Radius").GetComponent<Collider>();
                GameManager.AllPlayers[i].GetComponent<RemoveFromTrigger>().RemoveEnemyFromList(aggroRadius);
            }

            Destroy(this.gameObject);
        }
    }

}
