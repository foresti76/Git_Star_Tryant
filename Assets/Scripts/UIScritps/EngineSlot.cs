using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EngineSlot : MonoBehaviour, IDropHandler{

    public string childName;

    private Inventory inv;
    private ItemDatabase itemDatabase;
    ShipData shipData;

    // Use this for initialization
    void Start () {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject playerShip = GameObject.FindGameObjectWithTag("Player");
        itemDatabase = inv.GetComponent<ItemDatabase>();
        shipData = playerShip.GetComponent<ShipData>();

        Engine engineData = itemDatabase.FetchEngineByID(shipData.engine);

        if (childName == "")
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + engineData.Slug);
            equipmentObject.name = engineData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(engineData.ID);
            data.slotType = "Engine";
            data.ammount++;
            childName = engineData.Title;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
        if (droppedEquipment.equipment.Type == "Engine")
        {
            // swap out the current engine object in this slot and send it back to the inventory
            if (childName != "")
            {
                Transform equipment = this.transform.Find(childName);
                EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                currentEquipment.slot = droppedEquipment.slot;
                currentEquipment.slotType = "Inv";
                equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
            }

            // set the equipment to be parented correctly
            droppedEquipment.slotType = "Engine";
            childName = droppedEquipment.equipment.Title;

            // make sure the save data matches the current engine
            shipData.engine = droppedEquipment.equipment.ID;
            shipData.UpdateEngine(droppedEquipment.equipment.ID);
        }
    }
}
