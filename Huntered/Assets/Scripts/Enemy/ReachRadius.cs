using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachRadius : MonoBehaviour {

    public EnemySheet enemySheetScript;
    public GameObject weaponParent;
    public GameObject attackSpawner;
    private GameObject enemyWeapon;

    private float attackCooldown;
    private float attackDelay;


    private void Start() {
        // Set delay between attacks
        attackCooldown = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Cooldown"];

        // Set radius of aggro
        int reachScale = (int)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Reach Radius"];
        transform.localScale = new Vector3(reachScale, transform.localScale.y, reachScale);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            attackDelay = attackCooldown;
            enemySheetScript.actionMode = 2;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            enemySheetScript.actionMode = 1;
        }
    }


    private void Update() {
        // Attack player
        if (enemySheetScript.actionMode > 1) {
            attackDelay -= Time.deltaTime;

            if (attackDelay <= 0) {
                enemyWeapon = weaponParent.transform.GetChild(enemySheetScript.enemyClassID).gameObject;
                GameObject newAttack = Instantiate(enemyWeapon);

                newAttack.GetComponent<EnemyWeaponHandler>().lifetime = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Lifetime"];

                // Calculate damage depending on enemy level
                float baseDamage = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Damage"];
                newAttack.GetComponent<EnemyWeaponHandler>().damage = baseDamage + baseDamage * GameSettings.enemyDamageMultiplier * enemySheetScript.enemyLevel;

                newAttack.transform.parent = this.gameObject.transform;
                newAttack.transform.position = attackSpawner.transform.position;
                newAttack.transform.rotation = attackSpawner.transform.rotation;

                attackDelay = attackCooldown;
            }
        }
    }

}
