using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
// using Rewired.ControllerExtensions;

public class CharacterSenses : MonoBehaviour {

    public GameObject enemyLocationGO;
    public GameObject sensesRadiusGO;

    private Player player;
    private int charID = 0;

    // private int motorIndex = 0;
    private float motorLevel = 1.0f;
    private float duration = 0.1f;

    private float vibrationFrequency = 0;
    private float vibrationDelay = 0;
    private float vibrationLevel = 0;

    private float angleLimit = 90.0f;

    private bool enemyDetected = false;
    private bool startedCounting = false;



    private void Awake() {
        charID = this.gameObject.transform.parent.GetComponent<CharacterMovement>().charID;
        player = ReInput.players.GetPlayer(charID);

        sensesRadiusGO = this.gameObject;
    }


    private void Update() {
        if (enemyDetected) {
            OutputVibration();
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Environment") {
            if (other.GetComponent<CharacterMovement>().charID != charID) {
                // print("ENTERED!");
                player.SetVibration(0, motorLevel, duration);
                // player.SetVibration(1, motorLevel, duration);
            }
        }
    }


    private void OnTriggerStay(Collider other) {
        if (other.tag != "Environment") {
            if (other.GetComponent<CharacterMovement>().charID != charID) {
                enemyDetected = true;

                // Calculate distance to enemy
                float distanceEnemy = Vector3.Distance(this.gameObject.transform.parent.transform.position, other.transform.position);
                // vibrationFrequency = distanceEnemy / 5.0f;
                float maxDistance = (sensesRadiusGO.transform.localScale.x / 2) - 1;
                vibrationLevel = 1 - (distanceEnemy / maxDistance);

                // Set enemy location indicator
                enemyLocationGO.transform.localPosition = new Vector3(
                    enemyLocationGO.transform.localPosition.x,
                    enemyLocationGO.transform.localPosition.y,
                    distanceEnemy
                );

                // Calculating the angle between enemy and line of sight
                Vector3 angleFrom = other.transform.position - transform.position;
                Vector3 angleTo = enemyLocationGO.transform.position - transform.position;
                float calculatedAngle = Vector3.Angle(angleFrom, angleTo);

                if (calculatedAngle < angleLimit) {
                    vibrationFrequency = calculatedAngle / angleLimit;
                    // vibrationLevel = 1 - (calculatedAngle / 100);
                } else {
                    vibrationFrequency = 2.0f;
                    // vibrationLevel = 0.2f;
                }
            }
        }
    }


    private void OnTriggerExit(Collider other) {
        if (other.tag != "Environment") {
            if (other.GetComponent<CharacterMovement>().charID != charID) {
                enemyDetected = false;
            }
        }
    }


    private void OutputVibration() {
        if (!startedCounting) {
            startedCounting = true;
            vibrationDelay = vibrationFrequency;
        }

        if (startedCounting) {
            vibrationDelay -= Time.deltaTime;

            if (vibrationDelay <= 0) {
                // print("BRRRTT!");
                player.SetVibration(0, vibrationLevel, duration);
                // player.SetVibration(1, vibrationLevel, duration);

                vibrationDelay = vibrationFrequency;
            }
        } 
    }

}
