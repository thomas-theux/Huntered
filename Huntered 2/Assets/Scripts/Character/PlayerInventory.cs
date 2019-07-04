using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public List<List<Hashtable>> GhostsInventory = new List<List<Hashtable>>();
    // public List<Hashtable> GhostsInventory = new List<Hashtable>();
    public List<Hashtable> AllGhosts = new List<Hashtable>();

    public List<Hashtable> StrengthGhosts = new List<Hashtable>();
    public List<Hashtable> SpeedGhosts = new List<Hashtable>();
    public List<Hashtable> LuckGhosts = new List<Hashtable>();
    public List<Hashtable> WisdomGhosts = new List<Hashtable>();


    private void Awake() {
        GhostsInventory.Add(StrengthGhosts);
        GhostsInventory.Add(SpeedGhosts);
        GhostsInventory.Add(LuckGhosts);
        GhostsInventory.Add(WisdomGhosts);
    }

}
