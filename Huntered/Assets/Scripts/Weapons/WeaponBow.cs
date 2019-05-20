using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBow : MonoBehaviour {

    private float travelSpeed = 30.0f;


    private void Start() {
        transform.parent = null;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * travelSpeed;
    }

}
