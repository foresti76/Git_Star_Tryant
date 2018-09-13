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
    public Dictionary<string, Loot[]> lootTableLookup = new Dictionary<string, Loot[]>();

    void Start()
    {
        lootTableLookup.Add("lootTable_0", lootTable_0);
        lootTableLookup.Add("lootTable_1", lootTable_1);
    }
}
