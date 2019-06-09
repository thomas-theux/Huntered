using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private EnemySheet enemySheetScript;

    public GameObject weaponParent;
    public GameObject attackSpawner;
    private GameObject enemyWeapon;

    public bool playerIsClose = false;
    public bool allowedToAttack = false;

    public float attackDelay;
    private float attackCooldown;


    private void Start() {
        enemySheetScript = GetComponent<EnemySheet>();

        attackCooldown = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Cooldown"];
    }


    private void Update() {
        if (playerIsClose && !allowedToAttack) {
            allowedToAttack = true;
            AttackDelay();
            attackDelay = attackCooldown;
        }

        if (allowedToAttack) {
            AttackDelay();
        }
    }


    private void AttackDelay() {
        // Attack player
        if (attackDelay > 0) {
            attackDelay -= Time.deltaTime;
        } else {
            AttackPlayer();
            attackDelay = attackCooldown;
        }
    }


    private void AttackPlayer() {
        enemyWeapon = weaponParent.transform.GetChild(enemySheetScript.enemyClassID).gameObject;
        GameObject newAttack = Instantiate(enemyWeapon);

        newAttack.GetComponent<EnemyWeaponHandler>().lifetime = (float)GetComponent<EnemySheet>().classDataDict[enemySheetScript.enemyClassID]["Lifetime"];
        newAttack.GetComponent<EnemyWeaponHandler>().damage = (float)GetComponent<EnemySheet>().classDataDict[enemySheetScript.enemyClassID]["Damage"];
        newAttack.transform.parent = this.gameObject.transform;
        newAttack.transform.position = attackSpawner.transform.position;
        newAttack.transform.rotation = attackSpawner.transform.rotation;
    }

}
