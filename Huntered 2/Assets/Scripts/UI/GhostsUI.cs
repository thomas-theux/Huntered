using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GhostsUI : MonoBehaviour {

    public PlayerInventory PlayerInventoryScript;
    public GameObject GhostsContainer;
    public Image ListedGhost;

    private List<TMP_Text> ListedGhostTexts = new List<TMP_Text>();

    private float itemDistance = 100;


    private void OnEnable() {
        // Liste mit allen Ghosts löschen
        PlayerInventoryScript.GhostsInventory.Clear();

        // Go through all dictionaries and write the Ghosts in a master list
        for (int i = 0; i < 4; i++) {
            foreach (Hashtable child in PlayerInventoryScript.AllGhosts[i]) {
                PlayerInventoryScript.GhostsInventory.Add(child);
            }
        }

        // Instantiate Ghosts with proper position
        for (int j = 0; j < PlayerInventoryScript.GhostsInventory.Count; j++) {
            Image newGhost = Instantiate(ListedGhost, GhostsContainer.transform);
            newGhost.transform.SetParent(GhostsContainer.transform);
            newGhost.transform.localPosition = new Vector2(
                0,
                0 - (itemDistance * j)
            );

            newGhost.transform.GetChild(2).GetComponent<TMP_Text>().text = (int)PlayerInventoryScript.GhostsInventory[j]["Type"] + "";

            // Add texts to array that then can be accessed to display the Ghost's data
            // foreach (Hashtable child in PlayerInventoryScript.GhostsInventory) {   
            //     if (child.GetComponent<TMP_Text>()) {
            //         ListedGhostTexts.Add(child.GetComponent<TMP_Text>());
            //     }
            // }
        }
    }

}
