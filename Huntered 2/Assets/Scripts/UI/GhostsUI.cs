using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class GhostsUI : MonoBehaviour {

    public PlayerInventory PlayerInventoryScript;
    public PlayerSheet PlayerSheetScript;
    public GameObject GhostsContainer;
    public Image ListedGhost;

    private List<GameObject> displayedGhosts = new List<GameObject>();

    private float itemDistance = 100;
    private int currentIndex = 0;
    public GameObject[] GhostFilters;
    
    // REWIRED
    private bool navigateLeft = false;
    private bool navigateRight = false;


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
    }


    private void Update() {
        GetInput();
        UpdateIndex();
    }


    private void GetInput() {
        navigateLeft = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Left");
        navigateRight = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("DPad Right");
    }


    private void UpdateIndex() {
        if (navigateLeft) {
            if (currentIndex > 0) {
                currentIndex--;
            }
        }

        if (navigateRight) {
            if (currentIndex < GhostFilters.Length - 1) {
                currentIndex++;
            }
        }
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

    }


    private void OnDisable() {
        // Remove all (listed) Ghost elements from list
        for (int i = 0; i < displayedGhosts.Count; i++) {
            Destroy(displayedGhosts[i]);
        }
    }

}
