using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class GearUI : MonoBehaviour {

    public PlayerSheet PlayerSheetScript;

    public Image GearNavCursor;
    public TMP_Text GearTitle;

    public GameObject[] GhostSlotsParent;
    public GameObject ImprovementTextsParent;

    private List<Image> GhostSlotsArr = new List<Image>();
    private List<TMP_Text> ImprovementTextsArr = new List<TMP_Text>();

    private int cursorIndex = 0;
    private int initialCursorPos = -90;
    private int listItemHeight = 110;

    // REWIRED
    private bool navigateUp = false;
    private bool navigateDown = false;


    private void Awake() {
        DisplayCursor();
        DisplayGearTitle();
    }


    private void Update() {
        GetInput();
        UpdateIndex();
    }


    private void GetInput() {
        navigateUp = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Up");
        navigateDown = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Down");
    }


    private void UpdateIndex() {
        if (navigateUp) {
            if (cursorIndex > 0) {
                cursorIndex--;
                DisplayCursor();
                DisplayGearTitle();
            }
        }

        if (navigateDown) {
            if (cursorIndex < 3) {
                cursorIndex++;
                DisplayCursor();
                DisplayGearTitle();
            }
        }
    }


    private void DisplayCursor() {
        GearNavCursor.transform.localPosition = new Vector2(
            GearNavCursor.transform.localPosition.x,
            initialCursorPos - (cursorIndex * listItemHeight)
        );
    }


    private void DisplayGearTitle() {
        switch (cursorIndex) {
            case 0:
                GearTitle.text = TextsUI.GearTitleHead[GameSettings.language];
                break;
            case 1:
                GearTitle.text = TextsUI.GearTitleTorso[GameSettings.language];
                break;
            case 2:
                GearTitle.text = TextsUI.GearTitleWeapon[GameSettings.language];
                break;
            case 3:
                GearTitle.text = TextsUI.GearTitleLegs[GameSettings.language];
                break;
        }
    }


    private void DisplayGearTexts() {
    }

}
