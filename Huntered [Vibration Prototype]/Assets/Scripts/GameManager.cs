using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject groundGO;
    public GameObject enemyGO;


    private void Start() {
        float minX = 0 - ((groundGO.transform.localScale.x * 10) / 2);
        float maxX = 0 + ((groundGO.transform.localScale.x * 10) / 2);

        float minZ = 0 - ((groundGO.transform.localScale.z * 10) / 2);
        float maxZ = 0 + ((groundGO.transform.localScale.z * 10) / 2);

        float rndX = Random.Range(minX, maxX);
        float rndZ = Random.Range(minZ, maxZ);

        Vector3 rndPos = new Vector3(
            rndX,
            enemyGO.transform.position.y,
            rndZ
        );

        GameObject newEnemy = Instantiate(enemyGO);
        newEnemy.transform.position = rndPos;
    }
}
