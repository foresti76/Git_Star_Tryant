using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadarSlot : MonoBehaviour, IDropHandler
{

    public string childName;

    private Inventory inv;
    private GameObject playerShip;
    private Radar radarData;
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
        if (droppedEquipment.equipment.Type == "Radar")
        {
            // swap out the current radar object in this slot and send it back to the inventory
            if (childName != "")
            {
                Transform equipment = this.transform.Find(childName);
                EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                currentEquipment.slot = droppedEquipment.slot;
                currentEquipment.slotType = "Inv";
                equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
            }


            droppedEquipment.slotType = "Radar";
            // set up thje current radar data based on the data from the object
            UpdateRadar(droppedEquipment.equipment.ID);
            ShipData shipData = playerShip.GetComponent<ShipData>();
            shipData.radar = radarData.ID;
        }
    }

    public void UpdateRadar(int id)
    {
        radarData = itemDatabase.FetchRadarByID(id);

        if (childName == "")
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + radarData.Slug);
            equipmentObject.name = radarData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(id);
            data.slotType = "Radar";
            data.ammount++;
        }

        childName = radarData.Title;

        //set up all the things that are controlled by the radarData
        if (playerShip != null)
        {
            // make sure the save data matches the current radar

        }
    }
}
