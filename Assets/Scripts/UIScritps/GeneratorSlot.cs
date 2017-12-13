using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            UpdateGenerator(droppedEquipment.equipment.ID);
            // make sure the save data matches the current radar
            ShipData shipData = playerShip.GetComponent<ShipData>();
            shipData.generator = generatorData.ID;
        }
    }

    public void UpdateGenerator(int id)
    {
        generatorData = itemDatabase.FetchGeneratorByID(id);

        if (childName == "")
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + generatorData.Slug);
            equipmentObject.name = generatorData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(id);
            data.slotType = "Generator";
            data.ammount++;
        }

        childName = generatorData.Title;

        //set up all the things that are controlled by the radarData
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
        
