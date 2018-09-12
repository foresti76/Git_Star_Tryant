using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : MonoBehaviour
{
    public List<int> myLoot;
    public bool isBeingTractored = false;
    public LootPanel lootPanel;
    public LootTable lootTable;

    // Use this for initialization
    void Awake()
    {
        lootPanel = GameObject.Find("LootPanelControl").GetComponent<LootPanel>();
        lootTable = GameObject.Find("LootTable").GetComponent<LootTable>();
        Debug.Log(lootTable.lootTable_0);
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
        }
    }

    public void CreateLoot(string chosenLootTable, int amount)
    {
        lootTable = GameObject.Find("LootTable").GetComponent<LootTable>();
        // create a new list to add the loot too
        // add the appropriate amont of loot ids depending on its rarity to the list
        List<int> lootList = new List<int>();
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
            Debug.Log(lootList.ToString());
        }
        // now add the actual loot to this loot objects list
        Debug.Log("Amount: " + amount);
        myLoot = new List<int>();
        myLoot.Clear();
        for (int i = 0; i < amount; i++)
        {
            myLoot.Add(lootList[Random.Range(0, lootList.Count)]);
        }
        // clear the created list
        lootList.Clear();
    }
}
