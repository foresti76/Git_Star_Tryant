using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour, IDropHandler {

    public int id;
    public int affinity;

    private Inventory inv;
    private Shop shop;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        affinity = 2;
    }

    public void OnDrop(PointerEventData eventData)
    {
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();

        if (this.transform.childCount == 0)
        {
            if (droppedEquipment.slotType == "Shop")
            {
                return;
            }

            //clear the old inventory slot if you are moving to an empty one from within the inventory
            if (droppedEquipment.slotType == "Inv")
            {
                inv.equipments[droppedEquipment.slot] = new Equipment();
            }

            inv.playerRecord.GiveMoney(droppedEquipment.equipment.Cost / affinity);
            droppedEquipment.slot = id;
            droppedEquipment.slotType = "Shop";
        }
    }

}
