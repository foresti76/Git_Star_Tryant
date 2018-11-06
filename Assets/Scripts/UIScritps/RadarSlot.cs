using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadarSlot : MonoBehaviour, IDropHandler
{

    private Inventory inv;
    private ItemDatabase itemDatabase;
    Ship shipData;
    // Use this for initialization
    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject playerShip = GameObject.FindGameObjectWithTag("Player");
        itemDatabase = inv.GetComponent<ItemDatabase>();
        shipData = playerShip.GetComponent<Ship>();
        RadarData radarData = itemDatabase.FetchRadarByID(shipData.radar);

        if (this.transform.childCount == 1)
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + radarData.Slug);
            equipmentObject.name = radarData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(radarData.ID);
            data.slotType = "Radar";
            data.ammount++;
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
        if (droppedEquipment.equipment.Type == "Radar")
        {
            // swap out the current radar object in this slot and send it back to the inventory
            if (this.transform.childCount > 1)
            {
                Transform equipment = this.transform.GetChild(1);
                EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                currentEquipment.slot = droppedEquipment.slot;
                currentEquipment.slotType = "Inv";
                equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
            }


            droppedEquipment.slotType = "Radar";
            // set up thje current radar data based on the data from the object

            shipData.radar = droppedEquipment.equipment.ID;
            shipData.UpdateRadar(droppedEquipment.equipment.ID);
        }
    }
}
