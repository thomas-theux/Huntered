using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour {

    public GameObject GoldLoot;
    public GameObject GhostLoot;


    public void DropGold() {
        Vector3 enemyPos = transform.position;

        float minPosX = enemyPos.x - GameSettings.dropAreaSize;
        float maxPosX = enemyPos.x + GameSettings.dropAreaSize;
        float minPosZ = enemyPos.z - GameSettings.dropAreaSize;
        float maxPosZ = enemyPos.z + GameSettings.dropAreaSize;

        int goldDropAmount = Random.Range(GameSettings.minGoldDrop, GameSettings.maxGoldDrop);

        for (int i = 0; i < goldDropAmount; i++) {
            float dropPosX = Random.Range(minPosX, maxPosX);
            float dropPosZ = Random.Range(minPosZ, maxPosZ);

            GameObject newGoldDrop = Instantiate(GoldLoot);
            newGoldDrop.transform.position = new Vector3(dropPosX, newGoldDrop.transform.position.y, dropPosZ);
        }
    }


    public void DropGhosts() {
        // int dropChance = GameManager.AllPlayers[]
    }

}
