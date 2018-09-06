using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour {

    [System.Serializable]
    public class Loot
    {
        public int LootObjectID;
        public string Rarity;

        public Loot()
        {
        }
    }


    public Loot[] lootTable_0;
    public Loot[] lootTable_1;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
