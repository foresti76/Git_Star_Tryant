using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HullSlot : MonoBehaviour, IDropHandler {

    private Inventory inv;
    private GameObject playerShip;
    private Ship shipData;
    private ItemDatabase itemDatabase;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        itemDatabase = inv.GetComponent<ItemDatabase>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
        if (droppedEquipment.equipment.Type == "Ship")
        {
            // todo swap out the current ship object in this slot and send it back to the inventory
            droppedEquipment.slotType = "Ship";
            shipData = itemDatabase.FetchShipByID(droppedEquipment.equipment.ID);
            //set up all the things that are controlled by the shipData
            Hull hull = playerShip.GetComponent<Hull>();
            hull.maxHull = shipData.Hullpoints;
            hull.armor = shipData.Armor;

            // todo add the appropriate ammount of weapon and subsystem slots to the ship customization window.
            // todo change the ship model to match the current ship using slug.

        } else
        {
            return; //reject the object and send it back to the inventory slot it came from
        }
    }

}
