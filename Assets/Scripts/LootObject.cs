using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : MonoBehaviour
{
    public List<int> myLoot = new List<int>();
    public bool isBeingTractored = false;
    LootPanel lootPanel;

    // Use this for initialization
    void Start()
    {
        lootPanel = GetComponent<LootPanel>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isBeingTractored)
        {
            foreach (int loot in myLoot)
            {
                lootPanel.AddLoot(loot);
            }

            lootPanel.OpenLootPanel();
        }
    }
}
