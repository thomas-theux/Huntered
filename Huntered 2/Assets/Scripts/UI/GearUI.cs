using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class GearUI : MonoBehaviour {

    public PlayerSheet PlayerSheetScript;
    private AudioManager audioManagerScript;

    public Image GearNavCursor;
    public TMP_Text GearTitle;

    public GameObject[] GhostSlotsParent;
    public GameObject ImprovementTextsParent;

    private List<Image> GhostSlotsArr = new List<Image>();
    private List<TMP_Text> ImprovementTextsArr = new List<TMP_Text>();

    public Sprite GhostImage;

    private int cursorIndex = 0;
    private int initialCursorPos = -70;
    private int listItemHeight = 100;

    // REWIRED
    private bool navigateUp = false;
    private bool navigateDown = false;


    private void Awake() {
        audioManagerScript = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        InitializeTexts();

        // DisplayCursor();
        // DisplayGearTitle();
        // DisplayGearTexts();
    }


    private void InitializeTexts() {
        for (int i = 0; i < ImprovementTextsParent.transform.childCount; i++) {
            ImprovementTextsArr.Add(ImprovementTextsParent.transform.GetChild(i).GetComponent<TMP_Text>());
        }
    }


    private void OnEnable() {
        DisplayGhostSlots();

        DisplayCursor();
        DisplayGearTitle();
        DisplayGearTexts();
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
                DisplayGearTexts();
            }
        }

        if (navigateDown) {
            if (cursorIndex < 3) {
                cursorIndex++;
                DisplayCursor();
                DisplayGearTitle();
                DisplayGearTexts();
            }
        }
    }


    private void DisplayCursor() {
        audioManagerScript.Play("UINavigateMenu");

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
        switch (cursorIndex) {
            case 0:
                for (int i = 0; i < PlayerSheetScript.SlottedGhostsHead.Count; i++) {
                    if (PlayerSheetScript.SlottedGhostsHead[i].Contains("Name")) {
                        int ghostType = (int)PlayerSheetScript.SlottedGhostsHead[i]["Type"];
                        ImprovementTextsArr[i].text = (string)PlayerSheetScript.SlottedGhostsHead[i]["Description"];
                        ImprovementTextsArr[i].color = ColorManager.GhostColors[ghostType];
                    } else {
                        ImprovementTextsArr[i].text = "—";
                        ImprovementTextsArr[i].color = ColorManager.KeyWhite10;
                    }
                }
                break;

            case 1:
                for (int i = 0; i < PlayerSheetScript.SlottedGhostsTorso.Count; i++) {
                    if (PlayerSheetScript.SlottedGhostsTorso[i].Contains("Name")) {
                        int ghostType = (int)PlayerSheetScript.SlottedGhostsHead[i]["Type"];
                        ImprovementTextsArr[i].text = (string)PlayerSheetScript.SlottedGhostsTorso[i]["Description"];
                        ImprovementTextsArr[i].color = ColorManager.GhostColors[ghostType];
                    } else {
                        ImprovementTextsArr[i].text = "—";
                        ImprovementTextsArr[i].color = ColorManager.KeyWhite10;
                    }
                }
                break;

            case 2:
                for (int i = 0; i < PlayerSheetScript.SlottedGhostsWeapon.Count; i++) {
                    if (PlayerSheetScript.SlottedGhostsWeapon[i].Contains("Name")) {
                        int ghostType = (int)PlayerSheetScript.SlottedGhostsHead[i]["Type"];
                        ImprovementTextsArr[i].text = (string)PlayerSheetScript.SlottedGhostsWeapon[i]["Description"];
                        ImprovementTextsArr[i].color = ColorManager.GhostColors[ghostType];
                    } else {
                        ImprovementTextsArr[i].text = "—";
                        ImprovementTextsArr[i].color = ColorManager.KeyWhite10;
                    }
                }
                break;

            case 3:
                for (int i = 0; i < PlayerSheetScript.SlottedGhostsLegs.Count; i++) {
                    if (PlayerSheetScript.SlottedGhostsLegs[i].Contains("Name")) {
                        int ghostType = (int)PlayerSheetScript.SlottedGhostsHead[i]["Type"];
                        ImprovementTextsArr[i].text = (string)PlayerSheetScript.SlottedGhostsLegs[i]["Description"];
                        ImprovementTextsArr[i].color = ColorManager.GhostColors[ghostType];
                    } else {
                        ImprovementTextsArr[i].text = "—";
                        ImprovementTextsArr[i].color = ColorManager.KeyWhite10;
                    }
                }
                break;
        }
    }


    private void DisplayGhostSlots() {
        for (int i = 0; i < PlayerSheetScript.SlottedGhostsHead.Count; i++) {
            if (PlayerSheetScript.SlottedGhostsHead[i].Contains("Name")) {
                int ghostType = (int)PlayerSheetScript.SlottedGhostsHead[i]["Type"];
                int ghostLevel = (int)PlayerSheetScript.SlottedGhostsHead[i]["Level"];
                GhostSlotsParent[0].transform.GetChild(i).GetComponent<Image>().color = ColorManager.GhostColors[ghostType];
                GhostSlotsParent[0].transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = ghostLevel + "";
                GhostSlotsParent[0].transform.GetChild(i).GetComponent<Image>().sprite = GhostImage;
            } else {
                GhostSlotsParent[0].transform.GetChild(i).GetComponent<Image>().color = ColorManager.Whitet8;
                GhostSlotsParent[0].transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            }
        }

        for (int i = 0; i < PlayerSheetScript.SlottedGhostsTorso.Count; i++) {
            if (PlayerSheetScript.SlottedGhostsTorso[i].Contains("Name")) {
                // Show proper Ghost — copy code from above
            } else {
                GhostSlotsParent[1].transform.GetChild(i).GetComponent<Image>().color = ColorManager.Whitet8;
            }
        }

        for (int i = 0; i < PlayerSheetScript.SlottedGhostsWeapon.Count; i++) {
            if (PlayerSheetScript.SlottedGhostsWeapon[i].Contains("Name")) {
                // Show proper Ghost — copy code from above
            } else {
                GhostSlotsParent[2].transform.GetChild(i).GetComponent<Image>().color = ColorManager.Whitet8;
            }
        }

        for (int i = 0; i < PlayerSheetScript.SlottedGhostsLegs.Count; i++) {
            if (PlayerSheetScript.SlottedGhostsLegs[i].Contains("Name")) {
                // Show proper Ghost — copy code from above
            } else {
                GhostSlotsParent[3].transform.GetChild(i).GetComponent<Image>().color = ColorManager.Whitet8;
            }
        }
    }

}
