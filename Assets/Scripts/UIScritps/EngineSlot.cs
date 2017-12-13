using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EngineSlot : MonoBehaviour, IDropHandler{

    public string childName;

    private Inventory inv;
    private GameObject playerShip;
    private Engine engineData;
    private ItemDatabase itemDatabase;

    // Use this for initialization
    void Start () {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        itemDatabase = inv.GetComponent<ItemDatabase>();
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

            // set up thje current engine data based on the data from the object
            droppedEquipment.slotType = "Engine";

            //set up all the things that are controlled by the engineData
            UpdateEngine(droppedEquipment.equipment.ID);
            // make sure the save data matches the current engine
            ShipData shipData = playerShip.GetComponent<ShipData>();
            shipData.engine = engineData.ID;
        }
    }

    public void UpdateEngine(int id)
    {
        engineData = itemDatabase.FetchEngineByID(id);

        if(childName == "")
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + engineData.Slug);
            equipmentObject.name = engineData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(id);
            data.slotType = "Engine";
            data.ammount++;
        }

        childName = engineData.Title;

        if (playerShip != null)
        {

            //Set up the engine stuff
            ShipMovement engineScript = playerShip.GetComponent<ShipMovement>();
            engineScript.engineThrust = engineData.Acceleration;
            engineScript.reverseThrust = engineData.Acceleration / 2;
            engineScript.maxSpeed = engineData.Combat_Speed;
            engineScript.engineEnergyCost = engineData.Energy_Cost;
            // todo add cruising speed engineScript.cruiseSpeed = engineData.Crusing_Speed;
            // Todo use power from the generator when using the engine
            //Todo create signature when using engine
        }
    }

}
