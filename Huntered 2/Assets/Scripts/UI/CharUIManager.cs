using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharUIManager : MonoBehaviour {

    public PlayerSheet PlayerSheetScript;

    public GameObject[] CharMenuInterfaces;
    private int currentIndex = 0;


    // REWIRED
    private bool navigateLeft = false;
    private bool navigateRight = false;


    private void OnEnable() {
        DisplayUI();
    }


    private void DisplayUI() {
        for (int i = 0; i < CharMenuInterfaces.Length; i++) { CharMenuInterfaces[i].SetActive(false); }
        CharMenuInterfaces[currentIndex].SetActive(true);
    }


    private void Update() {
        GetInput();
    }


    private void GetInput() {
        navigateLeft = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("L1");
        navigateRight = ReInput.players.GetPlayer(PlayerSheetScript.playerID).GetButtonDown("R1");
    }

}
