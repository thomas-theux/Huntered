using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHandler : MonoBehaviour {

    public int weaponID;
    public float lifetime;
    public float damage;

    private float damageRandomizer = 0.10f;

    private void Start() {
        Destroy(this.gameObject, lifetime);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Enemy" && other.tag != "Gold" && other.tag != "Attack" && other.tag != "Ranged" && other.tag != "EnemyAttack" && other.tag != "EnemyRanged" && other.tag != "Trigger" && other.tag != "CollectRadius" && other.tag != "Ghost") {

            // Randomize damage
            float dmgMin = damage - damage * damageRandomizer;
            float dmgMax = damage + damage * damageRandomizer;

            float rndDmg = Random.Range(dmgMin, dmgMax);
            rndDmg = Mathf.Round(rndDmg);

            if (other.tag == "Player") {
                // Deal damage
                other.GetComponent<PlayerSheet>().currentHealth -= rndDmg;
            } else if (other.tag == "NPC") {
                other.GetComponent<NPCLifeHandler>().currentHealth -= rndDmg;
            }

            if (this.gameObject.tag == "EnemyRanged") {
                Destroy(this.gameObject);
            }
        }
    }
}
