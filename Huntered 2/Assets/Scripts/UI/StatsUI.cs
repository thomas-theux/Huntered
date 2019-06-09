using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class StatsUI : MonoBehaviour {

    public PlayerSheet playerSheetScript;

    public TMP_Text healthStat;
    public TMP_Text damageStat;
    public TMP_Text speedStat;
    public TMP_Text cooldownStat;

    public Image UICursor;
    private Vector2 initialCursorPos;
    private float menuItemDistance = 60.0f;

    private int currentIndex = 0;
    private int maxIndex = 3;

    private float increaseHealthBy = 0.025f;
    private float increaseDamageBy = 0.015f;
    private float increaseSpeedBy = 0.002f;
    private float decreaseCooldownBy = 0.00005f;

    // REWIRED
    private bool dpadUp;
    private bool dpadDown;
    private bool interactBtn;


    private void Awake() {
        initialCursorPos = UICursor.transform.position;
        DisplayStats();
    }


    private void Update() {
        GetInput();
        ChangeIndex();

        IncreaseStat();
    }


    private void GetInput() {
        dpadUp = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButtonDown("DPad Up");
        dpadDown = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButtonDown("DPad Down");

        interactBtn = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButton("X");
    }


    private void ChangeIndex() {
        if (dpadUp && currentIndex > 0) {
            currentIndex--;
            DisplayCursor();
        }

        if (dpadDown && currentIndex < maxIndex) {
            currentIndex++;
            DisplayCursor();
        }
    }


    private void DisplayCursor() {
        UICursor.transform.position = new Vector2(initialCursorPos.x, initialCursorPos.y - (currentIndex * menuItemDistance));
    }


    private void DisplayStats() {
        healthStat.text = playerSheetScript.maxHealth.ToString("F2");

        float damage = (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Damage"];
        damageStat.text = damage.ToString("F2");

        speedStat.text = playerSheetScript.moveSpeed.ToString("F2");

        float cooldown = (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Cooldown"];
        cooldown *= 1000;
        cooldownStat.text = cooldown.ToString("F0");
    }


    private void IncreaseStat() {
        if (interactBtn && playerSheetScript.currentGold >= 10) {
            playerSheetScript.currentGold -= 10;

            switch (currentIndex) {
                case 0:
                    float relativeHealth = playerSheetScript.currentHealth / playerSheetScript.maxHealth;
                    playerSheetScript.currentHealth += relativeHealth * increaseHealthBy;
                    playerSheetScript.maxHealth += increaseHealthBy;
                    break;
                case 1:
                    playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Damage"] = (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Damage"] + increaseDamageBy;
                    break;
                case 2:
                    playerSheetScript.moveSpeed += increaseSpeedBy;
                    break;
                case 3:
                    playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Cooldown"] = (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Cooldown"] - decreaseCooldownBy;
                    break;
            }

            DisplayStats();
        }
    }

}
