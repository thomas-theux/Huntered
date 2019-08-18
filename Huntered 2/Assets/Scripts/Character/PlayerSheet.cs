using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSheet : MonoBehaviour {

    public int playerID = 0;
    public int playerWeaponID = 0;
    public int currentGold = 4000;

    public float currentHealth = 0;
    public float maxHealth = 50.0f;
    public float moveSpeed = 6.0f;
    public int critChance = 2;
    public float critDamage = 1.5f;

    public float DiamondDropChance = 50;

    public float respawnTime = 10.0f;

    public bool CharMenuUI = false;
    public bool LinkingMenuUI = false;
    public bool DelayMovement = false;

    public bool isDead = false;
    public bool isDashing = false;
    public bool isIdle = false;
    public bool isWalking = false;
    public bool isAttacking = false;
    public int linkingPhase = 0;
    public bool isTalking = false;      // If player talks to an NPC – we need this for auto follow so that the player doesn't start following the other player

    public bool PotionCooldownActive = false;
    public float PotionCooldownTimeDef = 10.0f;
    public float PotionCooldownTime;

    public int UnlockedSlots = 5;

    public List<Hashtable> weaponDataDict = new List<Hashtable>();
    private Hashtable weaponBasic = new Hashtable();
    private Hashtable weaponTank = new Hashtable();
    private Hashtable weaponRanged = new Hashtable();
    private Hashtable weaponMage = new Hashtable();

    // All equipment arrays for Ghosts
    public List<Hashtable> SlottedGhostsHead = new List<Hashtable>();
    public List<Hashtable> SlottedGhostsTorso = new List<Hashtable>();
    public List<Hashtable> SlottedGhostsWeapon = new List<Hashtable>();
    public List<Hashtable> SlottedGhostsLegs = new List<Hashtable>();


    private void Awake() {
        // Initialize health
        currentHealth = maxHealth;

        // Add weapon data to dictionaries
        // BASIC WEAPON
        weaponBasic.Add("ID", 0);
        weaponBasic.Add("Name", "Basic Weapon");
        weaponBasic.Add("Damage", 10.0f);
        weaponBasic.Add("Cooldown", 0.5f);
        weaponBasic.Add("Damage Delay", 0.1f);
        weaponBasic.Add("Lifetime", 0.1f);
        weaponBasic.Add("Cast Time", 0.2f);

        // TANK
        weaponTank.Add("ID", 1);
        weaponTank.Add("Name", "Sword");
        weaponTank.Add("Damage", 18.0f);
        weaponTank.Add("Cooldown", 0.6f);
        weaponTank.Add("Damage Delay", 0.2f);
        weaponTank.Add("Lifetime", 0.3f);
        weaponTank.Add("Cast Time", 0.5f);

        // RANGED
        weaponRanged.Add("ID", 2);
        weaponRanged.Add("Name", "Bow");
        weaponRanged.Add("Damage", 12.0f);
        weaponRanged.Add("Cooldown", 0.4f);
        weaponRanged.Add("Damage Delay", 0.1f);
        weaponRanged.Add("Lifetime", 2.0f);
        weaponRanged.Add("Cast Time", 0.1f);

        // MAGE
        weaponMage.Add("ID", 3);
        weaponMage.Add("Name", "Staff");
        // weaponMage.Add("Damage", 16.0f);
        weaponMage.Add("Damage", 16000.0f);
        weaponMage.Add("Cooldown", 0.5f);
        weaponMage.Add("Damage Delay", 0.2f);
        weaponMage.Add("Lifetime", 1.0f);
        weaponMage.Add("Cast Time", 0.2f);

        // Add all dictionaries to an array
        weaponDataDict.Add(weaponBasic);
        weaponDataDict.Add(weaponTank);
        weaponDataDict.Add(weaponRanged);
        weaponDataDict.Add(weaponMage);

        // Initialize equipment arrays with the first slot available for players to link a Ghost
        for (int i = 0; i < UnlockedSlots; i++) {
            SlottedGhostsHead.Add(new Hashtable());
            SlottedGhostsTorso.Add(new Hashtable());
            SlottedGhostsWeapon.Add(new Hashtable());
            SlottedGhostsLegs.Add(new Hashtable());
        }
    }

}
