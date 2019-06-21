using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    private float respawnTime;
    private float respawnTimeDef = 5.0f;


    private void Start() {
        respawnTime = respawnTimeDef;
    }


    private void Update() {
        respawnTime -= Time.deltaTime;
        print(respawnTime);

        if (respawnTime <= 0) {
            print("respawn");
        }
    }

}
