using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour {

    public int weaponID;
    public float lifetime;
    public float damage;

    public GameObject AttackCastGO;
    public GameObject AttackImpactGO;

    float rndPitch;


    private void Start() {
        Destroy(this.gameObject, lifetime);

        rndPitch = Random.Range(0.8f, 1.2f);

        GameObject castAttackSound = AttackCastGO.transform.GetChild(weaponID).gameObject;
        castAttackSound.GetComponent<AudioSource>().pitch = rndPitch;
        Instantiate(castAttackSound);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player" && other.tag != "Gold" && other.tag != "Attack" && other.tag != "Ranged" && other.tag != "EnemyAttack" && other.tag != "EnemyRanged" && other.tag != "Trigger" && other.tag != "CollectRadius") {

            if (other.tag == "Enemy") {
                // Deal damage
                other.GetComponent<EnemyLifeHandler>().currentHealth -= damage;
            } else if (other.tag == "NPC") {
                other.GetComponent<NPCLifeHandler>().currentHealth -= damage;
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

}
