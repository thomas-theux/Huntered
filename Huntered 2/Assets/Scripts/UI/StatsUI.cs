using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class StatsUI : MonoBehaviour {

    private PlayerSheet playerSheetScript;
    private AudioManager audioManagerScript;

    public GameObject StatsParentGO;
    public GameObject MainContainer;

    public TMP_Text StatsTitle;
    public TMP_Text StatsText;

    public Slider[] StatsSliders;

    // Game language
    // public TMP_Text[] StatsTexts;
    public GameObject[] StatMenuItems;
    private List<Image> StatIcons = new List<Image>();
    private List<TMP_Text> StatValues = new List<TMP_Text>();

    // public TMP_Text healthStat;
    // public TMP_Text damageStat;
    // public TMP_Text speedStat;
    // public TMP_Text cooldownStat;

    public Image UICursor;
    private Vector2 initialCursorPos;
    private float menuItemDistance = 110.0f;

    private int currentIndex = 0;
    private int maxIndex = 3;

    private bool canIncrease = true;

	private float minThreshold = 0.5f;
	private float maxThreshold = 0.5f;
	private bool axisYActive;

    private float increaseHealthBy = 0.025f;
    private float increaseDamageBy = 0.015f;
    private float increaseSpeedBy = 0.002f;
    private float decreaseCooldownBy = 0.00005f;

    private int statCost = 10;
    private int costIncreaseMultiplierDef = 1;
    private int costIncreaseMultiplier = 1;
    private float costIncreaseDelay = 1.0f;
    private float t = 0;

    private bool playIncreaseSound = false;
    private Sound s;
    private float pitchDef;

    private bool initialized = false;

    // REWIRED
    private bool dpadUp;
    private bool dpadDown;
    private float verticalAxis;
    private bool interactBtn;


    private void Awake() {
        // Set language
        // StatsTexts[0].text = TextsUI.StatsTextHealth[GameSettings.language];
        // StatsTexts[1].text = TextsUI.StatsTextDamage[GameSettings.language];
        // StatsTexts[2].text = TextsUI.StatsTextSpeed[GameSettings.language];
        // StatsTexts[3].text = TextsUI.StatsTextCooldown[GameSettings.language];

        // Get all stats texts
        for (int i = 0; i < StatMenuItems.Length; i++) {
            StatIcons.Add(StatMenuItems[i].transform.GetChild(0).GetComponent<Image>());
            StatValues.Add(StatMenuItems[i].transform.GetChild(1).GetComponent<TMP_Text>());
        }
    }


    public void OnEnable() {
        if (!initialized) {
            playerSheetScript = transform.root.GetComponent<PlayerSheet>();
            audioManagerScript = GameObject.Find("AudioManager").GetComponent<AudioManager>();

            initialCursorPos = UICursor.transform.localPosition;

            s = Array.Find(audioManagerScript.sounds, sound => sound.name == "UIIncreaseStat");
            pitchDef = s.source.pitch;

            // Set the canvas of the second player to the left
            if (playerSheetScript.playerID == 1) {
                MainContainer.GetComponent<Image>().rectTransform.anchorMin = new Vector2(1, 0);
                MainContainer.GetComponent<Image>().rectTransform.anchorMax = new Vector2(1, 1);
                MainContainer.GetComponent<Image>().rectTransform.pivot = new Vector2(1, 0.5f);
            }

            t = costIncreaseDelay;

            initialized = true;
        }

        DisplayStats();
        DisplayCursor();
        DisplayTexts();
    }


    private void Update() {
        if (initialized && playerSheetScript.CharMenuUI) {
            GetInput();
            UpdateIndex();
            IncreaseStat();
            IncreaseCosts();
        }
    }


    private void GetInput() {
        dpadUp = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButtonDown("DPad Up");
        dpadDown = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButtonDown("DPad Down");

        verticalAxis = ReInput.players.GetPlayer(playerSheetScript.playerID).GetAxis("LS Vertical");

        interactBtn = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButton("X");
    }


    private void UpdateIndex() {
        // UI navigation with the analog sticks
        if (ReInput.players.GetPlayer(playerSheetScript.playerID).GetAxis("LS Vertical") > maxThreshold && !axisYActive) {
            if (currentIndex > 0) {
                axisYActive = true;
                currentIndex--;
            }
            DisplayCursor();
            DisplayTexts();
        }

        if (ReInput.players.GetPlayer(playerSheetScript.playerID).GetAxis("LS Vertical") < -maxThreshold && !axisYActive) {
            if (currentIndex < maxIndex) {
                axisYActive = true;
                currentIndex++;
            }
            DisplayCursor();
            DisplayTexts();
        }

        if (ReInput.players.GetPlayer(playerSheetScript.playerID).GetAxis("LS Vertical") <= minThreshold && ReInput.players.GetPlayer(playerSheetScript.playerID).GetAxis("LS Vertical") >= -minThreshold) {
            axisYActive = false;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // UI navigation with the dpad
        if (dpadUp) {
            if (currentIndex > 0) currentIndex--;
            DisplayCursor();
            DisplayTexts();
        }

        if (dpadDown) {
            if (currentIndex < maxIndex) currentIndex++;
            DisplayCursor();
            DisplayTexts();
        }
    }


    private void DisplayCursor() {
        // Cursor position
        UICursor.transform.localPosition = new Vector2(initialCursorPos.x, initialCursorPos.y - (currentIndex * menuItemDistance));
        audioManagerScript.Play("UINavigateMenu");

        // Nav text colors
        for (int i = 0; i < StatMenuItems.Length; i++) {
            if (currentIndex == i) {
                // StatsTexts[i].color = ColorManager.KeyBlue10;
                StatValues[i].color = ColorManager.KeyGold50;
                StatIcons[i].color = ColorManager.KeyGold50;
            } else {
                // StatsTexts[i].color = ColorManager.KeyWhite50;
                StatValues[i].color = ColorManager.KeyWhite40;
                StatIcons[i].color = ColorManager.KeyWhite40;
            }
        }
    }


    private void DisplayStats() {
        // Display the current stats values
        // healthStat.text = playerSheetScript.maxHealth.ToString("F2");
        StatValues[0].text = playerSheetScript.maxHealth.ToString("F1");

        float damage = (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Damage"];
        // damageStat.text = damage.ToString("F2");
        StatValues[1].text = damage.ToString("F2");

        // speedStat.text = playerSheetScript.moveSpeed.ToString("F2");
        StatValues[2].text = playerSheetScript.moveSpeed.ToString("F2");

        float cooldown = (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Cooldown"];
        cooldown *= 1000;
        // cooldownStat.text = cooldown.ToString("F0");
        StatValues[3].text = cooldown.ToString("F1");


        // Adjust the sliders values
        float currentHealthValue = 1 / (GameSettings.MaxHealthStat / playerSheetScript.maxHealth);
        StatsSliders[0].value = currentHealthValue;

        float currentDamageValue = 1 / (GameSettings.MaxDamageStat / (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Damage"]);
        StatsSliders[1].value = currentDamageValue;

        float currentSpeedValue = 1 / (GameSettings.MaxSpeedStat / playerSheetScript.moveSpeed);
        StatsSliders[2].value = currentSpeedValue;

        float currentCooldownValue = 1 / (((float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Cooldown"] * 1000) / GameSettings.MaxCooldownStat);
        StatsSliders[3].value = currentCooldownValue;
    }


    private void DisplayTexts() {
        switch (currentIndex) {
            case 0:
                StatsTitle.text = TextsUI.StatsTitleHealth[GameSettings.language];
                StatsText.text = TextsUI.StatsTextHealth[GameSettings.language];
                break;
            case 1:
                StatsTitle.text = TextsUI.StatsTitleDamage[GameSettings.language];
                StatsText.text = TextsUI.StatsTextDamage[GameSettings.language];
                break;
            case 2:
                StatsTitle.text = TextsUI.StatsTitleSpeed[GameSettings.language];
                StatsText.text = TextsUI.StatsTextSpeed[GameSettings.language];
                break;
            case 3:
                StatsTitle.text = TextsUI.StatsTitleCooldown[GameSettings.language];
                StatsText.text = TextsUI.StatsTextCooldown[GameSettings.language];
                break;
        }
    }


    private void IncreaseCosts() {
        // Increase costs when player is holding interact button
        if (interactBtn) {
            t -= Time.deltaTime;

            if (t <= 0) {
                costIncreaseMultiplier++;
                t = costIncreaseDelay;
            }
        }

        // Reset gold costs for stats when player is not increasing them
        if (costIncreaseMultiplier != costIncreaseMultiplierDef && !interactBtn) {
            costIncreaseMultiplier = costIncreaseMultiplierDef;
        }
    }


    private void IncreaseStat() {
        if (interactBtn && playerSheetScript.currentGold >= statCost * costIncreaseMultiplier) {

            canIncrease = true;

            switch (currentIndex) {
                case 0:
                    if (playerSheetScript.maxHealth >= GameSettings.MaxHealthStat)
                        canIncrease = false;
                    break;
                case 1:
                    if ((float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Damage"] >= GameSettings.MaxDamageStat)
                        canIncrease = false;
                    break;
                case 2:
                    if (playerSheetScript.moveSpeed >= GameSettings.MaxSpeedStat)
                        canIncrease = false;
                    break;
                case 3:
                    if (((float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Cooldown"] * 1000) <= GameSettings.MaxCooldownStat)
                        canIncrease = false;
                    break;
            }

            if (canIncrease) {
                playerSheetScript.currentGold -= statCost * costIncreaseMultiplier;

                if (!playIncreaseSound) {
                    playIncreaseSound = true;
                    audioManagerScript.Play("UIIncreaseStat");
                }

                s.source.pitch += 0.005f;

                switch (currentIndex) {
                    case 0:
                        float relativeHealth = playerSheetScript.currentHealth / playerSheetScript.maxHealth;
                        playerSheetScript.currentHealth += relativeHealth * (increaseHealthBy * costIncreaseMultiplier);
                        playerSheetScript.maxHealth += increaseHealthBy * costIncreaseMultiplier;
                        break;
                    case 1:
                        playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Damage"] = (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Damage"] + (increaseDamageBy * costIncreaseMultiplier);
                        break;
                    case 2:
                        playerSheetScript.moveSpeed += increaseSpeedBy * costIncreaseMultiplier;
                        break;
                    case 3:
                        playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Cooldown"] = (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Cooldown"] - (decreaseCooldownBy * costIncreaseMultiplier);
                        break;
                }
            } else {
                audioManagerScript.Stop("UIIncreaseStat");
            }

            DisplayStats();
        } else {
            if (playIncreaseSound) {
                playIncreaseSound = false;
                audioManagerScript.Stop("UIIncreaseStat");
                s.source.pitch = pitchDef;
            }
        }
    }


    private IEnumerator IncreaseSound() {
        playIncreaseSound = true;

        audioManagerScript.Play("UIIncreaseStat");
        yield return new WaitForSeconds(0.05f);

        playIncreaseSound = false;
    }

}
