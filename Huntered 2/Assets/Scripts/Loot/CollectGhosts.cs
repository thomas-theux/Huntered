using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGhosts : MonoBehaviour {

    private bool playerDetected = false;
    private Transform collectingPlayer;
    private float collectSpeed = 5.0f;

    public Hashtable GhostData = new Hashtable();
    public GameObject CollectGhostSound;

    private int articleDrawChance = 40;
    private int roundValueBy = 100;
    private int roundChanceBy = 5;

    public int enemyLevel;
    private int levelModifier;
    private int rndType;
    private int rndLevel;
    private int rndEffect;
    private float rndEffectValue;

    private int[] modifierTiers = {
        0,
        19,
        78,
        98,
        99
    };

    private int[] tierLevel = {
        -1,
        0,
        1,
        3
    };

    // BASIC VALUES
    private float _thousand = 1000;
    private float _hundred = 100;
    private float _ten = 10;
    private float _nine = 9;
    private float _eight = 8;
    private float _five = 5;
    private float _four = 4;
    private float _threePointFive = 3.5f;
    private float _three = 3;
    private float _twoPointFive = 2.5f;
    private float _two = 2;
    private float _one = 1;
    private float _zeroPointFive = 0.5f;


    public void InitializeGhost() {
        // TYPES

        // 0 = Strength
        // 1 = Speed
        // 2 = Luck
        // 3 = Wisdom

        ///////////////////////////////////////////////////////////

        // IDENTIFIER

        // 00 = Deal more damage
        // 01 = Receive less damage
        // 02 = Increase being-hit-heal chance
        // 03 = Gain 1% health every 1000 steps
        // 04 = Increase hit-heal chance

        // 05 = Increase attack speed
        // 06 = Shorten respawn delay
        // 07 = Increase move speed
        // 08 = Shorten skill cooldown

        // 09 = Increase dodge heal
        // 10 = Increase crit hit chance
        // 11 = Increase dodge-heal chance
        // 12 = Increase crit-hit-heal chance
        // 13 = Increase dodge chance
        // 14 = Increase crit hit damage
        // 15 = Increase crit hit heal

        // 16 = Increase gold pickup radius
        // 17 = Increase XP gain
        // 18 = Gain gold when leveling up
        // 19 = Increase collect-double-gold chance
        // 20 = Gain 1% gold every 1000 steps
        // 21 = Gain 1% XP every 1000 steps
        // 22 = Increase gold drop
        // 23 = Increase Ghost drop chance

        ///////////////////////////////////////////////////////////

        // Randomize attributes
        string rndName = RandomizeName();
        int ghostUID = GameManager.GhostUID;
        rndType = Random.Range(0, 4);
        rndLevel = RandomizeLevel();
        rndEffect = RandomizeEffect();
        string effectDescr = BuildDescription();
        int rndChance = RandomizeChance();
        int rndValue = RandomizeValue();

        // Add data to dictionary
        GhostData.Add("Name", rndName);                     // The randomly generated name of the Ghost
        GhostData.Add("UID", ghostUID);                     // The unique ID for the Ghost
        GhostData.Add("Type", rndType);                     // What stat type does the Ghost have? (see above)
        GhostData.Add("Level", rndLevel);                   // What level does the Ghost have? Ranging from 1-10
        GhostData.Add("Effect", rndEffect);                 // What effect does this Ghost apply? Move Speed, Damage, ..
        GhostData.Add("Description", effectDescr);          // Which effect does this Ghost carry?
        GhostData.Add("Effect Value", rndEffectValue);      // What's the value/number the effect improves (e.g. +10% damage)
        GhostData.Add("Link Chance", rndChance);            // The chance to link a Ghost to your equipment
        GhostData.Add("Value", rndValue);                   // How much gold do you get for selling this Ghost?
        GhostData.Add("Player Access", 0);                  // Which player can pick it up?

        // Increment UID for Ghosts
        GameManager.GhostUID++;

        // Set color depending on type
        int getType = (int)GhostData["Type"];
        foreach (Transform child in this.gameObject.transform)
            child.GetComponent<Renderer>().material.color = ColorManager.GhostColors[getType];
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if ((int)GhostData["Player Access"] == other.GetComponent<PlayerSheet>().playerID) {

                // Add this Ghost to the collecting players inventory master array
                other.GetComponent<PlayerInventory>().AllGhosts.Add(GhostData);

                // Add this Ghost to the sub arrays (for later filtering)
                int ghostType = (int)GhostData["Type"];
                other.GetComponent<PlayerInventory>().GhostsInventory[ghostType].Add(GhostData);

                // Add to one big array
                // other.GetComponent<PlayerInventory>().GhostsInventory.Add(GhostData);

                float rndPitch = Random.Range(0.95f, 1.0f);
                rndPitch = 1.0f;
                CollectGhostSound.GetComponent<AudioSource>().pitch = rndPitch;
                Instantiate(CollectGhostSound);

                Destroy(this.gameObject);
            }
        }
    }


    public void SetPlayerTarget(Collider targetPlayer) {
        if (!playerDetected) {
            if ((int)GhostData["Player Access"] == targetPlayer.GetComponent<PlayerSheet>().playerID) {
                collectingPlayer = targetPlayer.transform;
                playerDetected = true;
            }
        }
    }


    private void FixedUpdate() {
        if (playerDetected) {
            Vector3 desiredPos = collectingPlayer.transform.position;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, collectSpeed * Time.deltaTime);
            collectSpeed *= 1.2f;

            transform.position = smoothedPos;
        }
    }


    private string RandomizeName() {
        string generatedName = "";

        string rndArticle = "";
        string rndAdjective = "";
        string rndName = "";

        // Check for chance to add an article and get it from the Ghost texts script
        int articleChance = Random.Range(0, 100);
        if (articleChance < articleDrawChance) {
            int getRndArticle = Random.Range(0, TextsGhosts.GhostArticles[GameSettings.language].Length);
            rndArticle = TextsGhosts.GhostArticles[GameSettings.language][getRndArticle];
            rndArticle = rndArticle + " ";
        }

        // Get random adjective from Ghost texts script
        int getRndAdjective = Random.Range(0, TextsGhosts.GhostAdjectives[GameSettings.language].Length);
        rndAdjective = TextsGhosts.GhostAdjectives[GameSettings.language][getRndAdjective];

        // Get random name from Ghost texts script
        int getRndName = Random.Range(0, TextsGhosts.GhostNames[GameSettings.language].Length);
        rndName = TextsGhosts.GhostNames[GameSettings.language][getRndName];

        // Remove articles
        rndArticle = "";

        generatedName = rndArticle + rndAdjective + " " + rndName;

        return generatedName;
    }


    private int RandomizeLevel() {
        int rndModifier = Random.Range(0, 100);

        if (rndModifier >= modifierTiers[0] && rndModifier < modifierTiers[1]) {
            levelModifier = tierLevel[0];
        } else if (rndModifier >= modifierTiers[1] && rndModifier < modifierTiers[2]) {
            levelModifier = tierLevel[1];
        } else if (rndModifier >= modifierTiers[2] && rndModifier < modifierTiers[3]) {
            levelModifier = tierLevel[2];
        } else if (rndModifier >= modifierTiers[3] && rndModifier < modifierTiers[4]) {
            levelModifier = tierLevel[3];
        }

        float maxLevel = GameSettings.maxRepLevel / 10;
        int calculatedEnemyLevel = Mathf.RoundToInt(enemyLevel / maxLevel);

        int ghostLevel = calculatedEnemyLevel + levelModifier;

        if (ghostLevel < 1) {
            ghostLevel = 1;
        } else if (ghostLevel > 10) {
            ghostLevel = 10;
        }

        return ghostLevel;
    }


    private int RandomizeEffect() {
        int ghostIdentifier = 0;

        switch (rndType) {
            case 0:
                ghostIdentifier = Random.Range(0, 5);
                break;
            case 1:
                ghostIdentifier = Random.Range(5, 9);
                break;
            case 2:
                ghostIdentifier = Random.Range(9, 16);
                break;
            case 3:
                ghostIdentifier = Random.Range(16, 24);
                break;
        }

        return ghostIdentifier;
    }


    private string BuildDescription() {
        string effectDescription = "";

        switch (rndEffect) {
            case 0:
                rndEffectValue = _five + _three * (rndLevel - 1);
                effectDescription = TextsUI.EffectStrengthA[GameSettings.language] + rndEffectValue;
                break;
            case 1:
                rndEffectValue = -_two - _threePointFive * (rndLevel - 1);
                effectDescription = TextsUI.EffectStrengthB[GameSettings.language] + rndEffectValue;
                break;
            case 2:
                rndEffectValue = _two + _two * (rndLevel - 1);
                effectDescription = TextsUI.EffectStrengthC[GameSettings.language] + rndEffectValue;
                break;
            case 3:
                rndEffectValue = _thousand - _hundred * (rndLevel - 1);
                effectDescription = TextsUI.EffectStrengthD[GameSettings.language] + rndEffectValue;
                break;
            case 4:
                rndEffectValue = _two + _two * (rndLevel - 1);
                effectDescription = TextsUI.EffectStrengthE[GameSettings.language] + rndEffectValue;
                break;



            case 5:
                rndEffectValue = _five + _four * (rndLevel - 1);
                effectDescription = TextsUI.EffectSpeedA[GameSettings.language] + rndEffectValue;
                break;
            case 6:
                rndEffectValue = -_five - _twoPointFive * (rndLevel - 1);
                effectDescription = TextsUI.EffectSpeedB[GameSettings.language] + rndEffectValue;
                break;
            case 7:
                rndEffectValue = _five + _four * (rndLevel - 1);
                effectDescription = TextsUI.EffectSpeedC[GameSettings.language] + rndEffectValue;
                break;
            case 8:
                rndEffectValue = -_five - _two * (rndLevel - 1);
                effectDescription = TextsUI.EffectSpeedD[GameSettings.language] + rndEffectValue;
                break;



            case 9:
                rndEffectValue = _one + _two * (rndLevel - 1);
                effectDescription = TextsUI.EffectLuckA[GameSettings.language] + rndEffectValue;
                break;
            case 10:
                rndEffectValue = _zeroPointFive + _one * (rndLevel - 1);
                effectDescription = TextsUI.EffectLuckB[GameSettings.language] + rndEffectValue;
                break;
            case 11:
                rndEffectValue = _two + _nine * (rndLevel - 1);
                effectDescription = TextsUI.EffectLuckD[GameSettings.language] + rndEffectValue;
                break;
            case 12:
                rndEffectValue = _two + _eight * (rndLevel - 1);
                effectDescription = TextsUI.EffectLuckE[GameSettings.language] + rndEffectValue;
                break;
            case 13:
                rndEffectValue = _two + _two * (rndLevel - 1);
                effectDescription = TextsUI.EffectLuckC[GameSettings.language] + rndEffectValue;
                break;
            case 14:
                rndEffectValue = _two + _three * (rndLevel - 1);
                effectDescription = TextsUI.EffectLuckF[GameSettings.language] + rndEffectValue;
                break;
            case 15:
                rndEffectValue = _one + _two * (rndLevel - 1);
                effectDescription = TextsUI.EffectLuckG[GameSettings.language] + rndEffectValue;
                break;



            case 16:
                rndEffectValue = _zeroPointFive + _zeroPointFive * (rndLevel - 1);
                effectDescription = TextsUI.EffectWisdomA[GameSettings.language] + rndEffectValue;
                break;
            case 17:
                rndEffectValue = _five + _ten * (rndLevel - 1);
                effectDescription = TextsUI.EffectWisdomB[GameSettings.language] + rndEffectValue;
                break;
            case 18:
                rndEffectValue = _one + _two * (rndLevel - 1);
                effectDescription = TextsUI.EffectWisdomC[GameSettings.language] + rndEffectValue;
                break;
            case 19:
                rndEffectValue = _two + _two * (rndLevel - 1);
                effectDescription = TextsUI.EffectWisdomD[GameSettings.language] + rndEffectValue;
                break;
            case 20:
                rndEffectValue = _thousand - _hundred * (rndLevel - 1);
                effectDescription = TextsUI.EffectWisdomE[GameSettings.language] + rndEffectValue;
                break;
            case 21:
                rndEffectValue = _thousand - _hundred * (rndLevel - 1);
                effectDescription = TextsUI.EffectWisdomF[GameSettings.language] + rndEffectValue;
                break;
            case 22:
                rndEffectValue = _five + _ten * (rndLevel - 1);
                effectDescription = TextsUI.EffectWisdomG[GameSettings.language] + rndEffectValue;
                break;
            case 23:
                rndEffectValue = _five + _five * (rndLevel - 1);
                effectDescription = TextsUI.EffectWisdomH[GameSettings.language] + rndEffectValue;
                break;
        }

        return effectDescription;
    }


    private int RandomizeChance() {
        int newChance = 0;

        newChance = Random.Range(1, 101);

        float calcChance = newChance / roundChanceBy;
        newChance = Mathf.RoundToInt(calcChance);
        newChance *= roundChanceBy;

        return newChance;
    }


    private int RandomizeValue() {
        int newValue = 0;

        newValue = Random.Range(
            GameSettings.minGhostValue * rndLevel,
            GameSettings.maxGhostValue * rndLevel
        );

        float calcValue = newValue / roundValueBy;
        newValue = Mathf.RoundToInt(calcValue);
        newValue *= roundValueBy;

        if (newValue == 0) {
            newValue = 5;
        }

        return newValue;
    }

}
