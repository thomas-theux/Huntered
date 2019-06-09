using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    private Transform spawnPos;
    public GameObject PlayerGO;


    private void Awake() {
        spawnPos = GameObject.Find("SpawnPos").transform;

        for (int i = 0; i < GameSettings.PlayerCount; i++) {
            GameObject newPlayer = Instantiate(PlayerGO);
        }
    }

}
