﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour {

    private PlayerSheet playerSheetScript;

    private float moveDelayTime = 0;
    private float attackDelayTime = 0;

    public GameObject playerUI;
    public GameObject statsUI;
    // public GameObject playerCam;

    public GameObject weaponParent;
    public GameObject attackSpawner;

    private GameObject playerWeapon;

    private Vector3 movement;

    // REWIRED
	private float moveHorizontal;
	private float moveVertical;
    private float rotateHorizontal;
    private float rotateVertical;
    private bool interactBtn;
    private bool menuBtn;
    private bool attackBtn;


    public void InitializeCharacter() {
        playerSheetScript = GetComponent<PlayerSheet>();

        // playerCam.GetComponent<CameraFollow>().cameraID = playerSheetScript.playerID;
        // playerCam.GetComponent<CameraFollow>().InitializeCamera();

        // playerUI.GetComponent<Canvas>().worldCamera = playerCam.GetComponent<Camera>();
        // playerUI.GetComponent<Canvas>().planeDistance = 1;

        // statsUI.GetComponent<Canvas>().worldCamera = playerCam.GetComponent<Camera>();
        // statsUI.GetComponent<Canvas>().planeDistance = 1;
        // statsUI.SetActive(false);
    }


    private void Update() {
        GetInput();

        if (menuBtn) {
            OpenStats();
        }

        if (attackBtn) {
            CastAttack();
        }

        if (attackDelayTime > 0) {
            DelayAttack();
        }

        if (playerSheetScript.DelayMovement) {
            DelayMovement();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void GetInput() {
        if (!playerSheetScript.StatsUIActive) {
            interactBtn = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButtonDown("R1");
            attackBtn = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButton("X");
        }

        menuBtn = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButtonDown("Triangle");
    }


    private void CastAttack() {
        // Delay movement after an attack
        playerSheetScript.DelayMovement = true;
        moveDelayTime = GameSettings.MoveDelay + (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["CastTime"];

        if (attackDelayTime <= 0) {
            // Delay next attack
            attackDelayTime = (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Cooldown"];

            // Attack animation
            // [insert animation code]

            // Instantiate damage trigger
            StartCoroutine(DamageDelay());
        }
    }


    private IEnumerator DamageDelay() {
        float delay = (float)GetComponent<PlayerSheet>().weaponDataDict[playerSheetScript.playerWeaponID]["DamageDelay"];

        yield return new WaitForSeconds(delay);

        // Get the weapon the player has selected
        playerWeapon = weaponParent.transform.GetChild(playerSheetScript.playerWeaponID).gameObject;

        GameObject newAttack = Instantiate(playerWeapon);
        newAttack.GetComponent<PlayerWeaponHandler>().lifetime = (float)GetComponent<PlayerSheet>().weaponDataDict[playerSheetScript.playerWeaponID]["Lifetime"];
        newAttack.GetComponent<PlayerWeaponHandler>().damage = (float)GetComponent<PlayerSheet>().weaponDataDict[playerSheetScript.playerWeaponID]["Damage"];
        newAttack.transform.parent = this.gameObject.transform;
        newAttack.transform.position = attackSpawner.transform.position;
        newAttack.transform.rotation = attackSpawner.transform.rotation;
    }


    private void DelayMovement() {
        if (moveDelayTime > 0) {
            moveDelayTime -= Time.deltaTime;
        } else {
            playerSheetScript.DelayMovement = false;
        }
    }


    private void DelayAttack() {
        attackDelayTime -= Time.deltaTime;
    }


    private void OpenStats() {
        playerSheetScript.StatsUIActive = !playerSheetScript.StatsUIActive;
        statsUI.SetActive(playerSheetScript.StatsUIActive);
    }

}