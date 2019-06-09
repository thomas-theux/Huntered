using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHandler : MonoBehaviour {

    public int weaponID;
    public float lifetime;
    public float damage;

    private void Start() {
        Destroy(this.gameObject, 5);
    }


    private void OnTriggerEnter(Collider other) {
        print(other.name);

        if (other.tag != "Enemy" && other.tag != "Gold" && other.tag != "Attack" && other.tag != "Ranged" && other.tag != "EnemyAttack" && other.tag != "EnemyRanged" && other.tag != "Trigger") {

            if (other.tag == "Player") {
                // Deal damage
                other.GetComponent<PlayerSheet>().currentHealth -= damage;
            }

            if (this.gameObject.tag == "EnemyRanged") {
                Destroy(this.gameObject);
            }
        }
    }
}
