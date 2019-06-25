using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour {

    private void Awake() {
        float duration = GetComponent<AudioSource>().clip.length;
        Destroy(gameObject, duration);
    }

}
