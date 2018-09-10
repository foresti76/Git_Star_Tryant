using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : MonoBehaviour
{
    public List<int> myLoot = new List<int>();
    public bool isBeingTractored = false;
    public LootPanel lootPanel;
    public LootTable lootTable;

    // Use this for initialization
    void Start()
    {
        lootPanel = GetComponent<LootPanel>();
        lootTable = GetComponent<LootTable>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player" && isBeingTractored)
        {
            foreach (int loot in myLoot)
            {
                lootPanel.AddLoot(loot);
            }
            lootPanel.OpenLootPanel();
        }
    }

    public void CreateLoot(string chosenLootTable, int amount)
    {
        // create a new list to add the loot too
        List<int> lootList = new List<int>();
        // add the appropriate amont of loot ids depending on its rarity to the list
        foreach (LootTable.Loot loot in lootTable.lootTable_0)
        {
            int addtimes = 0;
            if (loot.Rarity == "common")
            {
                addtimes = 20;
            }
            if (loot.Rarity == "uncommon")
            {
                addtimes = 10;
            }
            if (loot.Rarity == "rare")
            {
                addtimes = 5;
            }
            if (loot.Rarity == "ultrarare")
            {
                addtimes = 3;
            }
            if (loot.Rarity == "legendary")
            {
                addtimes = 1;
            }
            for (int a = 0; a < addtimes; a++)
            {
                lootList.Add(loot.LootObjectID);
            }
        }
        // now add the actual loot to this loot objects list
        for (int i = 0; i <= amount; i++)
        {
            myLoot.Add(lootList[Random.Range(0, lootList.Count)]);
        }
        // clear the created list
        lootList.Clear();
    }
}
