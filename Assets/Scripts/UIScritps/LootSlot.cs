using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LootSlot : MonoBehaviour, IDropHandler {
    public int id;

    private LootPanel loot;

    void Start()
    {
        loot = GameObject.Find("LootPanelControl").GetComponent<LootPanel>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        // todo make sure that if you do not drop the loot on a slot it jumps back to the loot panel.
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();

        if(droppedEquipment.slotType != "Loot")
        {
            return;
        }

        if (loot.loot[id].ID == -1)
        {
            loot.loot[droppedEquipment.slot] = new Equipment();
            loot.loot[id] = droppedEquipment.equipment;
            droppedEquipment.slot = id;
        }
    }
}
