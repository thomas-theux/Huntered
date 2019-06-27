using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour {

    private float height = 3.0f;
    private float animationSpeed = 2.0f;
    private float lifetime = 1.0f;
    private GameObject camTarget;


    private void Awake() {
        Destroy(this.gameObject, lifetime);
        camTarget = GameObject.Find("DevCam");
    }


    private void Update() {
        Vector3 desiredPos = new Vector3(
            this.transform.localPosition.x,
            this.transform.localPosition.x + height,
            this.transform.localPosition.z
        );
        Vector3 smoothedPos = Vector3.Lerp(transform.localPosition, desiredPos, animationSpeed * Time.deltaTime);

        transform.localPosition = smoothedPos;
    }

}
