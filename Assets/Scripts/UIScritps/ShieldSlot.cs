using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShieldSlot : MonoBehaviour, IDropHandler{

    private Inventory inv;
    private GameObject playerShip;
    private ShieldData shieldData;
    private ItemDatabase itemDatabase;
    Ship shipData;

    // Use this for initialization
    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        itemDatabase = inv.GetComponent<ItemDatabase>();
        shipData = playerShip.GetComponent<Ship>();

        shieldData = itemDatabase.FetchShieldByID(shipData.shield);

        if (this.transform.childCount == 1)
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + shieldData.Slug);
            equipmentObject.name = shieldData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(shieldData.ID);
            data.slotType = "Shield";
            data.ammount++;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
        if (droppedEquipment.equipment.Type == "Shield")
        {
            // swap out the current shield object in this slot and send it back to the inventory
            if (this.transform.childCount > 1)
            {
                Transform equipment = this.transform.GetChild(1);
                EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                currentEquipment.slot = droppedEquipment.slot;
                currentEquipment.slotType = "Inv";
                equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
            }

            // set up thje current shield data based on the data from the object
            droppedEquipment.slotType = "Shield";

            // make sure the save data matches the current engine
            shipData.shield = droppedEquipment.equipment.ID;
            shipData.UpdateShield(droppedEquipment.equipment.ID);

        }
    }
}

