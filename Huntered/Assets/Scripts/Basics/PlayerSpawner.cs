using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    private GameObject spawnPos;
    public GameObject PlayerGO;


    // private void Awake() {
    //     spawnPos = GameObject.Find("SpawnPos");

    //     for (int i = 0; i < GameSettings.PlayerCount; i++) {
    //         GameObject newPlayer = Instantiate(PlayerGO);

    //         newPlayer.GetComponent<PlayerSheet>().playerID = i;

    //         newPlayer.transform.position = new Vector3(
    //             i * 2,
    //             spawnPos.transform.position.y,
    //             spawnPos.transform.position.z
    //         );

    //         float camPosX = 0.5f * i;
    //         newPlayer.GetComponent<PlayerController>().PlayerCam.rect = new Rect(camPosX, 0, 0.5f, 1.0f);

    //     }
    // }

}
