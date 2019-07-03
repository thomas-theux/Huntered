using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGhosts : MonoBehaviour {

    private bool playerDetected = false;
    private Transform collectingPlayer;
    private float collectSpeed = 5.0f;

    public Hashtable GhostData = new Hashtable();
    public GameObject CollectGhostSound;


    private void Awake() {
        // TYPES
        // 0 = Strength
        // 1 = Speed
        // 2 = Luck
        // 3 = Wisdom

        // Randomize attributes
        string rndName = RandomizeName();
        int rndType = Random.Range(0, 4);
        int rndLevel = RandomizeLevel();
        int rndEffect = Random.Range(0, 2);

        // Add data to dictionary
        GhostData.Add("Name", rndName);                     // The randomly generated name of the Ghost
        GhostData.Add("Type", rndType);                     // What stat type does the Ghost have? (see above)
        GhostData.Add("Level", rndLevel);                   // What level does the Ghost have? Ranging from 1-10
        GhostData.Add("Effect", rndEffect);                 // What effect does this Ghost apply? Move Speed, Damage, ..
        GhostData.Add("Player Access", 0);                  // Which player can pick it up?

        // Set color depending on type
        int getType = (int)GhostData["Type"];
        foreach (Transform child in this.gameObject.transform)
            child.GetComponent<Renderer>().material.color = ColorManager.GhostColors[getType];
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if ((int)GhostData["Player Access"] == other.GetComponent<PlayerSheet>().playerID) {

                // Add this Ghost to the collecting players inventory
                int ghostType = (int)GhostData["Type"];
                other.GetComponent<PlayerInventory>().AllGhosts[ghostType].Add(GhostData);

                // Add to one big array
                // other.GetComponent<PlayerInventory>().GhostsInventory.Add(GhostData);

                float rndPitch = Random.Range(0.95f, 1.0f);
                rndPitch = 1.0f;
                CollectGhostSound.GetComponent<AudioSource>().pitch = rndPitch;
                Instantiate(CollectGhostSound);

                Destroy(this.gameObject);
            }
        }
    }


    public void SetPlayerTarget(Collider targetPlayer) {
        if (!playerDetected) {
            if ((int)GhostData["Player Access"] == targetPlayer.GetComponent<PlayerSheet>().playerID) {
                collectingPlayer = targetPlayer.transform;
                playerDetected = true;
            }
        }
    }


    private void FixedUpdate() {
        if (playerDetected) {
            Vector3 desiredPos = collectingPlayer.transform.position;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, collectSpeed * Time.deltaTime);
            collectSpeed *= 1.2f;

            transform.position = smoothedPos;
        }
    }


    private string RandomizeName() {
        string rndName;

        rndName = "Manu";

        return rndName;
    }


    private int RandomizeLevel() {
        int rndLevel;

        rndLevel = 1;

        return rndLevel;
    }

}
