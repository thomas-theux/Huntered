using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEdges : MonoBehaviour {

    private Camera mainCam;

    private void Awake() {
        mainCam = GameObject.Find("DevCam").GetComponent<Camera>();
    }

}
