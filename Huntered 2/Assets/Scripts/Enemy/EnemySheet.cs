using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySheet : MonoBehaviour {

    public int enemyID = 0;
    public int enemyClassID = 0;
    public int enemyLevel = 0;

    public bool DelayMovement = false;

    // 0 = idle ; 1 = aggro ; 2 = in reach and attacking
    public int actionMode = 0;

    public List<Hashtable> classDataDict = new List<Hashtable>();
    private Hashtable classBasic = new Hashtable();
    private Hashtable classRanged = new Hashtable();
    private Hashtable classTank = new Hashtable();


    private void Awake() {
        // Add weapon data to dictionaries
        // BASIC ENEMY
        classBasic.Add("ID", 0);
        classBasic.Add("Name", "Basic Class");
        classBasic.Add("Move Speed", 2.0f);
        classBasic.Add("Aggro Radius", 30);
        classBasic.Add("Reach Radius", 5);
        classBasic.Add("Damage", 4.0f);
        classBasic.Add("Cooldown", 1.0f);
        classBasic.Add("DamageDelay", 0.1f);
        classBasic.Add("Lifetime", 0.1f);
        classBasic.Add("CastTime", 0.2f);

        // TANK ENEMY
        classTank.Add("ID", 1);
        classTank.Add("Name", "Tank Class");
        classTank.Add("Move Speed", 1.5f);
        classTank.Add("Aggro Radius", 24);
        classTank.Add("Reach Radius", 8);
        classTank.Add("Damage", 8.0f);
        classTank.Add("Cooldown", 2.0f);
        classTank.Add("DamageDelay", 0.2f);
        classTank.Add("Lifetime", 0.2f);
        classTank.Add("CastTime", 0.5f);

        // RANGED ENEMY
        classRanged.Add("ID", 2);
        classRanged.Add("Name", "Ranged Class");
        classRanged.Add("Move Speed", 3.0f);
        classRanged.Add("Aggro Radius", 60);
        classRanged.Add("Reach Radius", 50);
        classRanged.Add("Damage", 2.0f);
        classRanged.Add("Cooldown", 0.5f);
        classRanged.Add("DamageDelay", 0.1f);
        classRanged.Add("Lifetime", 2.0f);
        classRanged.Add("CastTime", 0.1f);

        // Add all dictionaries to an array
        classDataDict.Add(classBasic);
        classDataDict.Add(classTank);
        classDataDict.Add(classRanged);
    }

}
