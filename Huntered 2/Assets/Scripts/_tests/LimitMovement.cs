using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitMovement : MonoBehaviour {

    private Camera MainCamera;


    void Start(){
        MainCamera = GameObject.Find("DevCam").GetComponent<Camera>();
    }


    void FixedUpdate() {
        Vector3 pos = MainCamera.WorldToViewportPoint (transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = MainCamera.ViewportToWorldPoint(pos);
    }

}
