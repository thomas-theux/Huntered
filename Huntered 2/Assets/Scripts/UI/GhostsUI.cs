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
        }
    }

}
