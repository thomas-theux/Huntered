using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroRadius : MonoBehaviour {

    public GameObject enemyGO;
    public EnemySheet enemySheetScript;

    private Rigidbody rb;
    private Collider otherCollider;

    private float approachSpeed;


    private void Start() {
        rb = enemyGO.GetComponent<Rigidbody>();
        approachSpeed = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Move Speed"];

        // Set radius of aggro
        int aggroScale = (int)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Aggro Radius"];
        transform.localScale = new Vector3(aggroScale, transform.localScale.y, aggroScale);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            // Activate aggro trigger
            enemySheetScript.actionMode = 1;
            otherCollider = other;

            // Unlock y-rotation when player is close
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }


    private void OnTriggerExit(Collider other) {
        // Deactivate aggro trigger
        if (other.tag == "Player") {
            enemySheetScript.actionMode = 0;
        }

        // Lock overall rotation when player is out of sight
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }


    private void Update() {
        if (enemySheetScript.actionMode > 0) {
            // Approach player only if the enemy is not a ranged class
            if (enemySheetScript.enemyClassID < 2) {
                // Move enemy towards player
                enemyGO.transform.position = Vector3.MoveTowards(enemyGO.transform.position, otherCollider.transform.position, approachSpeed * Time.deltaTime);
                enemyGO.transform.LookAt(otherCollider.transform);
            }
        }
    }

}
