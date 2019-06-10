using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject devCam;


    private void Awake() {
        // devCam = GameObject.Find("DevCam");
        // Destroy(devCam);

        this.gameObject.GetComponent<GamepadManager>().InitializeGamepads();
        this.gameObject.GetComponent<PlayerSpawner>().SpawnCharacters();
    }

}
