using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneratorSlot : MonoBehaviour, IDropHandler{

    public string childName;

    private Inventory inv;
    private GameObject playerShip;
    private Generator generatorData;
    private ItemDatabase itemDatabase;

    // Use this for initialization
    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        itemDatabase = inv.GetComponent<ItemDatabase>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
        if (droppedEquipment.equipment.Type == "Generator")
        {
            // swap out the current generator object in this slot and send it back to the inventory
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
            droppedEquipment.slotType = "Generator";
            generatorData = itemDatabase.FetchGeneratorByID(droppedEquipment.equipment.ID);
            childName = generatorData.Title;

            //set up all the things that are controlled by the generatorData
            if (playerShip != null)
            {
                ShipGenerator generatorScript = playerShip.GetComponent<ShipGenerator>();
                generatorScript.maxPower = generatorData.Storage_Capacity;
                generatorScript.regenRate = generatorData.Energy_Generation;
                generatorScript.currentPower = generatorScript.maxPower;
                //Todo create signature when using generator
            }
        }
    }
}
