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
    public Image[] GhostFilters;
    public Image ListedGhost;
    public Image GhostNavCursor;
    public Image ContentContainer;

    private List<GameObject> displayedGhosts = new List<GameObject>();

    private float initialCursorPos;

    private int listItemHeight = 110;
    private int childrenCount = 0;

    private int currentIndex = 0;
    private int cursorIndex = 0;
    private int cursorPos = 0;
    private int minCursorIndex = 1;
    private int maxCursorIndex = 4;

    private float basicYPos = 10.0f;

    private bool scrollIsDelayed = false;
    private float tDef = 0.15f;
    private float t;

    // public Image ButtonUpImage;
    // public Image ButtonDownImage;

    // REWIRED
    private bool navigateLeft = false;
    private bool navigateRight = false;
    private bool scrollUp = false;
    private bool scrollDown = false;


    private void Awake() {
        audioManagerScript = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        initialCursorPos = GhostNavCursor.transform.localPosition.y;
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

        ResetGhostCursor();
        MoveCursor();
    }


    private void Update() {
        GetInput();
        UpdateIndex();

        if (!scrollIsDelayed) {
            ContentNavigation();
        }

        if (scrollIsDelayed) {
            ScrollDelay();
        }

        DisableScrollDelay();
    }


    private void GetInput() {
        navigateLeft = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Left");
        navigateRight = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Right");

        scrollUp = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButton("DPad Up");
        scrollDown = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButton("DPad Down");
    }


    private void ScrollDelay() {
        t -= Time.deltaTime;

        if (t <= 0) {
            scrollIsDelayed = false;
        }
    }


    private void DisableScrollDelay() {
        if (ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonUp("DPad Up") || ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonUp("DPad Down")) {
            scrollIsDelayed = false;
        }
    }


    private void ResetGhostCursor() {
        cursorIndex = 0;
        cursorPos = 0;
        ContentContainer.transform.localPosition = Vector2.zero;
    }


    private void CheckForContent() {
        if (childrenCount == 0) {
            GhostNavCursor.enabled = false;
        } else {
            GhostNavCursor.enabled = true;
        }
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
        ResetGhostCursor();
        CheckForContent();
        MoveCursor();
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


    private void ContentNavigation() {
        if (scrollUp) {
            if (cursorIndex > 0) {
                cursorIndex--;
                MoveCursor();
                // DisplayLinkChance();
                audioManagerScript.Play("UINavigateMenu");

                // Delay scrolling to prevent that scrolling is too fast
                t = tDef;
                scrollIsDelayed = true;

                if (cursorPos > minCursorIndex) {
                    cursorPos--;
                } else {
                    ScrollContent(-1);
                }

                if (cursorIndex == 0) {
                    cursorPos = 0;
                }
            }
        }

        if (scrollDown) {
            if (cursorIndex < childrenCount - 1) {
                cursorIndex++;
                MoveCursor();
                // DisplayLinkChance();
                audioManagerScript.Play("UINavigateMenu");

                // Delay scrolling to prevent that scrolling is too fast
                t = tDef;
                scrollIsDelayed = true;

                if (cursorPos < maxCursorIndex) {
                    cursorPos++;
                } else {
                    ScrollContent(1);
                }

                if (cursorIndex == childrenCount - 1) {
                    cursorPos = maxCursorIndex + 1;
                }
            }
        }

        // Display scroll arrow buttons
    }


    private void MoveCursor() {
        GhostNavCursor.transform.localPosition = new Vector2(
            GhostNavCursor.transform.localPosition.x,
            initialCursorPos - (cursorIndex * listItemHeight)
        );

        print((string)PlayerInventoryScript.AllGhosts[cursorIndex]["Description"]);
    }


    // Display link chance when hovering
    private void DisplayLinkChance() {
        displayedGhosts[cursorIndex-1].transform.GetChild(2).gameObject.SetActive(true);
        displayedGhosts[cursorIndex-1].transform.GetChild(5).gameObject.SetActive(false);

        displayedGhosts[cursorIndex+1].transform.GetChild(2).gameObject.SetActive(true);
        displayedGhosts[cursorIndex+1].transform.GetChild(5).gameObject.SetActive(false);

        displayedGhosts[cursorIndex].transform.GetChild(2).gameObject.SetActive(false);
        displayedGhosts[cursorIndex].transform.GetChild(5).gameObject.SetActive(true);
    }


    private void ScrollContent(int scrollDirection) {
        ContentContainer.transform.localPosition = new Vector2(
            ContentContainer.transform.localPosition.x,
            ContentContainer.transform.localPosition.y + (listItemHeight * scrollDirection)
        );
    }


    private void AdjustScrollView() {
        cursorIndex = 0;
        childrenCount = displayedGhosts.Count;

        // Set the height of cthe ontainer depending on the Ghost count
        float newHeight = childrenCount * listItemHeight;
        GhostsContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(100, newHeight);
    }


    private void DisplayAllGhosts() {
        // Instantiate Ghosts with proper position
        for (int j = 0; j < PlayerInventoryScript.AllGhosts.Count; j++) {
            Image newGhost = Instantiate(ListedGhost, GhostsContainer.transform);
            displayedGhosts.Add(newGhost.gameObject);
            newGhost.transform.SetParent(GhostsContainer.transform);
            newGhost.transform.localPosition = new Vector2(
                0,
                basicYPos - (listItemHeight * j)
            );

            // Check for types and apply language
            string typeText = "";
            int ghostType = (int)PlayerInventoryScript.AllGhosts[j]["Type"];

            switch (ghostType) {
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

            newGhost.transform.GetChild(1).GetComponent<Image>().color = ColorManager.GhostColors[ghostType];

            newGhost.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>().text = typeText;
            newGhost.transform.GetChild(2).GetComponent<Image>().color = ColorManager.GhostColors[ghostType];

            newGhost.transform.GetChild(3).GetComponent<TMP_Text>().text = (string)PlayerInventoryScript.AllGhosts[j]["Name"];

            newGhost.transform.GetChild(4).transform.GetChild(0).GetComponent<TMP_Text>().text = (int)PlayerInventoryScript.AllGhosts[j]["Level"] + "";

            newGhost.transform.GetChild(5).GetComponent<TMP_Text>().text = (int)PlayerInventoryScript.AllGhosts[j]["Chance"] + "%";
            newGhost.transform.GetChild(5).gameObject.SetActive(false);
        }
    }


    private void DisplayFilteredGhosts() {
        for (int k = 0; k < PlayerInventoryScript.GhostsInventory[currentIndex-1].Count; k++) {
            Image newGhost = Instantiate(ListedGhost, GhostsContainer.transform);
            displayedGhosts.Add(newGhost.gameObject);
            newGhost.transform.SetParent(GhostsContainer.transform);
            newGhost.transform.localPosition = new Vector2(
                0,
                basicYPos - (listItemHeight * k)
            );

            // Check for types and apply language
            string typeText = "";
            int ghostType = (int)PlayerInventoryScript.GhostsInventory[currentIndex-1][k]["Type"];

            switch (ghostType) {
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

            newGhost.transform.GetChild(1).GetComponent<Image>().color = ColorManager.GhostColors[ghostType];

            newGhost.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>().text = typeText;
            newGhost.transform.GetChild(2).GetComponent<Image>().color = ColorManager.GhostColors[ghostType];

            newGhost.transform.GetChild(3).GetComponent<TMP_Text>().text = (string)PlayerInventoryScript.GhostsInventory[currentIndex-1][k]["Name"];

            newGhost.transform.GetChild(4).transform.GetChild(0).GetComponent<TMP_Text>().text = (int)PlayerInventoryScript.GhostsInventory[currentIndex-1][k]["Level"] + "";

            newGhost.transform.GetChild(5).GetComponent<TMP_Text>().text = (int)PlayerInventoryScript.GhostsInventory[currentIndex-1][k]["Chance"] + "%";
            newGhost.transform.GetChild(5).gameObject.SetActive(false);
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
