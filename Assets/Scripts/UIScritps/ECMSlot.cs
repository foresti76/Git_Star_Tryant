using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ECMSlot : MonoBehaviour, IDropHandler{

    private Inventory inv;
    private ItemDatabase itemDatabase;
    Ship ship;

    // Use this for initialization
    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject playerShip = GameObject.FindGameObjectWithTag("Player");
        itemDatabase = inv.GetComponent<ItemDatabase>();
        ship = playerShip.GetComponent<Ship>();

        ECMData ecmData = itemDatabase.FetchECMByID(ship.ecm);

        if (this.transform.childCount == 1)
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + ecmData.Slug);
            equipmentObject.name = ecmData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(ecmData.ID);
            data.slotType = "ECM";
            data.ammount++;
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
        if (droppedEquipment.equipment.Type == "ECM")
        {
            // swap out the current ecm object in this slot and send it back to the inventory
            if (this.transform.childCount > 1)
            {
                Transform equipment = this.transform.GetChild(1);
                EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                currentEquipment.slot = droppedEquipment.slot;
                currentEquipment.slotType = "Inv";
                equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
            }

            // set up thje current ecm data based on the data from the object
            droppedEquipment.slotType = "ECM";

            ship.ecm = droppedEquipment.equipment.ID;
            ship.UpdateECM(droppedEquipment.equipment.ID);
        }
    }
}
