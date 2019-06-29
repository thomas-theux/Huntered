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
    private Hashtable classTank = new Hashtable();
    private Hashtable classRanged = new Hashtable();
    private Hashtable classMage = new Hashtable();
    private Hashtable classBoss = new Hashtable();
    private Hashtable classDummy = new Hashtable();


    private void Awake() {
        // Add weapon data to dictionaries
        // BASIC ENEMY
        classBasic.Add("ID", 0);
        classBasic.Add("Name", "Basic Class");
        classBasic.Add("Move Speed", 2.0f);
        classBasic.Add("Aggro Radius", 26.0f);
        classBasic.Add("Attack Radius", 5.0f);
        classBasic.Add("Damage", 3.0f);
        classBasic.Add("Cooldown", 1.0f);
        classBasic.Add("Damage Delay", 0.1f);
        classBasic.Add("Lifetime", 0.1f);
        classBasic.Add("Cast Time", 0.2f);

        // TANK ENEMY
        classTank.Add("ID", 1);
        classTank.Add("Name", "Tank Class");
        classTank.Add("Move Speed", 1.5f);
        classTank.Add("Aggro Radius", 20.0f);
        classTank.Add("Attack Radius", 8.0f);
        classTank.Add("Damage", 6.0f);
        classTank.Add("Cooldown", 2.0f);
        classTank.Add("Damage Delay", 0.2f);
        classTank.Add("Lifetime", 0.2f);
        classTank.Add("Cast Time", 0.5f);

        // RANGED ENEMY
        classRanged.Add("ID", 2);
        classRanged.Add("Name", "Ranged Class");
        classRanged.Add("Move Speed", 3.0f);
        classRanged.Add("Aggro Radius", 40.0f);
        classRanged.Add("Attack Radius", 36.0f);
        classRanged.Add("Damage", 1.5f);
        classRanged.Add("Cooldown", 0.8f);
        classRanged.Add("Damage Delay", 0.1f);
        classRanged.Add("Lifetime", 2.0f);
        classRanged.Add("Cast Time", 0.1f);

        // MAGE ENEMY
        classMage.Add("ID", 3);
        classMage.Add("Name", "Mage Class");
        classMage.Add("Move Speed", 2.0f);
        classMage.Add("Aggro Radius", 28.0f);
        classMage.Add("Attack Radius", 24.0f);
        classMage.Add("Damage", 4.0f);
        classMage.Add("Cooldown", 1.5f);
        classMage.Add("Damage Delay", 0.2f);
        classMage.Add("Lifetime", 2.0f);
        classMage.Add("Cast Time", 0.2f);

        // BOSS ENEMY
        classBoss.Add("ID", 4);
        classBoss.Add("Name", "Mage Class");
        classBoss.Add("Move Speed", 1.5f);
        classBoss.Add("Aggro Radius", 10.0f);
        classBoss.Add("Attack Radius", 5.0f);
        classBoss.Add("Damage", 12.0f);
        classBoss.Add("Cooldown", 2.0f);
        classBoss.Add("Damage Delay", 0.4f);
        classBoss.Add("Lifetime", 0.3f);
        classBoss.Add("Cast Time", 0.8f);

        // DUMMY ENEMY
        classDummy.Add("ID", 5);
        classDummy.Add("Name", "Dummy");
        classDummy.Add("Move Speed", 1.0f);
        classDummy.Add("Aggro Radius", 1.0f);
        classDummy.Add("Attack Radius", 1.0f);
        classDummy.Add("Damage", 1.0f);
        classDummy.Add("Cooldown", 10.0f);
        classDummy.Add("Damage Delay", 0.5f);
        classDummy.Add("Lifetime", 0.1f);
        classDummy.Add("Cast Time", 1.0f);

        // Add all enemy dictionaries to an array
        classDataDict.Add(classBasic);
        classDataDict.Add(classTank);
        classDataDict.Add(classRanged);
        classDataDict.Add(classMage);
        classDataDict.Add(classBoss);
        classDataDict.Add(classDummy);
    }

}
