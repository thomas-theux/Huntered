﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacing : MonoBehaviour {

    private void Update() {
        transform.rotation = Camera.main.transform.rotation;
        // transform.LookAt(Camera.main.transform);
    }
}
