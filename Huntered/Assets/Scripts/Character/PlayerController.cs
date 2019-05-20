using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour {

    private PlayerSheet playerSheetScript;
	private CharacterController cc;

    public int charID;
    private int playerWeaponID = 0;

    public GameObject weaponParent;
    public GameObject attackSpawner;
    private GameObject playerWeapon;

    private bool isAttacking = false;

    private Vector3 movement;
    // private float moveSpeed = 2.0f;
    private float moveSpeed = 10.0f;


    // REWIRED
	private float moveHorizontal;
	private float moveVertical;
    private float rotateHorizontal;
    private float rotateVertical;
    private bool interactBtn;


    private void Awake() {
        cc = this.gameObject.GetComponent<CharacterController>();

        playerSheetScript = GetComponent<PlayerSheet>();
    }


    private void Update() {
        GetInput();

        if (interactBtn && !isAttacking) {
            CastAttack();
        }
    }


    private void FixedUpdate() {
        MoveCharacter();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void GetInput() {
        moveHorizontal = ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal");
		moveVertical = ReInput.players.GetPlayer(charID).GetAxis("LS Vertical");

        rotateHorizontal = ReInput.players.GetPlayer(charID).GetAxis("RS Horizontal");
        rotateVertical = ReInput.players.GetPlayer(charID).GetAxis("RS Vertical");

        interactBtn = ReInput.players.GetPlayer(charID).GetButton("X");
    }


    private void MoveCharacter() {
        // Get movement input
        movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Normalize diagonal movement
        movement = Vector3.ClampMagnitude(movement, 1);

        // Move character
        cc.Move(movement * moveSpeed * Time.deltaTime);

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
        playerWeaponID = playerSheetScript.playerWeaponID;
        playerWeapon = weaponParent.transform.GetChild(playerWeaponID).gameObject;

        GameObject newAttack = Instantiate(playerWeapon);
        newAttack.GetComponent<WeaponHandler>().lifetime = (float)GetComponent<PlayerSheet>().weaponDataDict[playerWeaponID]["Lifetime"];
        newAttack.GetComponent<WeaponHandler>().damage = (float)GetComponent<PlayerSheet>().weaponDataDict[playerWeaponID]["Damage"];
        newAttack.transform.parent = this.gameObject.transform;
        newAttack.transform.position = attackSpawner.transform.position;
        newAttack.transform.rotation = attackSpawner.transform.rotation;

        StartCoroutine(AttackDelay());
    }


    private IEnumerator AttackDelay() {
        float cooldownTime = (float)playerSheetScript.weaponDataDict[playerWeaponID]["Cooldown"];
        yield return new WaitForSeconds(cooldownTime);

        isAttacking = false;
    }

}
