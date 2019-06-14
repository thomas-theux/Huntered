using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStaff : MonoBehaviour {

    private float travelSpeed = 20.0f;


    private void Start() {
        transform.parent = null;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * travelSpeed;
    }
}
