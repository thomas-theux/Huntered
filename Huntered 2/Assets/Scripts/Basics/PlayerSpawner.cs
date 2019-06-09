using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    private Transform spawnPos;
    public GameObject PlayerGO;

    private Vector3 playerDistances = new Vector3(3, 0, 1);


    public void SpawnCharacters() {
        spawnPos = GameObject.Find("SpawnPos").transform;

        for (int i = 0; i < GameSettings.PlayerCount; i++) {
            GameObject newPlayer = Instantiate(PlayerGO);
            newPlayer.transform.position = playerDistances * i;

            newPlayer.GetComponent<PlayerSheet>().playerID = i;
            newPlayer.GetComponent<PlayerController>().InitializeCharacter();
        }
    }

}
