using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {

    public int id;

    private Inventory inv;
    private LootPanel lootPanelControl;
    private Shop shop;

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

            if (droppedEquipment.slotType == "Shop")  
            {
                //need to clear out the old shop slot if it has anything in it.
                // if they are pulling from the shop check to see if they have enough money and if they don't return the item to the shop 
                // if they do have enough money take that money away and then they have the item in thier inventory
                if(inv.playerRecord.playerMoney < droppedEquipment.equipment.Cost)
                {
                    return;
                }
                else
                {
                    inv.playerRecord.SpendMoney(droppedEquipment.equipment.Cost);
                }
            }

            inv.equipments[id] = droppedEquipment.equipment;
            droppedEquipment.slot = id;
            droppedEquipment.slotType = "Inv";
        }
        else if (inv.equipments[id].ID != -1 && droppedEquipment.slotType == "Inv" && droppedEquipment.slot != this.id)
        {
            //todo seems to be a bug with pulling in from the weapon slot here.
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
