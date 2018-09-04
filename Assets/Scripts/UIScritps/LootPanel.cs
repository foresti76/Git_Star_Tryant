using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootPanel : MonoBehaviour
{

    public GameObject lootPanel;
    public GameObject slotPanel;
    public GameObject lootSlot;
    public GameObject lootItem;

    ItemDatabase equipmentDatbase;
    private int currentLoot = 0;
    private Inventory inv;

    public List<Equipment> loot = new List<Equipment>();
    public List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        equipmentDatbase = GameObject.Find("Inventory").GetComponent<ItemDatabase>();

        lootPanel.SetActive(false);
        // remove once we can actually populate the inventory with real stuff
        //AddLoot(4);
        //AddLoot(5);
        //AddLoot(6);
        //AddLoot(6);
        
    }

    public void AddLoot(int id)
    {
        lootPanel.SetActive(true);

        //create a new slot for the loot
        loot.Add(new Equipment());
        slots.Add(Instantiate(lootSlot));
        slots[currentLoot].transform.SetParent(slotPanel.transform, false);
        slots[currentLoot].GetComponent<LootSlot>().id = currentLoot;
        currentLoot ++;

        Equipment lootToAdd = equipmentDatbase.FetchEquipmentByID(id);
        if (lootToAdd.Stackable && (CheckIfEquipmentIsInLoot(lootToAdd) > -1))
        {
            EquipmentData data = slots[CheckIfEquipmentIsInLoot(lootToAdd)].transform.GetChild(0).GetComponent<EquipmentData>();
            data.ammount++;
            data.transform.GetChild(0).GetComponent<Text>().text = data.ammount.ToString();
        }
        else
        {
            for (int i = 0; i < loot.Count; i++)
            {
                if (loot[i].ID == -1)
                {
                    loot[i] = lootToAdd;
                    GameObject equipmentObject = Instantiate(lootItem);
                    equipmentObject.transform.SetParent(slots[i].transform, false);             
                    equipmentObject.GetComponent<Image>().sprite = lootToAdd.Sprite;
                    equipmentObject.name = lootToAdd.Title;
                    equipmentObject.transform.localPosition = new Vector2(0,0);
                    EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
                    data.equipment = lootToAdd;
                    data.slotType = "Loot";
                    data.ammount++;
                    data.slot = i;
                    break;
                }
            }
        }
     }

    int CheckIfEquipmentIsInLoot(Equipment equipment)
    {
        for (int i = 0; i < loot.Count; i++)
        {
            //todo set a variable that can be accessed rather than having to iterate through them all every time.
            if (loot[i].ID == equipment.ID)
                return i;
        }
        return -1;
    }
    
    // Take all the loot and hide the loot panel

    public void TakeAll()
    {
        //iterate through all the loot items and give them to the inventory
        for(int i = 0; i < slots.Count ; i++)
        {
            //Just go through all the slots and give them to the inventory
            if (loot[i].ID != -1)
            {
                inv.AddEquipment(loot[i].ID);
                //check to see if there is more than one in the ammount field of the loot and add more.
                EquipmentData data = slots[i].gameObject.transform.GetChild(0).GetComponent<EquipmentData>();

                if (data.ammount > 1)
                {
                    for(int j = 0; j < (data.ammount -1) ; j++)
                    {
                        inv.AddEquipment(loot[i].ID);
                    }
                }
                //remove the current equipment and gameobject from loot and slots.
                loot[i] = new Equipment();
            }
            GameObject slotToDestroy = slots[i].gameObject;
            Destroy(slotToDestroy);
        }
        currentLoot = 0;
        lootPanel.SetActive(false);
    }
}

