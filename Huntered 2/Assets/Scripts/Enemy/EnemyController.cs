using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject enemyGO;
    private EnemySheet enemySheetScript;
    private UnityEngine.AI.NavMeshAgent enemyAgent;

    public GameObject attackSpawner;
    public GameObject weaponParent;
    private GameObject enemyWeapon;

    private List<Collider> playerTargets = new List<Collider>();

    private float attackRadius;
    private float attackCooldown;
    private float attackDelayTime;
    private float moveDelayTime = 0;


    private void Start() {
        enemyAgent = enemyGO.GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemySheetScript = enemyGO.GetComponent<EnemySheet>();

        // Set radius of aggro
        float aggroScale = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Aggro Radius"];
        transform.localScale = new Vector3(aggroScale, transform.localScale.y, aggroScale);

        attackRadius = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Attack Radius"] / 2;
        attackCooldown = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Cooldown"];
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            playerTargets.Add(other);
        }
    }


    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            RemovePlayer(other);
        }
    }


    private void Update() {
        if (playerTargets.Count > 0) {
            float distanceToPlayer = Vector3.Distance(enemyGO.transform.position, playerTargets[0].transform.position);

            if (distanceToPlayer > attackRadius) {
                enemyAgent.destination = playerTargets[0].transform.position;
            } else {
                enemyAgent.destination = this.transform.position;
                enemyGO.transform.LookAt(playerTargets[0].transform, transform.up);

                // Attack player
                CastAttack();

                // Cool down
                if (attackDelayTime > 0) {
                    CoolDown();
                }

                // Delay movement
                if (enemySheetScript.DelayMovement) {
                    DelayMovement();
                }
            }
        } else {
            enemyAgent.destination = this.transform.position;
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
            moveDelayTime = GameSettings.MoveDelay + (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Cast Time"];
        }
    }


    private void CoolDown() {
        attackDelayTime -= Time.deltaTime;
    }


    private IEnumerator DamageDelay() {
        float delay = (float)enemySheetScript.classDataDict[enemySheetScript.enemyClassID]["Damage Delay"];

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


    // Remove the player that left the aggro trigger
    public void RemovePlayer(Collider other) {
        playerTargets.Remove(other);
    }

}