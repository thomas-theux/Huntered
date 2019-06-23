using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterUI : MonoBehaviour {

    public TMP_Text CurrentGoldText;
    public Slider HealthBar;
    public Slider PotionCooldown;
    public GameObject BasicsInterface;
    public Image CharacterIndicator;

    private PlayerSheet playerSheetScript;
    private RemoveFromTrigger removeFromTriggerScript;

    public GameObject ModelGO;
    private Collider playerCollider;

    private float respawnTime;
    private int respawnTo = -1;

    private bool initialized = false;

    private float smoothSpeed = 10.0f;


    public void InitializeUI() {
        playerSheetScript = GetComponent<PlayerSheet>();
        removeFromTriggerScript = GetComponent<RemoveFromTrigger>();

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

        if (playerSheetScript.PotionCooldownActive) {
            UpdatePotionCooldown();
        }
    }


    private void UpdateGold() {
        CurrentGoldText.text = playerSheetScript.currentGold + "";
    }

    private void UpdatePotionCooldown() {
        float mappedTimeValue = playerSheetScript.PotionCooldownTime / playerSheetScript.PotionCooldownTimeDef;
        PotionCooldown.value = 1 - mappedTimeValue;
    }


    private void UpdateHealth() {
        // Update value / health of slider
        float desiredHP = playerSheetScript.currentHealth / playerSheetScript.maxHealth;
        float smoothedHP = Mathf.Lerp(HealthBar.value, desiredHP, smoothSpeed * Time.deltaTime);
        HealthBar.value = smoothedHP;

        // Limit current health
        if (playerSheetScript.currentHealth > playerSheetScript.maxHealth) {
            playerSheetScript.currentHealth = playerSheetScript.maxHealth;
        }

        // Kill when health is below 0
        if (playerSheetScript.currentHealth <= 0) {
            // Remove Player from enemies trigger if inside
            removeFromTriggerScript.TellEnemiesToRemove();

            // Disable all relevant game objects
            ModelGO.SetActive(false);
            CharacterIndicator.fillAmount = 0;
            playerCollider.enabled = false;

            this.transform.rotation = Quaternion.identity;

            respawnTime = playerSheetScript.respawnTime;

            playerSheetScript.isDead = true;
        }
    }


    private void RespawnTimer() {
        respawnTime -= Time.deltaTime;

        float currentFill = respawnTime / playerSheetScript.respawnTime;
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
