using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumber : MonoBehaviour {

    private int[] modifierTiers = {
        0,
        24,
        49,
        74,
        100
    };


    private void Update() {
        if (Input.GetKeyDown("g")) {
            RandomizeLevel();
        }
    }

    private int RandomizeLevel() {
        int rndModifier = Random.Range(0, 100);

        print(rndModifier);

        if (rndModifier >= modifierTiers[0] && rndModifier < modifierTiers[1]) {
            print("Tier 1");
        } else if (rndModifier >= modifierTiers[1] && rndModifier < modifierTiers[2]) {
            print("Tier 2");
        } else if (rndModifier >= modifierTiers[2] && rndModifier < modifierTiers[3]) {
            print("Tier 3");
        } else if (rndModifier >= modifierTiers[3] && rndModifier < modifierTiers[4]) {
            print("Tier 4");
        }

        int rndLevel = 1;
        return rndLevel;
    }

}
