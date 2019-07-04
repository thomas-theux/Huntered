using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class GhostsUI : MonoBehaviour {

    public PlayerInventory PlayerInventoryScript;
    public PlayerSheet PlayerSheetScript;
    private AudioManager audioManagerScript;

    public GameObject GhostsContainer;
    public Image ListedGhost;

    private List<GameObject> displayedGhosts = new List<GameObject>();

    private int listItemHeight = 100;
    private int listItemMargin = 2;

    private float itemDistance = 102;
    private int currentIndex = 0;
    public Image[] GhostFilters;

    // public Image ButtonUpImage;
    // public Image ButtonDownImage;

    private ScrollRect scrollRectangle;
    public GameObject ScrollMask;
    private float scrollSpeed = 8;
    private float scrollAmount;

    // REWIRED
    private bool navigateLeft = false;
    private bool navigateRight = false;
    private bool scrollUp = false;
    private bool scrollDown = false;


    private void Awake() {
        audioManagerScript = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        scrollRectangle = ScrollMask.GetComponent<ScrollRect>();
    }


    private void OnEnable() {
        // Liste mit allen Ghosts löschen
        // PlayerInventoryScript.GhostsInventory.Clear();

        // Go through all dictionaries and write the Ghosts in a master list
        // for (int i = 0; i < 4; i++) {
        //     foreach (Hashtable child in PlayerInventoryScript.AllGhosts[i]) {
        //         PlayerInventoryScript.GhostsInventory.Add(child);
        //     }
        // }

        if (currentIndex == 0) {
            DisplayAllGhosts();
        } else {
            DisplayFilteredGhosts();
        }

        UpdateIconTransparency();
        AdjustScrollView();
    }


    private void Update() {
        GetInput();
        ScrollContent();
        UpdateIndex();
    }


    private void GetInput() {
        navigateLeft = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Left");
        navigateRight = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Right");

        scrollUp = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButton("DPad Up");
        scrollDown = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButton("DPad Down");
    }


    private void UpdateIndex() {
        if (navigateLeft) {
            if (currentIndex > 0) {
                currentIndex--;
                UpdateNav();
            }
        }

        if (navigateRight) {
            if (currentIndex < GhostFilters.Length - 1) {
                currentIndex++;
                UpdateNav();
            }
        }
    }


    private void UpdateNav() {
        audioManagerScript.Play("UINavigateMenu");

        UpdateIconTransparency();

        // Remove all Ghost prefabs from UI
        RemoveListedGhosts();

        // Display the right Ghosts
        if (currentIndex == 0) {
            DisplayAllGhosts();
        } else {
            DisplayFilteredGhosts();
        }

        AdjustScrollView();
    }


    private void UpdateIconTransparency() {
        // Nav icon transparency
        for (int i = 0; i < GhostFilters.Length; i++) {
            if (currentIndex == i) {
                GhostFilters[i].color = ColorManager.ImageTransparent0;
            } else {
                GhostFilters[i].color = ColorManager.ImageTransparent50;
            }
        }
    }


    private void ScrollContent() {
        // Scroll content up and down
        if (scrollUp) {
            scrollRectangle.verticalNormalizedPosition += scrollAmount;
        }

        if (scrollDown) {
            scrollRectangle.verticalNormalizedPosition -= scrollAmount;
        }

        // Display scroll arrow buttons
        // if (scrollRectangle.verticalNormalizedPosition == 1) {
        //     ButtonUpImage.color = ColorManager.ImageTransparent100;
        // } else {
        //     ButtonUpImage.color = ColorManager.ImageTransparent0;
        // }

        // if (scrollRectangle.verticalNormalizedPosition == 0) {
        //     ButtonDownImage.color = ColorManager.ImageTransparent100;
        // } else {
        //     ButtonDownImage.color = ColorManager.ImageTransparent0;
        // }
    }


    private void AdjustScrollView() {
        float children = displayedGhosts.Count;

        scrollRectangle.verticalNormalizedPosition = 1;

        float newHeight = (children * listItemHeight) + ((children - 1) * listItemMargin);
        GhostsContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(100, newHeight);

        float contentHeight = GhostsContainer.GetComponent<RectTransform>().sizeDelta.y;
        print(contentHeight);
        scrollAmount = 1 / (contentHeight);
        print(scrollAmount);
    }


    private void DisplayAllGhosts() {
        // Instantiate Ghosts with proper position
        for (int j = 0; j < PlayerInventoryScript.AllGhosts.Count; j++) {
            Image newGhost = Instantiate(ListedGhost, GhostsContainer.transform);
            displayedGhosts.Add(newGhost.gameObject);
            newGhost.transform.SetParent(GhostsContainer.transform);
            newGhost.transform.localPosition = new Vector2(
                0,
                0 - (itemDistance * j)
            );

            // Check for types and apply language
            string typeText = "";
            switch ((int)PlayerInventoryScript.AllGhosts[j]["Type"]) {
                case 0:
                    typeText = TextsUI.GhostsTypeStrength[GameSettings.language];
                    break;
                case 1:
                    typeText = TextsUI.GhostsTypeSpeed[GameSettings.language];
                    break;
                case 2:
                    typeText = TextsUI.GhostsTypeLuck[GameSettings.language];
                    break;
                case 3:
                    typeText = TextsUI.GhostsTypeWisdom[GameSettings.language];
                    break;
            }

            newGhost.transform.GetChild(1).GetComponent<TMP_Text>().text = (string)PlayerInventoryScript.AllGhosts[j]["Name"];
            newGhost.transform.GetChild(2).GetComponent<TMP_Text>().text = typeText;
            newGhost.transform.GetChild(3).GetComponent<TMP_Text>().text = "LVL " + (int)PlayerInventoryScript.AllGhosts[j]["Level"];
        }
    }


    private void DisplayFilteredGhosts() {
        for (int k = 0; k < PlayerInventoryScript.GhostsInventory[currentIndex-1].Count; k++) {
            Image newGhost = Instantiate(ListedGhost, GhostsContainer.transform);
            displayedGhosts.Add(newGhost.gameObject);
            newGhost.transform.SetParent(GhostsContainer.transform);
            newGhost.transform.localPosition = new Vector2(
                0,
                0 - (itemDistance * k)
            );

            // Check for types and apply language
            string typeText = "";
            switch ((int)PlayerInventoryScript.GhostsInventory[currentIndex-1][k]["Type"]) {
                case 0:
                    typeText = TextsUI.GhostsTypeStrength[GameSettings.language];
                    break;
                case 1:
                    typeText = TextsUI.GhostsTypeSpeed[GameSettings.language];
                    break;
                case 2:
                    typeText = TextsUI.GhostsTypeLuck[GameSettings.language];
                    break;
                case 3:
                    typeText = TextsUI.GhostsTypeWisdom[GameSettings.language];
                    break;
            }

            newGhost.transform.GetChild(1).GetComponent<TMP_Text>().text = (string)PlayerInventoryScript.GhostsInventory[currentIndex-1][k]["Name"];
            newGhost.transform.GetChild(2).GetComponent<TMP_Text>().text = typeText;
            newGhost.transform.GetChild(3).GetComponent<TMP_Text>().text = "LVL " + (int)PlayerInventoryScript.GhostsInventory[currentIndex-1][k]["Level"];
        }
    }


    private void OnDisable() {
        RemoveListedGhosts();
    }


    private void RemoveListedGhosts() {
        // Remove all (listed) Ghost elements from list
        for (int i = 0; i < displayedGhosts.Count; i++) {
            Destroy(displayedGhosts[i]);
        }

        displayedGhosts.Clear();
    }

}
