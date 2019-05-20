using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour {

    public int weaponID;
    public float lifetime;
    public float damage;

    private void Start() {
        Destroy(this.gameObject, lifetime);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player" && other.tag != "Gold" && other.tag != "Attack") {
            
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

}
