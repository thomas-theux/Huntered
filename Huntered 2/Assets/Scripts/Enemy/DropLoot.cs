using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour {

    public GameObject GoldLoot;
    public GameObject GhostLoot;
    private int enemyLevel;

    private Vector3 enemyPos;
    private float minPosX;
    private float maxPosX;
    private float minPosZ;
    private float maxPosZ;


    private void Awake() {
        enemyLevel = GetComponent<EnemySheet>().enemyLevel;
        if (enemyLevel == 0) enemyLevel = 1;
    }


    public void DropGold() {
        GetPos();

        int goldDropAmount = Random.Range(GameSettings.minGoldDrop, GameSettings.maxGoldDrop);

        for (int i = 0; i < goldDropAmount; i++) {
            float dropPosX = Random.Range(minPosX, maxPosX);
            float dropPosZ = Random.Range(minPosZ, maxPosZ);

            GameObject newGoldDrop = Instantiate(GoldLoot);
            newGoldDrop.transform.position = new Vector3(dropPosX, newGoldDrop.transform.position.y, dropPosZ);
        }
    }


    public void DropGhosts() {
        for (int i = 0; i < GameManager.AllPlayers.Count; i++) {

            for (int j = 0; j < enemyLevel; j++) {
                float dropChance = GameManager.AllPlayers[i].GetComponent<PlayerSheet>().DiamondDropChance;
                int rndDropChance = Random.Range(0, 100);

                if (rndDropChance < dropChance) {
                    GetPos();
                    
                    float dropPosX = Random.Range(minPosX, maxPosX);
                    float dropPosZ = Random.Range(minPosZ, maxPosZ);

                    GameObject newGhostDrop = Instantiate(GhostLoot);
                    newGhostDrop.transform.position = new Vector3(dropPosX, newGhostDrop.transform.position.y, dropPosZ);
                    // newGhostDrop.GetComponent<CollectGhosts>().PlayerAccess = i;
                    newGhostDrop.GetComponent<CollectGhosts>().GhostData["Player Access"] = i;
                }
            }
        }
    }


    private void GetPos() {
        enemyPos = transform.position;

        minPosX = enemyPos.x - GameSettings.dropAreaSize;
        maxPosX = enemyPos.x + GameSettings.dropAreaSize;
        minPosZ = enemyPos.z - GameSettings.dropAreaSize;
        maxPosZ = enemyPos.z + GameSettings.dropAreaSize;
    }

}
