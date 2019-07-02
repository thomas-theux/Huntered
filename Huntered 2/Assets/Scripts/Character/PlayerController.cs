using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour {

    private PlayerSheet playerSheetScript;
    private SkillSheet skillSheetScript;
    private AudioManager audioManagerScript;

    public GameObject DetectionArea;

    private float moveDelayTime = 0;
    private float attackDelayTime = 0;

    public GameObject playerUI;
    public GameObject CharUI;
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
    private bool potionBtn;


    public void InitializeCharacter() {
        playerSheetScript = GetComponent<PlayerSheet>();
        skillSheetScript = GetComponent<SkillSheet>();
        audioManagerScript = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }


    private void Update() {
        GetInput();

        if (menuBtn) {
            OpenCharMenu();
        }

        if (attackBtn) {
            CastAttack();
        }

        // Detect if player is attacking
        if (attackBtn) {
            if (!playerSheetScript.isAttacking) { playerSheetScript.isAttacking = true; }
        } else {
            if (playerSheetScript.isAttacking) { playerSheetScript.isAttacking = false; }
        }

        if (potionBtn) {
            if (playerSheetScript.currentHealth != playerSheetScript.maxHealth) {
                if (!playerSheetScript.PotionCooldownActive) {
                    HealCharacter();
                }
            }
        }

        if (playerSheetScript.PotionCooldownActive) {
            PotionCooldown();
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
        if (!playerSheetScript.CharMenuUI) {
            interactBtn = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButtonDown("R1");
            attackBtn = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButton("X");
            potionBtn = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButtonDown("L1");
        }

        menuBtn = ReInput.players.GetPlayer(playerSheetScript.playerID).GetButtonDown("Triangle");
    }


    private void CastAttack() {
        // Delay movement after an attack
        playerSheetScript.DelayMovement = true;
        moveDelayTime = GameSettings.MoveDelay + (float)playerSheetScript.weaponDataDict[playerSheetScript.playerWeaponID]["Cast Time"];

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
        float delay = (float)GetComponent<PlayerSheet>().weaponDataDict[playerSheetScript.playerWeaponID]["Damage Delay"];

        yield return new WaitForSeconds(delay);

        // Get the weapon the player has selected
        playerWeapon = weaponParent.transform.GetChild(playerSheetScript.playerWeaponID).gameObject;

        GameObject newAttack = Instantiate(playerWeapon);
        newAttack.GetComponent<PlayerWeaponHandler>().weaponID = playerSheetScript.playerWeaponID;
        newAttack.GetComponent<PlayerWeaponHandler>().casterID = playerSheetScript.playerID;
        newAttack.GetComponent<PlayerWeaponHandler>().lifetime = (float)GetComponent<PlayerSheet>().weaponDataDict[playerSheetScript.playerWeaponID]["Lifetime"];
        newAttack.GetComponent<PlayerWeaponHandler>().damage = (float)GetComponent<PlayerSheet>().weaponDataDict[playerSheetScript.playerWeaponID]["Damage"];
        newAttack.GetComponent<PlayerWeaponHandler>().critChance = playerSheetScript.critChance;
        newAttack.GetComponent<PlayerWeaponHandler>().critDamage = playerSheetScript.critDamage;
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


    private void OpenCharMenu() {
        playerSheetScript.CharMenuUI = !playerSheetScript.CharMenuUI;
        CharUI.SetActive(playerSheetScript.CharMenuUI);

        if (playerSheetScript.CharMenuUI) {
            audioManagerScript.Play("UIOpenStatsMenu");
        } else {
            audioManagerScript.Play("UICloseStatsMenu");
        }
    }


    private void HealCharacter() {
        playerSheetScript.PotionCooldownTime = playerSheetScript.PotionCooldownTimeDef;
        playerSheetScript.PotionCooldownActive = true;

        float healAmount = playerSheetScript.maxHealth * (skillSheetScript.SkillHealPercentage / 100);
        playerSheetScript.currentHealth += healAmount;
    }


    private void PotionCooldown() {
        playerSheetScript.PotionCooldownTime -= Time.deltaTime;

        if (playerSheetScript.PotionCooldownTime <= 0) {
            playerSheetScript.PotionCooldownActive = false;

            // Potion is ready again
        }
    }

}
