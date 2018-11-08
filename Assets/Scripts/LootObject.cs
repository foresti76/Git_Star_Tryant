using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : MonoBehaviour
{
    public List<int> myLoot;
    public bool isBeingTractored = false;
    public LootPanel lootPanel;
    public LootTable lootTable;

    TractorBeam tractorBeam;
    // Use this for initialization
    void Awake()
    {
        tractorBeam = GameObject.FindGameObjectWithTag("Player").GetComponent<TractorBeam>();
        lootPanel = GameObject.Find("LootPanelControl").GetComponent<LootPanel>();
        lootTable = GameObject.Find("LootTable").GetComponent<LootTable>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player" && isBeingTractored)
        {
            foreach (int loot in myLoot)
            {
                lootPanel.AddLoot(loot);
            }

            lootPanel.currentLootObject = this;
            lootPanel.OpenLootPanel();
            tractorBeam.DisngageTractorBeam();
        }
    }

    public void CreateLoot(string chosenLootTable, int amount)
    {
        lootTable = GameObject.Find("LootTable").GetComponent<LootTable>();
        // create a new list to add the loot too
        // add the appropriate amont of loot ids depending on its rarity to the list
        List<int> lootList = new List<int>();
        foreach (LootTable.Loot loot in lootTable.lootTableLookup[chosenLootTable])
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
        myLoot = new List<int>();
        for (int i = 0; i < amount; i++)
        {
            myLoot.Add(lootList[Random.Range(0, lootList.Count - 1)]);
        }
        // clear the created list
        lootList.Clear();
    }
}
