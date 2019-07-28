using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class CharUIManager : MonoBehaviour {

    public PlayerSheet PlayerSheetScript;
    private AudioManager audioManagerScript;

    public GameObject[] CharMenuInterfaces;
    public TMP_Text[] NavTexts;
    public Image cursorImage;

    private int currentIndex = 0;
    private float buttonDistance = 140;
    private float cursorStartX;

    // Game language
    // public TMP_Text[] MenuNavTexts;


    private void Awake() {
        audioManagerScript = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cursorStartX = cursorImage.transform.localPosition.x;

        // Set language
        NavTexts[0].text = TextsUI.CharMenuHero[GameSettings.language];
        NavTexts[1].text = TextsUI.CharMenuSkills[GameSettings.language];
        NavTexts[2].text = TextsUI.CharMenuGear[GameSettings.language];
        NavTexts[3].text = TextsUI.CharMenuGhosts[GameSettings.language];
    }


    // REWIRED
    private bool navigateLeft = false;
    private bool navigateRight = false;


    private void OnEnable() {
        DisplayUI();
        DisplayCursor();
    }


    private void DisplayUI() {
        for (int i = 0; i < CharMenuInterfaces.Length; i++) { CharMenuInterfaces[i].SetActive(false); }
        CharMenuInterfaces[currentIndex].SetActive(true);
    }


    private void DisplayCursor() {
        // Cursor position
        cursorImage.transform.localPosition = new Vector2(
            cursorStartX + (buttonDistance * currentIndex),
            cursorImage.transform.localPosition.y
        );

        audioManagerScript.Play("UINavigateMenu");

        // Nav text colors
        for (int i = 0; i < NavTexts.Length; i++) {
            if (currentIndex == i) {
                NavTexts[i].color = ColorManager.KeyGold50;
            } else {
                NavTexts[i].color = ColorManager.KeyWhite50t;
            }
        }
    }


    private void Update() {
        GetInput();
        UpdateIndex();
    }


    private void GetInput() {
        navigateLeft = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("L1");
        navigateRight = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("R1");
    }


    private void UpdateIndex() {
        if (navigateLeft) {
            if (currentIndex > 0) {
                currentIndex--;
                DisplayCursor();
                DisplayUI();
            }
        }

        if (navigateRight) {
            if (currentIndex < CharMenuInterfaces.Length - 1) {
                currentIndex++;
                DisplayCursor();
                DisplayUI();
            }
        }
    }

}
