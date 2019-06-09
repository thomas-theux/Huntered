using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRadius : MonoBehaviour {

    private bool playerDetected = false;
    private Transform collectingPlayer;
    private float collectSpeed = 5.0f;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            collectingPlayer = other.transform;
            playerDetected = true;
        }
    }


    private void FixedUpdate() {
        if (playerDetected) {
            Vector3 desiredPos = new Vector3(collectingPlayer.transform.position.x,  transform.parent.position.y, collectingPlayer.transform.position.z);
            Vector3 smoothedPos = Vector3.Lerp(transform.parent.position, desiredPos, collectSpeed * Time.deltaTime);
            collectSpeed *= 1.2f;

            transform.parent.position = smoothedPos;
        }
    }

}
