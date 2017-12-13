using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RCSSlot : MonoBehaviour, IDropHandler
{

    public string childName;

    private Inventory inv;
    private GameObject playerShip;
    private RCS rcsData;
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
        if (droppedEquipment.equipment.Type == "RCS")
        {
            // swap out the current rcs object in this slot and send it back to the inventory
            if (childName != "")
            {
                Transform equipment = this.transform.Find(childName);
                EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                currentEquipment.slot = droppedEquipment.slot;
                currentEquipment.slotType = "Inv";
                equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
            }
            //this lets the equipment object know who its parent is
            droppedEquipment.slotType = "RCS";
            // set up thje current rcs data based on the data from the object

            UpdateRCS(droppedEquipment.equipment.ID);
            // make sure the save data matches the current rcs
            ShipData shipData = playerShip.GetComponent<ShipData>();
            shipData.rcs = rcsData.ID;
        }
    }

    public void UpdateRCS(int id)
    {
        rcsData = itemDatabase.FetchRCSByID(id);

        if (childName == "")
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + rcsData.Slug);
            equipmentObject.name = rcsData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(id);
            data.slotType = "RCS";
            data.ammount++;
        }

        childName = rcsData.Title;

        //set up all the things that are controlled by the radarData
        if (playerShip != null)
        {
            ShipMovement rcsScript = playerShip.GetComponent<ShipMovement>();
            rcsScript.rotateThrust = rcsData.Rot;
            rcsScript.rcsEnergyCost = rcsData.Energy_Cost;
            //Todo create signature when using rcs
        }
    }
}
        
