using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject groundGO;
    public GameObject enemyGO;

    public static float currentRep = 0;
    public static int currentRepLevel = 0;

    public static List<float> neededRepArr = new List<float>();
    public static List<float> repGainArr = new List<float>();


    private void Awake() {
        float calculatedRep = GameSettings.baseRepNeeded;
        float calculatedGain = GameSettings.baseRepGain;

        for (int i = 0; i < GameSettings.maxRepLevel; i++) {
            neededRepArr.Add(calculatedRep);
            calculatedRep = calculatedRep + calculatedRep * GameSettings.repNeededMultiplier;

            repGainArr.Add(calculatedGain);
            calculatedGain = calculatedGain + calculatedGain * GameSettings.repMultiplier;
        }
    }
    

    public static void AddReputation() {
        while (currentRep >= neededRepArr[currentRepLevel]) {
            currentRepLevel++;
        }
    }


    public static void SubtractReputation() {
        while (currentRepLevel > 0 && currentRep < neededRepArr[currentRepLevel-1]) {
            currentRepLevel--;
        } 
    }

}
