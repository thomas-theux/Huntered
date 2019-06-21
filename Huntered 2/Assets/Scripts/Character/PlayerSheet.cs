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

    public bool StatsUIActive = false;
    public bool DelayMovement = false;

    public bool isDead = false;

    public List<Hashtable> weaponDataDict = new List<Hashtable>();
    private Hashtable weaponBasic = new Hashtable();
    private Hashtable weaponSword = new Hashtable();
    private Hashtable weaponBow = new Hashtable();


    private void Awake() {
        // Initialize health
        currentHealth = maxHealth;

        // Add weapon data to dictionaries
        // BASIC WEAPON
        weaponBasic.Add("ID", 0);
        weaponBasic.Add("Name", "Basic Weapon");
        weaponBasic.Add("Damage", 10.0f);
        weaponBasic.Add("Cooldown", 0.5f);
        weaponBasic.Add("DamageDelay", 0.1f);
        weaponBasic.Add("Lifetime", 0.1f);
        weaponBasic.Add("CastTime", 0.2f);

        // SWORD
        weaponSword.Add("ID", 1);
        weaponSword.Add("Name", "Sword");
        weaponSword.Add("Damage", 18.0f);
        weaponSword.Add("Cooldown", 0.6f);
        weaponSword.Add("DamageDelay", 0.2f);
        weaponSword.Add("Lifetime", 0.3f);
        weaponSword.Add("CastTime", 0.5f);

        // BOW
        weaponBow.Add("ID", 2);
        weaponBow.Add("Name", "Bow");
        weaponBow.Add("Damage", 12.0f);
        weaponBow.Add("Cooldown", 0.4f);
        weaponBow.Add("DamageDelay", 0.1f);
        weaponBow.Add("Lifetime", 2.0f);
        weaponBow.Add("CastTime", 0.1f);

        // Add all dictionaries to an array
        weaponDataDict.Add(weaponBasic);
        weaponDataDict.Add(weaponSword);
        weaponDataDict.Add(weaponBow);
    }

}
