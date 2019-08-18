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

    private int listItemHeight = 100;
    private int childrenCount = 0;

    private int currentIndex = 0;
    private int cursorIndex = 0;
    private int cursorPos = 0;
    private int minCursorIndex = 1;
    private int maxCursorIndex = 4;

    private float basicYPos = 10.0f;
    private int listPos = 0;

    private bool scrollIsDelayed = false;
    private float tDef = 0.15f;
    private float t;

    // public Image ButtonUpImage;
    // public Image ButtonDownImage;

    // REWIRED
    private bool interactBtn = false;
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

        InitializeGhosts();

        ResetGhostCursor();
        MoveCursor();
    }


    private void InitializeGhosts() {
        // if (currentIndex == 0) {
        //     DisplayAllGhosts();
        // } else {
        //     DisplayFilteredGhosts();
        // }
        DisplayAllGhosts();

        UpdateIconTransparency();
        AdjustScrollView();
    }


    private void Update() {
        GetInput();
        UpdateIndex();

        if (interactBtn) {
            if (PlayerSheetScript.linkingPhase == 0) {
                ReleaseGhost();
            }
        }

        if (!scrollIsDelayed) {
            ContentNavigation();
        }

        if (scrollIsDelayed) {
            ScrollDelay();
        }

        DisableScrollDelay();
    }


    private void GetInput() {
        if (PlayerSheetScript.LinkingMenuUI) {
            interactBtn = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("X");
        }

        if (PlayerSheetScript.linkingPhase < 3) {
            navigateLeft = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Left");
            navigateRight = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Right");

            scrollUp = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButton("DPad Up");
            scrollDown = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButton("DPad Down");
        }
    }


    private void ReleaseGhost() {
        int releaseeUID = (int)displayedGhosts[cursorIndex].GetComponent<GhostSheet>().GhostStats["UID"];
        int removeGhostIndex = 0;

        for (int i = 0; i < PlayerInventoryScript.AllGhosts.Count; i++) {
            int properGhostUID = (int)PlayerInventoryScript.AllGhosts[i]["UID"];

            if (properGhostUID == releaseeUID) {
                removeGhostIndex = i;
            }
        }

        PlayerSheetScript.currentGold += (int)PlayerInventoryScript.AllGhosts[removeGhostIndex]["Value"];
        PlayerInventoryScript.AllGhosts.RemoveAt(removeGhostIndex);

        RemoveListedGhosts();
        if (cursorIndex == PlayerInventoryScript.AllGhosts.Count) {
            if (cursorIndex > 0) {
                cursorIndex--;
            }
        }

        InitializeGhosts();
        MoveCursor();
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
        // if (currentIndex == 0) {
        //     DisplayAllGhosts();
        // } else {
        //     DisplayFilteredGhosts();
        // }
        DisplayAllGhosts();

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
                GhostFilters[i].color = ColorManager.ImageTransparent30;
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
    }


    private void ScrollContent(int scrollDirection) {
        ContentContainer.transform.localPosition = new Vector2(
            ContentContainer.transform.localPosition.x,
            ContentContainer.transform.localPosition.y + (listItemHeight * scrollDirection)
        );
    }


    private void AdjustScrollView() {
        childrenCount = displayedGhosts.Count;

        // Set the height of the container depending on the Ghost count
        float newHeight = childrenCount * listItemHeight;
        GhostsContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(100, newHeight);
    }


    private void DisplayAllGhosts() {
        // Reset list position index
        listPos = 0;

        // Instantiate Ghosts with proper position
        for (int j = 0; j < PlayerInventoryScript.AllGhosts.Count; j++) {

            int ghostType = (int)PlayerInventoryScript.AllGhosts[j]["Type"];

            if (currentIndex > 0) {
                if (ghostType == currentIndex-1) {
                    AddNewGhost(ghostType, j);
                }
            } else {
                AddNewGhost(ghostType, j);
            }
        }
    }


    private void AddNewGhost(int ghostType, int arrIndex) {
        Image newGhost = Instantiate(ListedGhost, GhostsContainer.transform);
        displayedGhosts.Add(newGhost.gameObject);
        newGhost.transform.SetParent(GhostsContainer.transform);
        newGhost.transform.localPosition = new Vector2(
            0,
            basicYPos - (listItemHeight * listPos)
        );

        newGhost.GetComponent<GhostSheet>().GhostStats = PlayerInventoryScript.AllGhosts[arrIndex];

        listPos++;
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
