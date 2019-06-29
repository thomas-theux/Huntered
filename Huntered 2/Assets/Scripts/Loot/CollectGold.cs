using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGold : MonoBehaviour {

    private bool playerDetected = false;
    private Transform collectingPlayer;
    private float collectSpeed = 5.0f;

    public GameObject CollectGoldSound;


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            // Formula for gold gain
            int addGold = Mathf.RoundToInt(GameSettings.baseGoldGain + (ReputationManager.currentRepLevel * GameSettings.goldMultiplier));
            other.GetComponent<PlayerSheet>().currentGold += addGold;

            float rndPitch = Random.Range(0.95f, 1.0f);
            rndPitch = 1.0f;
            CollectGoldSound.GetComponent<AudioSource>().pitch = rndPitch;
            Instantiate(CollectGoldSound);

            Destroy(this.gameObject);
        }
    }


    public void SetPlayerTarget(Collider targetPlayer) {
        if (!playerDetected) {
            collectingPlayer = targetPlayer.transform;
            playerDetected = true;
        }
    }


    private void FixedUpdate() {
        if (playerDetected) {
            Vector3 desiredPos = collectingPlayer.transform.position;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, collectSpeed * Time.deltaTime);
            collectSpeed *= 1.2f;

            transform.position = smoothedPos;
        }
    }

}
