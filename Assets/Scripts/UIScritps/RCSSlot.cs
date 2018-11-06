using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RCSSlot : MonoBehaviour, IDropHandler
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

        RCSData rcsData = itemDatabase.FetchRCSByID(shipData.rcs);
        if (this.transform.childCount == 1)
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + rcsData.Slug);
            equipmentObject.name = rcsData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(rcsData.ID);
            data.slotType = "RCS";
            data.ammount++;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
        if (droppedEquipment.equipment.Type == "RCS")
        {
            // swap out the current rcs object in this slot and send it back to the inventory
            if (this.transform.childCount > 1)
            {
                Transform equipment = this.transform.GetChild(1);
                EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                currentEquipment.slot = droppedEquipment.slot;
                currentEquipment.slotType = "Inv";
                equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
            }
            //this lets the equipment object know who its parent is
            droppedEquipment.slotType = "RCS";

            // make sure the save data matches the current rcs
            shipData.rcs = droppedEquipment.equipment.ID;
            shipData.UpdateRCS(droppedEquipment.equipment.ID);
        }
    }
}
        
