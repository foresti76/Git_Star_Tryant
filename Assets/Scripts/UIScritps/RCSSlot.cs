using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

            // set up thje current rcs data based on the data from the object
            droppedEquipment.slotType = "RCS";
            rcsData = itemDatabase.FetchRCSByID(droppedEquipment.equipment.ID);
            childName = rcsData.Title;

            //set up all the things that are controlled by the rcsData
            if (playerShip != null)
            {
                ShipMovement rcsScript = playerShip.GetComponent<ShipMovement>();
                rcsScript.rotateThrust = rcsData.Rot;
                rcsScript.rcsEnergyCost = rcsData.Energy_Cost;
                //Todo create signature when using rcs
            }
        }
    }
}
