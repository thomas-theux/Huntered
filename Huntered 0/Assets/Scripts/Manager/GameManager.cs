﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject devCam;
    public static List<GameObject> AllPlayers = new List<GameObject>();


    private void Awake() {
        // devCam = GameObject.Find("DevCam");
        // Destroy(devCam);

        this.gameObject.GetComponent<GamepadManager>().InitializeGamepads();
        this.gameObject.GetComponent<PlayerSpawner>().SpawnCharacters();
    }

}