using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AimAssist : MonoBehaviour {

    private List<Collider> enemyColliderList = new List<Collider>();
    private List<GameObject> posIndicatorList = new List<GameObject>();
    private List<float> distancesList = new List<float>();

    public GameObject EnemyPosIndicator;
    public GameObject PosIndicatorContainer;

    public GameObject ClosestEnemy;


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            GameObject newEnemyIndicator = Instantiate(EnemyPosIndicator);
            newEnemyIndicator.transform.parent = PosIndicatorContainer.transform;
            newEnemyIndicator.transform.localPosition = new Vector3(0.0f, 2.0f, 0.0f);

            // newEnemyIndicator.name = other.name;
            // print("Added " + newEnemyIndicator.name);

            posIndicatorList.Add(newEnemyIndicator);
            enemyColliderList.Add(other);
            distancesList.Add(0.0f);
        }
    }


    private void OnTriggerExit(Collider other) {
        RemoveEnemy(other);
    }


    public void RemoveEnemy(Collider other) {
        if (other.tag == "Enemy") {
            int removeEnemy = enemyColliderList.IndexOf(other);

            // print("Removed " + enemyColliderList[removeEnemy].name);

            Destroy(posIndicatorList[removeEnemy]);
            posIndicatorList.RemoveAt(removeEnemy);
            enemyColliderList.RemoveAt(removeEnemy);
            distancesList.RemoveAt(removeEnemy);
        }
    }


    private void Update() {
        if (posIndicatorList.Count > 0) {
            UpdatePosition();
            GetShortestDistance();
        }
    }


    private void UpdatePosition() {
        for (int i = 0; i < posIndicatorList.Count; i++) {
            float distanceToCharacter = Vector3.Distance(this.transform.parent.position, enemyColliderList[i].transform.position);

            posIndicatorList[i].transform.localPosition = new Vector3(
                0.0f,
                1.0f,
                distanceToCharacter
            );
        }
    }


    private void GetShortestDistance() {
        for (int i = 0; i < posIndicatorList.Count; i++) {
            float distanceToIndicator = Vector3.Distance(posIndicatorList[i].transform.position, enemyColliderList[i].transform.position);
            distancesList[i] = distanceToIndicator;

            float shortestDistance = distancesList.Min();
            int index = distancesList.IndexOf(shortestDistance);

            ClosestEnemy = enemyColliderList[index].gameObject;
        }
    }

}
