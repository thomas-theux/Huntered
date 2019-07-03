using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerWeaponHandler : MonoBehaviour {

    public int weaponID;
    public int casterID;
    public float lifetime;
    public float damage;
    public float critChance;
    public float critDamage;

    private bool didCrit = false;

    private float damageRandomizer = 0.10f;

    public GameObject AttackCastGO;
    public GameObject AttackImpactGO;
    public Canvas DamageTextCanvas;
    private TMP_Text damageText;

    float rndPitch;


    private void Start() {
        Destroy(this.gameObject, lifetime);

        rndPitch = Random.Range(0.8f, 1.2f);

        GameObject castAttackSound = AttackCastGO.transform.GetChild(weaponID).gameObject;
        castAttackSound.GetComponent<AudioSource>().pitch = rndPitch;
        Instantiate(castAttackSound);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player" && other.tag != "Gold" && other.tag != "Attack" && other.tag != "Ranged" && other.tag != "EnemyAttack" && other.tag != "EnemyRanged" && other.tag != "Trigger" && other.tag != "CollectRadius" && other.tag != "Ghost") {

            // Randomize damage
            float dmgMin = damage - damage * damageRandomizer;
            float dmgMax = damage + damage * damageRandomizer;

            float rndDmg = Random.Range(dmgMin, dmgMax);

            // Check for crit chance and apply crit damage
            float rndCrit = Random.Range(0, 100);
            if (rndCrit < critChance) {
                rndDmg *= critDamage;
                didCrit = true;
            }

            rndDmg = Mathf.Round(rndDmg);

            if (other.tag == "Enemy") {
                // Spawn damage text above enemy's head
                SpawnDamageText(other, rndDmg);

                // Tell enemy who's attacking
                GameObject aggroRadius = other.transform.Find("Aggro Radius").gameObject;
                Collider attackingPlayer = GameManager.AllPlayers[casterID].GetComponent<Collider>();
                aggroRadius.GetComponent<EnemyController>().playerTargets.Add(attackingPlayer);

                // Deal damage
                other.GetComponent<EnemyLifeHandler>().currentHealth -= rndDmg;
            } else if (other.tag == "NPC") {
                // Spawn damage text above enemy's head
                SpawnDamageText(other, rndDmg);

                other.GetComponent<NPCLifeHandler>().currentHealth -= rndDmg;
            }

            if (this.gameObject.tag == "Ranged") {
                Destroy(this.gameObject);
            }
        }
    }


    private void OnDestroy() {
        GameObject castImpactSound = AttackImpactGO.transform.GetChild(weaponID).gameObject;
        castImpactSound.GetComponent<AudioSource>().pitch = rndPitch;
        Instantiate(castImpactSound);
    }


    private void SpawnDamageText(Collider other, float rndDmg) {
        Canvas newDamageText = Instantiate(DamageTextCanvas);
        newDamageText.transform.SetParent(other.transform);
        newDamageText.transform.position = other.transform.position;
        newDamageText.transform.GetChild(0).GetComponent<TMP_Text>().text = rndDmg + "";

        // Apply different text color for crit hits
        if (didCrit) {
            newDamageText.transform.GetChild(0).GetComponent<TMP_Text>().color = ColorManager.KeyGold50;
        }
    }

}
