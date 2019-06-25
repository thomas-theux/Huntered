using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class LoadLevel : MonoBehaviour {

    private void Update() {
        if (ReInput.players.GetPlayer(0).GetButtonDown("X")) {
            SceneManager.LoadScene("0 Test Level");
        }
    }

}
