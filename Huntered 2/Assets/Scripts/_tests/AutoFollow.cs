using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFollow : MonoBehaviour {

    private PlayerSheet playerSheetScript;
    private GameObject otherPlayerGO;
    private UnityEngine.AI.NavMeshAgent playerAgent;

    private float minDistance = 3.0f;


    private void Start() {
        playerSheetScript = this.GetComponent<PlayerSheet>();
        playerAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

        playerAgent.enabled = false;

        // Set ID this player should walk to when idle
        int otherPlayerID = 0;
        if (playerSheetScript.playerID == 0) { otherPlayerID = 1; }
        otherPlayerGO = GameManager.AllPlayers[otherPlayerID];
    }


    private void Update() {
        if (playerSheetScript.isIdle) {
            playerAgent.enabled = true;

            float distanceToPlayer = Vector3.Distance(this.transform.position, otherPlayerGO.transform.position);

            if (distanceToPlayer > minDistance) {
                playerAgent.destination = otherPlayerGO.transform.position;
            } else {
                playerAgent.destination = this.transform.position;
            }
        }
    }

}
