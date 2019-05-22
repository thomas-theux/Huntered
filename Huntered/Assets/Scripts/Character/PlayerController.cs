using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour {

    private PlayerSheet playerSheetScript;
	private CharacterController cc;

    public GameObject statsUI;
    private bool statsUIActive = false;

    public GameObject weaponParent;
    public GameObject attackSpawner;
    private GameObject playerWeapon;

    private bool isAttacking = false;

    private Vector3 movement;

    // REWIRED
	private float moveHorizontal;
	private float moveVertical;
    private float rotateHorizontal;
    private float rotateVertical;
    private bool interactBtn;
    private bool menuBtn;
    private bool attackBtn;


    private void Awake() {
        cc = this.gameObject.GetComponent<CharacterController>();

        playerSheetScript = GetComponent<PlayerSheet>();
    }


    private void Update() {
        GetInput();

        if (menuBtn) {
            OpenStats();
        }

        if (attackBtn && !isAttacking) {
            CastAttack();
        }
    }


    private void FixedUpdate() {
        MoveCharacter();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void GetInput() {
        if (!statsUIActive) {
            moveHorizontal = ReInput.players.GetPlayer(playerSheetScript.playerID).GetAxis("LS Horizontal");
            moveVertical = ReInput.players.GetPlayer(playerSheetScript.playerID).GetAxis("LS Vertical");

            rotateHorizontal = ReInput.players.GetPlayer(playerSheetScript.playerID).GetAxis("RS Horizontal");
            rotateVertical = ReInput.players.GetPlayer(playerSheetScript.playerID).GetAxis("RS Vertical");

            interactBtn = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButtonDown("X");
            attackBtn = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButton("R1");
        }

        menuBtn = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButtonDown("Triangle");
    }


    private void MoveCharacter() {
        // Get movement input
        movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Normalize diagonal movement
        movement = Vector3.ClampMagnitude(movement, 1);

        // Move character
        cc.Move(movement * playerSheetScript.moveSpeed * Time.deltaTime);

        // Rotate character in the direction of movement
        if (movement != Vector3.zero) {
            transform.forward = movement;
        }

        // Rotate the player with the right stick
        Vector3 lookDirection = new Vector3(rotateHorizontal, 0, rotateVertical);
        transform.LookAt(transform.position + lookDirection);
    }


    private void CastAttack() {
        isAttacking = true;

        // Get the weapon the player has selected
        playerWeapon = weaponParent.transform.GetChild(playerSheetScript.playerWeaponID).gameObject;

        GameObject newAttack = Instantiate(playerWeapon);
        newAttack.GetComponent<PlayerWeaponHandler>().lifetime = (float)GetComponent<PlayerSheet>().weaponDataDict[playerSheetScript.playerWeaponID]["Lifetime"];
        newAttack.GetComponent<PlayerWeaponHandler>().damage = (float)GetComponent<PlayerSheet>().weaponDataDict[playerSheetScript.playerWeaponID]["Damage"];
        newAttack.transform.parent = this.gameObject.transform;
        newAttack.transform.position = attackSpawner.transform.position;
        newAttack.transform.rotation = attackSpawner.transform.rotation;

        StartCoroutine(AttackDelay());
    }


    private IEnumerator AttackDelay() {
        float cooldownTime = (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Cooldown"];
        yield return new WaitForSeconds(cooldownTime);

        isAttacking = false;
    }


    private void OpenStats() {
        statsUIActive = !statsUIActive;
        statsUI.SetActive(statsUIActive);
    }

}
