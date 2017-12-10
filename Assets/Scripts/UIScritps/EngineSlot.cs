using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            engineData = itemDatabase.FetchEngineByID(droppedEquipment.equipment.ID);
            childName = engineData.Title;

            //set up all the things that are controlled by the engineData
            if (playerShip != null)
            {
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
}
