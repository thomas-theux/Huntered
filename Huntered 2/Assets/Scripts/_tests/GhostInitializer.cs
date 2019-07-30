using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInitializer : MonoBehaviour {

    private CollectGhosts collectGhostsScript;

    public int GhostSlot;

    private void Start() {
        collectGhostsScript = GetComponent<CollectGhosts>();
        collectGhostsScript.InitializeGhost();
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerSheet>().SlottedGhostsHead[GhostSlot] = collectGhostsScript.GhostData;
        }
    }

}
