using System.Collections;
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
    private GameObject playerShip;
    private Ship shipData;
    private ItemDatabase itemDatabase;
    private int smWeaponSlotAmmount;
    private int medWeaponSlotAmmount;
    private int lgWeaponSlotAmmount;
    private int subsystemsSlotAmmount;

    public List<GameObject> weaponSlots = new List<GameObject>();
    public List<GameObject> subsystemSlots = new List<GameObject>();

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        itemDatabase = inv.GetComponent<ItemDatabase>();
        //  Todo UpdatePanels(); once we have data loaded at the start for the ship
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

            // set up thje current ship data based on the data from the object
            droppedEquipment.slotType = "Ship";
            shipData = itemDatabase.FetchShipByID(droppedEquipment.equipment.ID);
            childName = shipData.Title;

            //set up all the things that are controlled by the shipData
            if(playerShip != null)
            { 
            Hull hull = playerShip.GetComponent<Hull>();
            hull.maxHull = shipData.Hullpoints;
            hull.armor = shipData.Armor;
            }
            UpdatePanels();
            // todo change the ship model to match the current ship using slug.

        } else
        {
            return; //reject the object and send it back to the inventory slot it came from
        }
    }

    void UpdatePanels()
    {

        smWeaponSlotAmmount = shipData.Sm_Hardpoints;
        medWeaponSlotAmmount = shipData.Med_Hardpoints;
        lgWeaponSlotAmmount = shipData.Lg_Hardpoints;
        subsystemsSlotAmmount = shipData.Subsystems;

        //todo destroy all the current children for weapons and subsystems and put the leftover items in the inventory.

        if (smWeaponSlotAmmount > 0) { 
            for (int i = 0; i < smWeaponSlotAmmount; i++)
            {
                weaponSlots.Add(Instantiate(weaponSlot));
                weaponSlots[i].transform.SetParent(weaponsPanel.transform, false);
                WeaponSlot weaponSlotScript = weaponSlots[i].GetComponent<WeaponSlot>();
                weaponSlotScript.weaponSlotSize = "small";
                weaponSlotScript.id = i;
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
                weaponSlotScript.id = i;
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
                weaponSlotScript.id = i;
                Text slotSize = weaponSlots[i].GetComponentInChildren<Text>();
                slotSize.text = weaponSlotScript.weaponSlotSize;
            }
        }

        for (int i = 0; i < subsystemsSlotAmmount; i++)
        {
            subsystemSlots.Add(Instantiate(subsystemSlot));
            subsystemSlots[i].transform.SetParent(subsystemPanel.transform, false);
        }
    }
}
