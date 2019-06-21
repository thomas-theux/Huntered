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
    private TriggerTest triggerTestScript;

    public GameObject ModelGO;
    private Collider playerCollider;

    private float respawnTime;
    private float respawnTimeDef = 5.0f;
    private int respawnTo = -1;

    private bool initialized = false;


    public void InitializeUI() {
        playerSheetScript = GetComponent<PlayerSheet>();
        triggerTestScript = GetComponent<TriggerTest>();

        CharacterIndicator.color = ColorManager.PlayerOne;
        playerCollider = this.GetComponent<Collider>();

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
        if (initialized && !playerSheetScript.isDead) {
            UpdateGold();
            UpdateHealth();
        }

        if (playerSheetScript.isDead) {
            RespawnTimer();
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
            // Remove Player from enemies trigger if inside
            // triggerTestScript.RemoveFromTrigger();

            // Disable all relevant game objects
            ModelGO.SetActive(false);
            CharacterIndicator.fillAmount = 0;
            playerCollider.enabled = false;

            this.transform.rotation = Quaternion.identity;

            respawnTime = respawnTimeDef;

            playerSheetScript.isDead = true;
        }
    }


    private void RespawnTimer() {
        respawnTime -= Time.deltaTime;

        float currentFill = respawnTime / respawnTimeDef;
        CharacterIndicator.fillAmount = 1 - currentFill;

        if (respawnTime <= 0) {
            RespawnPlayer();
        }
    }


    private void RespawnPlayer() {
        if (playerSheetScript.playerID == 0) {
            respawnTo = 1;
        } else {
            respawnTo = 0;
        }

        // Respawn to other players position
        Transform otherPlayer = GameObject.Find("Character" + respawnTo).transform;
        this.gameObject.transform.position = otherPlayer.position;

        ModelGO.SetActive(true);
        playerCollider.enabled = true;

        playerSheetScript.currentHealth = playerSheetScript.maxHealth;

        playerSheetScript.isDead = false;
    }

}
