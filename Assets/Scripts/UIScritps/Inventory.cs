﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameObject inventoryPanel;
    public GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    public PlayerRecord playerRecord;

    public ItemDatabase equipmentDatbase;
    private int slotAmmount;


    public List<Equipment> equipments = new List<Equipment>();
    public List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        playerRecord = GameObject.Find("PlayerRecord").GetComponent<PlayerRecord>();
        equipmentDatbase = GetComponent<ItemDatabase>(); 
        slotAmmount = 20;
        for (int i = 0; i < slotAmmount; i++)
        {
            equipments.Add(new Equipment());
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform, false);
            slots[i].GetComponent<InventorySlot>().id = i ;
        }
        // remove once we can actually populate the inventory with real stuff
        AddEquipment(90);
        AddEquipment(88);
        AddEquipment(85);
        AddEquipment(81);
        AddEquipment(82);
        AddEquipment(80);
        AddEquipment(90);
        AddEquipment(90);
        AddEquipment(90);

    }
    public void AddEquipment(int id)
    {
        Equipment equipmentToAdd = equipmentDatbase.FetchEquipmentByID(id);
        if (equipmentToAdd.Stackable && (CheckIfEquipmentIsInInventory(equipmentToAdd) > -1))
        {
            EquipmentData data = slots[CheckIfEquipmentIsInInventory(equipmentToAdd)].transform.GetChild(0).GetComponent<EquipmentData>();
            data.ammount++ ;
            data.transform.GetChild(0).GetComponent<Text>().text = data.ammount.ToString();
            data.transform.GetChild(1).GetComponent<Text>().text = data.equipment.Cost.ToString();
        }
        else
        { 
            for (int i = 0; i < equipments.Count; i++)
            {
                if(equipments[i].ID == -1)
                {
                    equipments[i] = equipmentToAdd;
                    GameObject equipmentObject = Instantiate(inventoryItem);
                    equipmentObject.transform.SetParent(slots[i].transform, false);
                    //InventorySlot invSlot = equipmentObject.transform.parent.GetComponent<InventorySlot>();
                    equipmentObject.transform.localPosition = new Vector2(0,0);
                    equipmentObject.GetComponent<Image>().sprite = equipmentToAdd.Sprite;
                    equipmentObject.name = equipmentToAdd.Title;
                    EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
                    data.equipment = equipmentToAdd;
                    data.slotType = "Inv";
                    data.transform.GetChild(1).GetComponent<Text>().text = data.equipment.Cost.ToString();
                    data.ammount++;
                    data.slot = i;
                    break;
                }
            }
        }
    }

    int CheckIfEquipmentIsInInventory(Equipment equipment)
    {
        for (int i = 0; i < equipments.Count; i++)
        {
            //todo set a variable that can be accessed rather than having to iterate through them all every time.
            if (equipments[i].ID == equipment.ID)
            {
                return i;
            }
        }
        return -1;
    }
}
