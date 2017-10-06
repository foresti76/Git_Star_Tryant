using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {

    public int id;

    private Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
        if (inv.equipments[id].ID == -1)
        {
            inv.equipments[droppedEquipment.slot] = new Equipment();
            inv.equipments[id] = droppedEquipment.equipment;
            droppedEquipment.slot = id;
        }
        else if (inv.equipments[id].ID != -1)
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
