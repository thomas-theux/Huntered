using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroRadius : MonoBehaviour {

    public GameObject enemyGO;
    public EnemySheet enemySheetScript;

    // private Rigidbody rb;
    private Collider otherCollider;
    private UnityEngine.AI.NavMeshAgent playerTarget;

    private float approachSpeed;

    private List<Collider> targets = new List<Collider>();


    private void Start() {
        // rb = enemyGO.GetComponent<Rigidbody>();
        playerTarget = enemyGO.GetComponent<UnityEngine.AI.NavMeshAgent>();

        float calculatedSpeed = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Move Speed"];
        approachSpeed = calculatedSpeed + calculatedSpeed * GameSettings.enemySpeedMultiplier * enemySheetScript.enemyLevel;

        // Set radius of aggro
        int aggroScale = (int)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Aggro Radius"];
        transform.localScale = new Vector3(aggroScale, transform.localScale.y, aggroScale);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            // Activate aggro trigger
            enemySheetScript.actionMode = 1;

            targets.Add(other);
        }
    }


    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            if (enemySheetScript.actionMode == 1) {
                playerTarget.destination = targets[0].transform.position;
            } else {
                playerTarget.destination = this.transform.position;
                enemyGO.transform.LookAt(targets[0].transform, transform.up);
            }
        }
    }


    private void OnTriggerExit(Collider other) {
        // Deactivate aggro trigger
        if (other.tag == "Player") {
            // Remove the player that left the aggro trigger
            targets.Remove(other);

            if (targets.Count == 0) {
                enemySheetScript.actionMode = 0;
                playerTarget.destination = this.transform.position;
            }
        }
    }

}
