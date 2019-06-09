using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadius : MonoBehaviour {

    public EnemySheet enemySheetScript;
    public GameObject weaponParent;
    public GameObject attackSpawner;
    private GameObject enemyWeapon;

    private float attackCooldown;
    private float attackDelayTime;

    private float moveDelayTime = 0;


    private void Start() {
        // Set delay between attacks
        attackCooldown = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Cooldown"];

        // Set radius of aggro
        int reachScale = (int)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Reach Radius"];
        transform.localScale = new Vector3(reachScale, transform.localScale.y, reachScale);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            // attackDelayTime = attackCooldown;
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
            CastAttack();
        }

        // Cool down
        if (attackDelayTime > 0) {
            CoolDown();
        }

        // Delay movement
        if (enemySheetScript.DelayMovement) {
            DelayMovement();
        }
    }


    private void CastAttack() {
        if (attackDelayTime <= 0) {
            // Attack animation
            // [insert animation code]

            // Instantiate damage trigger
            StartCoroutine(DamageDelay());

            // Delay next attack
            attackDelayTime = attackCooldown;

            // Delay movement after an attack
            enemySheetScript.DelayMovement = true;
            moveDelayTime = GameSettings.MoveDelay + (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["CastTime"];
        }
    }


    private void CoolDown() {
        attackDelayTime -= Time.deltaTime;
    }


    private IEnumerator DamageDelay() {
        float delay = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["DamageDelay"];

        yield return new WaitForSeconds(delay);

        enemyWeapon = weaponParent.transform.GetChild(enemySheetScript.enemyClassID).gameObject;
        GameObject newAttack = Instantiate(enemyWeapon);

        newAttack.GetComponent<EnemyWeaponHandler>().lifetime = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Lifetime"];

        // Calculate damage depending on enemy level
        float baseDamage = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Damage"];
        newAttack.GetComponent<EnemyWeaponHandler>().damage = baseDamage + baseDamage * GameSettings.enemyDamageMultiplier * enemySheetScript.enemyLevel;

        newAttack.transform.parent = this.gameObject.transform;
        newAttack.transform.position = attackSpawner.transform.position;
        newAttack.transform.rotation = attackSpawner.transform.rotation;
    }


    private void DelayMovement() {
        if (moveDelayTime > 0) {
            moveDelayTime -= Time.deltaTime;
        } else {
            enemySheetScript.DelayMovement = false;
        }
    }

}
