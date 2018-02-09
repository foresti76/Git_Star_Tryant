﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HullSlot : MonoBehaviour, IDropHandler {

    public GameObject weaponsPanel;
    public GameObject weaponSlot;
    public GameObject subsystemPanel;
    public GameObject subsystemSlot;
    public string childName;

    private Inventory inv;
    private ItemDatabase itemDatabase;
    private int smWeaponSlotAmmount;
    private int medWeaponSlotAmmount;
    private int lgWeaponSlotAmmount;
    private int subsystemsSlotAmmount;
    ShipData shipData;

    public List<GameObject> weaponSlots = new List<GameObject>();
    public List<GameObject> subsystemSlots = new List<GameObject>();

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        itemDatabase = inv.GetComponent<ItemDatabase>();

        GameObject playerShip = GameObject.FindGameObjectWithTag("Player");
        shipData = playerShip.GetComponent<ShipData>();

        Ship hullData = itemDatabase.FetchShipByID(shipData.hullID);

        if (childName == "")
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + hullData.Slug);
            equipmentObject.name = hullData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(hullData.ID);
            data.slotType = "Ship";
            data.ammount++;
            childName = hullData.Title;
            UpdatePanels(hullData.ID);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
        if (droppedEquipment.equipment.Type == "Ship")
        {
            // swap out the current ship object in this slot and send it back to the inventory
            if (childName != "")
            {
                Transform equipment = this.transform.Find(childName);
                EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                currentEquipment.slot = droppedEquipment.slot;
                currentEquipment.slotType = "Inv";
                equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
            }

            // set up the equipment object to have the correct parent
            droppedEquipment.slotType = "Ship";
            childName = droppedEquipment.equipment.Title;

            // set up thje current ship data based on the data from the object
            shipData.hullID = droppedEquipment.equipment.ID;
            shipData.UpdateHull(droppedEquipment.equipment.ID);
            UpdatePanels(droppedEquipment.equipment.ID);
        }
    }

           void UpdatePanels(int id)
    {
        Ship hullData = itemDatabase.FetchShipByID(id);

        smWeaponSlotAmmount = hullData.Sm_Hardpoints;
        medWeaponSlotAmmount = hullData.Med_Hardpoints;
        lgWeaponSlotAmmount = hullData.Lg_Hardpoints;
        subsystemsSlotAmmount = hullData.Subsystems;

        //todo destroy all the current children for weapons and subsystems and put the leftover items in the inventory.

        if (smWeaponSlotAmmount > 0) { 
            for (int i = 0; i < smWeaponSlotAmmount; i++)
            {
                weaponSlots.Add(Instantiate(weaponSlot));
                weaponSlots[i].transform.SetParent(weaponsPanel.transform, false);
                WeaponSlot weaponSlotScript = weaponSlots[i].GetComponent<WeaponSlot>();
                weaponSlotScript.weaponSlotSize = "small";
                weaponSlotScript.slotId = i;
                Text slotSize = weaponSlots[i].GetComponentInChildren<Text>();
                slotSize.text = weaponSlotScript.weaponSlotSize;
            }
        }
        if (medWeaponSlotAmmount > 0)
        { 
            for (int i = 0; i < medWeaponSlotAmmount; i++)
            {
                weaponSlots.Add(Instantiate(weaponSlot));
                weaponSlots[i].transform.SetParent(weaponsPanel.transform, false);
                WeaponSlot weaponSlotScript = weaponSlots[i].GetComponent<WeaponSlot>();
                weaponSlotScript.weaponSlotSize = "medium";
                weaponSlotScript.slotId = i;
                Text slotSize = weaponSlots[i].GetComponentInChildren<Text>();
                slotSize.text = weaponSlotScript.weaponSlotSize;
            }
        }
        if (lgWeaponSlotAmmount > 0)
        {
            for (int i = 0; i < smWeaponSlotAmmount; i++)
            {
                weaponSlots.Add(Instantiate(weaponSlot));
                weaponSlots[i].transform.SetParent(weaponsPanel.transform, false);
                WeaponSlot weaponSlotScript = weaponSlots[i].GetComponent<WeaponSlot>();
                weaponSlotScript.weaponSlotSize = "large";
                weaponSlotScript.slotId = i;
                Text slotSize = weaponSlots[i].GetComponentInChildren<Text>();
                slotSize.text = weaponSlotScript.weaponSlotSize;
            }
        }

        if (subsystemsSlotAmmount > 0)
        {
            for (int i = 0; i < subsystemsSlotAmmount; i++)
            {
                subsystemSlots.Add(Instantiate(subsystemSlot));
                subsystemSlots[i].transform.SetParent(subsystemPanel.transform, false);
            }
        }
    }
}