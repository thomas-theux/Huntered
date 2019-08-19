using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class GearUI : MonoBehaviour {

    public PlayerSheet PlayerSheetScript;
    public GhostsUI GhostsUIScript;
    private AudioManager audioManagerScript;

    public Image GearNavCursor;
    public TMP_Text GearTitle;

    public GameObject[] GearArray;
    public Image GhostCursor;

    public GameObject LinkingDialog;
    public TMP_Text DialogTitle;
    public TMP_Text DialogGhostEffect;
    public TMP_Text DialogText;

    public GameObject[] GhostSlotsParent;
    public GameObject ImprovementTextsParent;

    public GameObject GhostSelection;
    public GameObject GearInterface;
    public GameObject MenuNav;

    private List<Image> GhostSlotsArr = new List<Image>();
    private List<TMP_Text> ImprovementTextsArr = new List<TMP_Text>();

    public Sprite GhostImage;

    private float ghostDistance = 47;
    private float ghostCursorPosX;
    private float ghostCursorPosY;

    private int cursorIndex = 0;
    private int ghostNavIndex = 0;
    private int initialCursorPos = -70;
    private int listItemHeight = 100;

    private int linkChance = 0;

    // REWIRED
    private bool navigateUp = false;
    private bool navigateDown = false;
    private bool navigateGhostsLeft = false;
    private bool navigateGhostsRight = false;
    private bool interactBtn = false;
    private bool cancelBtn = false;


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

        // DialogTitle.text = TextsUI.DialogTitle[GameSettings.language];
    }


    private void OnEnable() {
        cursorIndex = 0;

        DisplayGhostSlots();

        DisplayCursor();
        DisplayGearTitle();
        DisplayGearTexts();
    }


    private void Update() {
        GetInput();
        UpdateIndex();

        if (interactBtn) {
            if (PlayerSheetScript.linkingPhase == 0) {
                EnableSlotNavigation();
                return;
            }

            if (PlayerSheetScript.linkingPhase == 1) {
                EnableGhostSelection();
                return;
            }

            if (PlayerSheetScript.linkingPhase == 2) {
                StartLinkingProcess();
                return;
            }

            if (PlayerSheetScript.linkingPhase == 3) {
                LinkGhost();
                return;
            }
        }

        if (cancelBtn) {
            if (PlayerSheetScript.linkingPhase == 1) {
                DisableSlotNavigation();
                return;
            }
            if (PlayerSheetScript.linkingPhase == 2) {
                DisableGhostSelection();
                return;
            }
            if (PlayerSheetScript.linkingPhase == 3) {
                StopLinkingProcess();
                return;
            }
        }

        if (PlayerSheetScript.linkingPhase == 1) {
            UpdateGhostNav();
        }
    }


    private void GetInput() {
        if (PlayerSheetScript.linkingPhase == 0) {
            navigateUp = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Up");
            navigateDown = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Down");
        }
        if (PlayerSheetScript.linkingPhase == 1) {
            navigateGhostsLeft = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Left");
            navigateGhostsRight = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Right");
        }
        if (PlayerSheetScript.linkingPhase > 0) {
            cancelBtn = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("Circle");
        }

        interactBtn = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("X");
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


    private void DisplayGhostNavCursor() {
        // Cursor position
        GhostCursor.transform.localPosition = new Vector2(
            (ghostCursorPosX + (ghostDistance * ghostNavIndex)) - 7,
            ghostCursorPosY + 7
        );

        audioManagerScript.Play("UINavigateMenu");
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


    private void DisplayGhostTitle() {
        int ghostType = 0;

        switch (cursorIndex) {
            case 0:
                if (PlayerSheetScript.SlottedGhostsHead[ghostNavIndex].Contains("Name")) {
                    GearTitle.text = (string)PlayerSheetScript.SlottedGhostsHead[ghostNavIndex]["Name"];
                    ImprovementTextsArr[0].text = (string)PlayerSheetScript.SlottedGhostsHead[ghostNavIndex]["Description"];

                    ghostType = (int)PlayerSheetScript.SlottedGhostsHead[ghostNavIndex]["Type"];
                    ImprovementTextsArr[0].color = ColorManager.GhostColors[ghostType];
                } else {
                    GearTitle.text = TextsUI.GearSlotEmptyTitle[GameSettings.language];
                    ImprovementTextsArr[0].text = "<i>" + TextsUI.GearSlotEmptyText[GameSettings.language] + "</i>";
                    ImprovementTextsArr[0].color = ColorManager.KeyWhite40;
                }
                break;
            case 1:
                if (PlayerSheetScript.SlottedGhostsTorso[ghostNavIndex].Contains("Name")) {
                    GearTitle.text = (string)PlayerSheetScript.SlottedGhostsTorso[ghostNavIndex]["Name"];
                    ImprovementTextsArr[0].text = (string)PlayerSheetScript.SlottedGhostsTorso[ghostNavIndex]["Description"];

                    ghostType = (int)PlayerSheetScript.SlottedGhostsTorso[ghostNavIndex]["Type"];
                    ImprovementTextsArr[0].color = ColorManager.GhostColors[ghostType];
                } else {
                    GearTitle.text = TextsUI.GearSlotEmptyTitle[GameSettings.language];
                    ImprovementTextsArr[0].text = "<i>" + TextsUI.GearSlotEmptyText[GameSettings.language] + "</i>";
                    ImprovementTextsArr[0].color = ColorManager.KeyWhite40;
                }
                break;
            case 2:
                if (PlayerSheetScript.SlottedGhostsWeapon[ghostNavIndex].Contains("Name")) {
                    GearTitle.text = (string)PlayerSheetScript.SlottedGhostsWeapon[ghostNavIndex]["Name"];
                    ImprovementTextsArr[0].text = (string)PlayerSheetScript.SlottedGhostsWeapon[ghostNavIndex]["Description"];

                    ghostType = (int)PlayerSheetScript.SlottedGhostsWeapon[ghostNavIndex]["Type"];
                    ImprovementTextsArr[0].color = ColorManager.GhostColors[ghostType];
                } else {
                    GearTitle.text = TextsUI.GearSlotEmptyTitle[GameSettings.language];
                    ImprovementTextsArr[0].text = "<i>" + TextsUI.GearSlotEmptyText[GameSettings.language] + "</i>";
                    ImprovementTextsArr[0].color = ColorManager.KeyWhite40;
                }
                break;
            case 3:
                if (PlayerSheetScript.SlottedGhostsLegs[ghostNavIndex].Contains("Name")) {
                    GearTitle.text = (string)PlayerSheetScript.SlottedGhostsLegs[ghostNavIndex]["Name"];
                    ImprovementTextsArr[0].text = (string)PlayerSheetScript.SlottedGhostsLegs[ghostNavIndex]["Description"];

                    ghostType = (int)PlayerSheetScript.SlottedGhostsLegs[ghostNavIndex]["Type"];
                    ImprovementTextsArr[0].color = ColorManager.GhostColors[ghostType];
                } else {
                    GearTitle.text = TextsUI.GearSlotEmptyTitle[GameSettings.language];
                    ImprovementTextsArr[0].text = "<i>" + TextsUI.GearSlotEmptyText[GameSettings.language] + "</i>";
                    ImprovementTextsArr[0].color = ColorManager.KeyWhite40;
                }
                break;
        }
    }


    private void SetGhostTextsEmpty() {
        for (int i = 1; i < ImprovementTextsParent.transform.childCount; i++) {
            ImprovementTextsArr[i].text = "";
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
                        int ghostType = (int)PlayerSheetScript.SlottedGhostsTorso[i]["Type"];
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
                        int ghostType = (int)PlayerSheetScript.SlottedGhostsWeapon[i]["Type"];
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
                        int ghostType = (int)PlayerSheetScript.SlottedGhostsLegs[i]["Type"];
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
                int ghostType = (int)PlayerSheetScript.SlottedGhostsTorso[i]["Type"];
                int ghostLevel = (int)PlayerSheetScript.SlottedGhostsTorso[i]["Level"];
                GhostSlotsParent[1].transform.GetChild(i).GetComponent<Image>().color = ColorManager.GhostColors[ghostType];
                GhostSlotsParent[1].transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = ghostLevel + "";
                GhostSlotsParent[1].transform.GetChild(i).GetComponent<Image>().sprite = GhostImage;
            } else {
                GhostSlotsParent[1].transform.GetChild(i).GetComponent<Image>().color = ColorManager.Whitet8;
                GhostSlotsParent[1].transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            }
        }

        for (int i = 0; i < PlayerSheetScript.SlottedGhostsWeapon.Count; i++) {
            if (PlayerSheetScript.SlottedGhostsWeapon[i].Contains("Name")) {
                int ghostType = (int)PlayerSheetScript.SlottedGhostsWeapon[i]["Type"];
                int ghostLevel = (int)PlayerSheetScript.SlottedGhostsWeapon[i]["Level"];
                GhostSlotsParent[2].transform.GetChild(i).GetComponent<Image>().color = ColorManager.GhostColors[ghostType];
                GhostSlotsParent[2].transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = ghostLevel + "";
                GhostSlotsParent[2].transform.GetChild(i).GetComponent<Image>().sprite = GhostImage;
            } else {
                GhostSlotsParent[2].transform.GetChild(i).GetComponent<Image>().color = ColorManager.Whitet8;
                GhostSlotsParent[2].transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            }
        }

        for (int i = 0; i < PlayerSheetScript.SlottedGhostsLegs.Count; i++) {
            if (PlayerSheetScript.SlottedGhostsLegs[i].Contains("Name")) {
                int ghostType = (int)PlayerSheetScript.SlottedGhostsLegs[i]["Type"];
                int ghostLevel = (int)PlayerSheetScript.SlottedGhostsLegs[i]["Level"];
                GhostSlotsParent[3].transform.GetChild(i).GetComponent<Image>().color = ColorManager.GhostColors[ghostType];
                GhostSlotsParent[3].transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = ghostLevel + "";
                GhostSlotsParent[3].transform.GetChild(i).GetComponent<Image>().sprite = GhostImage;
            } else {
                GhostSlotsParent[3].transform.GetChild(i).GetComponent<Image>().color = ColorManager.Whitet8;
                GhostSlotsParent[3].transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            }
        }
    }


    private void EnableSlotNavigation() {
        PlayerSheetScript.linkingPhase = 1;

        // Reset Ghost nav cursor to the left
        ghostNavIndex = 0;

        GhostCursor.transform.SetParent(GearArray[cursorIndex].transform);

        ghostCursorPosX = GearArray[cursorIndex].transform.GetChild(1).localPosition.x;
        ghostCursorPosY = GearArray[cursorIndex].transform.GetChild(1).localPosition.y;

        DisplayGhostNavCursor();
        DisplayGhostTitle();
        SetGhostTextsEmpty();

        GhostCursor.color = ColorManager.ImageTransparent0;
    }


    private void DisableSlotNavigation() {
        PlayerSheetScript.linkingPhase = 0;

        GhostCursor.color = ColorManager.ImageTransparent100;

        DisplayGearTitle();
    }


    private void UpdateGhostNav() {
        if (navigateGhostsLeft) {
            if (ghostNavIndex > 0) {
                ghostNavIndex--;
                DisplayGhostNavCursor();
                DisplayGhostTitle();
            }
        }

        if (navigateGhostsRight) {
            if (ghostNavIndex < PlayerSheetScript.UnlockedSlots-1) {
                ghostNavIndex++;
                DisplayGhostNavCursor();
                DisplayGhostTitle();
            }
        }
    }


    private void EnableGhostSelection() {
        PlayerSheetScript.linkingPhase = 2;
        GhostSelection.SetActive(true);
        GearInterface.SetActive(false);
        MenuNav.SetActive(false);
    }


    private void DisableGhostSelection() {
        PlayerSheetScript.linkingPhase = 1;
        GhostSelection.SetActive(false);
        GearInterface.SetActive(true);
        MenuNav.SetActive(true);
    }


    private void StartLinkingProcess() {
        PlayerSheetScript.linkingPhase = 3;

        string ghostEffect = GhostsUIScript.GetGhostEffect();
        int ghostType = GhostsUIScript.GetGhostType();
        linkChance = GhostsUIScript.GetLinkChance();

        DialogTitle.text = TextsUI.DialogTitle[GameSettings.language];
        DialogGhostEffect.text = ghostEffect;
        DialogGhostEffect.color = ColorManager.GhostColors[ghostType];
        DialogText.text = TextsUI.DialogIntro[GameSettings.language] + "<b> " + linkChance + "% Chance" + " </b>" + "\n" + TextsUI.DialogEnd[GameSettings.language];

        LinkingDialog.SetActive(true);
    }


    private void StopLinkingProcess() {
        PlayerSheetScript.linkingPhase = 2;

        LinkingDialog.SetActive(false);
    }


    private void LinkGhost() {
        int rndChance = Random.Range(0, 100);

        if (rndChance < linkChance) {
            // Success linking!
            GameObject linkedGhostGO = GhostsUIScript.GetGhostGO();

            switch (cursorIndex) {
                case 0:
                    // Link to head
                    PlayerSheetScript.SlottedGhostsHead[ghostNavIndex] = linkedGhostGO.GetComponent<GhostSheet>().GhostStats;
                    break;
                case 1:
                    // Link to torso
                    PlayerSheetScript.SlottedGhostsTorso[ghostNavIndex] = linkedGhostGO.GetComponent<GhostSheet>().GhostStats;
                    break;
                case 2:
                    // Link to weapon
                    PlayerSheetScript.SlottedGhostsWeapon[ghostNavIndex] = linkedGhostGO.GetComponent<GhostSheet>().GhostStats;
                    break;
                case 3:
                    // Link to legs
                    PlayerSheetScript.SlottedGhostsLegs[ghostNavIndex] = linkedGhostGO.GetComponent<GhostSheet>().GhostStats;
                    break;
            }

            DisplayGhostSlots();
        } else {
            // Fail linking!
        }

        GhostsUIScript.DestroyGhost();

        StopLinkingProcess();
        DisableGhostSelection();
    }

}
