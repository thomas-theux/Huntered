using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPos : MonoBehaviour {

    private Vector3 posOne;
    private Vector3 posTwo;


    private void Update() {
        posOne = GameObject.Find("Character0").transform.position;
        posTwo = GameObject.Find("Character1").transform.position;

        float posX = (posOne.x + posTwo.x) / 2;
        float posZ = (posOne.z + posTwo.z) / 2;

        this.gameObject.transform.position = new Vector3(posX, 1, posZ);
    }
}
