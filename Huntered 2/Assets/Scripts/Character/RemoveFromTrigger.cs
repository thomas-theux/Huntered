using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFromTrigger : MonoBehaviour {

    private PlayerSheet playerSheetScript;
    private List<GameObject> triggerList = new List<GameObject>();
    // private bool toldCollider = false;


    private void Start() {
        playerSheetScript = GetComponent<PlayerSheet>();
    }


    public void TellEnemiesToRemove() {
        for (int i = 0; i < triggerList.Count; i++) {
            triggerList[i].transform.GetChild(3).GetComponent<EnemyController>().RemovePlayer(this.GetComponent<Collider>());
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Trigger") {
            if (!triggerList.Contains(other.transform.parent.gameObject)) {
                triggerList.Add(other.transform.parent.gameObject);
            }
        }
    }


    private void OnTriggerExit(Collider other) {
        if (other.tag == "Trigger") {
            if (triggerList.Contains(other.transform.parent.gameObject)) {
                triggerList.Remove(other.transform.parent.gameObject);
            }
        }
    }

}
