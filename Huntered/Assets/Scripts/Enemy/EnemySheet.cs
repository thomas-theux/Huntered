using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySheet : MonoBehaviour {

    public int enemyID = 0;
    public int enemyClassID = 0;
    public int enemyLevel = 1;

    public List<Hashtable> classDataDict = new List<Hashtable>();
    private Hashtable classBasic = new Hashtable();
    private Hashtable classRanged = new Hashtable();
    private Hashtable classTank = new Hashtable();


    private void Awake() {
        // Add weapon data to dictionaries
        // BASIC ENEMY
        classBasic.Add("ID", 0);
        classBasic.Add("Name", "Basic Class");
        classBasic.Add("Move Speed", 3.0f);
        classBasic.Add("Aggro Radius", 20);
        classBasic.Add("Damage", 4.0f);
        classBasic.Add("Cooldown", 0.5f);
        classBasic.Add("Lifetime", 0.1f);
        
        // TANK ENEMY
        classTank.Add("ID", 1);
        classTank.Add("Name", "Tank Class");
        classTank.Add("Move Speed", 0.3f);
        classTank.Add("Aggro Radius", 20);
        classTank.Add("Damage", 6.0f);
        classTank.Add("Cooldown", 0.6f);
        classTank.Add("Lifetime", 0.2f);
        
        // RANGED ENEMY
        classRanged.Add("ID", 2);
        classRanged.Add("Name", "Ranged Class");
        classRanged.Add("Move Speed", 0.6f);
        classRanged.Add("Aggro Radius", 40);
        classRanged.Add("Damage", 3.0f);
        classRanged.Add("Cooldown", 0.4f);
        classRanged.Add("Lifetime", 2.0f);

        // Add all dictionaries to an array
        classDataDict.Add(classBasic);
        classDataDict.Add(classTank);
        classDataDict.Add(classRanged);
    }

}
