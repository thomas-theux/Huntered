﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public List<Hashtable> GhostsInventory = new List<Hashtable>();


    private void Update() {
        if (Input.GetKey("g")) {
            print(GhostsInventory.Count);
        }
    }

}
