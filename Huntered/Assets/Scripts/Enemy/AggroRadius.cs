using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroRadius : MonoBehaviour {

    public GameObject enemyGO;
    public EnemySheet enemySheetScript;
    public EnemyController enemyControllerScript;

    private Rigidbody rb;

    private float approachSpeed;
    private float minDistance = 2.0f;


    private void Awake() {
        rb = enemyGO.GetComponent<Rigidbody>();
        approachSpeed = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Move Speed"];

        // Set radius of aggro
        int aggroScale = (int)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Aggro Radius"];
        transform.localScale = new Vector3(aggroScale, transform.localScale.y, aggroScale);
    }


    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            // Unlock y-rotation when player is close
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            // Approach player only if the enemy is not a ranged class
            if (enemySheetScript.enemyClassID < 2) {
                if (Vector3.Distance(enemyGO.transform.position, other.transform.position) > minDistance) {
                    // Stop attacking if player is too far away
                    if (enemyControllerScript.playerIsClose) {
                        enemyControllerScript.playerIsClose = false;
                        enemyControllerScript.allowedToAttack = false;
                    }

                    // Lerp movement towards player
                    Vector3 desiredPos = new Vector3(other.transform.position.x,  enemyGO.transform.position.y, other.transform.position.z);
                    Vector3 smoothedPos = Vector3.Lerp(enemyGO.transform.position, desiredPos, approachSpeed * Time.deltaTime);

                    enemyGO.transform.position = smoothedPos;
                } else {
                    enemyControllerScript.playerIsClose = true;
                }
            } else {
                enemyControllerScript.playerIsClose = true;
            }

            enemyGO.transform.LookAt(other.transform);
        }
    }


    private void OnTriggerExit(Collider other) {
        // Lock overall rotation when player is out of sight
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        // Stop attacking if player is too far away
        enemyControllerScript.playerIsClose = false;
        enemyControllerScript.allowedToAttack = false;
    }

}
