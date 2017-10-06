using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LootSlot : MonoBehaviour, IDropHandler {
    public int id;

    private Inventory inv;
    private Loot loot;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        loot = GameObject.Find("Loot").GetComponent<Loot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        // todo make sure that if you do not drop the loot on a slot it jumps back to the loot panel.
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
        if (inv.equipments[id].ID == -1)
        {
            loot.loot[droppedEquipment.slot] = new Equipment();
            inv.equipments[id] = droppedEquipment.equipment;
            droppedEquipment.slot = id;
        }
        else 
        {
            transform.localPosition = new Vector2(0, 0);
        }
    }
}
