using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ECMSlot : MonoBehaviour, IDropHandler{

    public string childName;

    private Inventory inv;
    private GameObject playerShip;
    private ECM ecmData;
    private ItemDatabase itemDatabase;

    // Use this for initialization
    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        itemDatabase = inv.GetComponent<ItemDatabase>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
        if (droppedEquipment.equipment.Type == "ECM")
        {
            // swap out the current ecm object in this slot and send it back to the inventory
            if (childName != "")
            {
                Transform equipment = this.transform.Find(childName);
                EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                currentEquipment.slot = droppedEquipment.slot;
                currentEquipment.slotType = "Inv";
                equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
            }

            // set up thje current ecm data based on the data from the object
            droppedEquipment.slotType = "ECM";
            UpdateECM(droppedEquipment.equipment.ID);
            ShipData shipData = playerShip.GetComponent<ShipData>();
            shipData.ecm = ecmData.ID;
        }
    }

    public void UpdateECM(int id)
    {
        ecmData = itemDatabase.FetchECMByID(id);

        if (childName == "")
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + ecmData.Slug);
            equipmentObject.name = ecmData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(id);
            data.slotType = "ECM";
            data.ammount++;
        }

        childName = ecmData.Title;

        //set up all the things that are controlled by the ecmData
        if (playerShip != null)
            {

            // make sure the save data matches the current engine

            //Todo set up the data that makes an ecm on the ship
        }
    }
}
