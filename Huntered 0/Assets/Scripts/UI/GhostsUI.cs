using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostsUI : MonoBehaviour {

    public PlayerInventory PlayerInventoryScript;
    public GameObject GhostsContainer;
    public Image ListedGhost;

    private float itemDistance = 100;


    private void OnEnable() {
        int overallGhosts = 0;

        // Go through all dictionaries and display the Ghosts in a list
        for (int i = 0; i < 4; i++) {
            overallGhosts += PlayerInventoryScript.AllGhosts[i].Count;
        }

        for (int j = 0; j < overallGhosts; j++) {
            Image newGhost = Instantiate(ListedGhost, GhostsContainer.transform);
            newGhost.transform.SetParent(GhostsContainer.transform);
            newGhost.transform.localPosition = new Vector2(
                0,
                0 - (itemDistance * j)
            );
<<<<<<< HEAD
=======

            newGhost.transform.GetChild(2).GetComponent<TMP_Text>().text = (int)PlayerInventoryScript.GhostsInventory[j]["Type"] + "";

            // Add texts to array that then can be accessed to display the Ghost's data
            // foreach (Hashtable child in PlayerInventoryScript.GhostsInventory) {   
            //     if (child.GetComponent<TMP_Text>()) {
            //         ListedGhostTexts.Add(child.GetComponent<TMP_Text>());
            //     }
            // }
>>>>>>> parent of fb2e8d5... ms
        }
    }

}
