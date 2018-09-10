using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {

    public int id;

    private Inventory inv;
    private LootPanel lootPanelControl;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        lootPanelControl = GameObject.Find("LootPanelControl").GetComponent<LootPanel>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();

        if (inv.equipments[id].ID == -1)
        {
            //clear the old inventory slot if you are moving to an empty one from within the inventory
            if (droppedEquipment.slotType == "Inv")
            {
                inv.equipments[droppedEquipment.slot] = new Equipment();
            }

            // clear the loot data out if you are pulling from the loot box
            if (droppedEquipment.slotType == "Loot")
            {
                lootPanelControl.loot[droppedEquipment.slot] = new Equipment();
                //remove the loot from the lootObject;
                lootPanelControl.currentLootObject.myLoot.Remove(droppedEquipment.equipment.ID);
            }

            inv.equipments[id] = droppedEquipment.equipment;
            droppedEquipment.slot = id;
            droppedEquipment.slotType = "Inv";
        }
        else if (inv.equipments[id].ID != -1 && droppedEquipment.slotType == "Inv" && droppedEquipment.slot != this.id)
        {
            Transform equipment = this.transform.GetChild(0);
            //todo make a getter setter method to move items to new slots
            equipment.GetComponent<EquipmentData>().slot = droppedEquipment.slot;
            equipment.transform.SetParent(inv.slots[droppedEquipment.slot].transform);
            equipment.transform.position = inv.slots[droppedEquipment.slot].transform.position;

            droppedEquipment.slot = id;

            inv.equipments[id] = droppedEquipment.equipment;
            inv.equipments[droppedEquipment.slot] = equipment.GetComponent<EquipmentData>().equipment;
        }
    }

}
