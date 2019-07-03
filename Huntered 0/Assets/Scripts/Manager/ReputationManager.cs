using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationManager : MonoBehaviour {

    public static float currentRep = 0;
    public static int currentRepLevel = 0;
    public static int repLevelMax = 0;

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
            // FindObjectOfType<AudioManager>().Play("LevelUp1");
            FindObjectOfType<AudioManager>().Play("LevelUp2");

            if (currentRepLevel > repLevelMax) {
                // Set indicator to the highest level the players reached
                repLevelMax = currentRepLevel;

                // Heal all characters
                for (int i = 0; i < GameManager.AllPlayers.Count; i++) {
                    GameManager.AllPlayers[i].GetComponent<PlayerSheet>().currentHealth = GameManager.AllPlayers[i].GetComponent<PlayerSheet>().maxHealth;
                }
            }
        }
    }


    public static void SubtractReputation() {
        while (currentRepLevel > 0 && currentRep < neededRepArr[currentRepLevel-1]) {
            currentRepLevel--;
            FindObjectOfType<AudioManager>().Play("LevelDown");
        }
    }
}
