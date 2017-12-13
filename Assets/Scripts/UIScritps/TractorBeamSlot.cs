using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TractorBeamSlot : MonoBehaviour, IDropHandler{

    public string childName;

    private Inventory inv;
    private GameObject playerShip;
    private TractorBeam tractorBeamData;
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
        if (droppedEquipment.equipment.Type == "TractorBeam")
        {
            // swap out the current tractorBeam object in this slot and send it back to the inventory
            if (childName != "")
            {
                Transform equipment = this.transform.Find(childName);
                EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                currentEquipment.slot = droppedEquipment.slot;
                currentEquipment.slotType = "Inv";
                equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
            }

            // set up the current TractorBeam data based on the data from the object
            droppedEquipment.slotType = "TractorBeam";
            UpdateTractorBeam(droppedEquipment.equipment.ID);
            // make sure the save data matches the current TractorBeam
            ShipData shipData = playerShip.GetComponent<ShipData>();
            shipData.tractorbeam = tractorBeamData.ID;
        }
    }
    public void UpdateTractorBeam(int id)
    {
        tractorBeamData = itemDatabase.FetchTractorBeamByID(id);

        if (childName == "")
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + tractorBeamData.Slug);
            equipmentObject.name = tractorBeamData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(id);
            data.slotType = "TractorBeam";
            data.ammount++;
        }

        childName = tractorBeamData.Title;

        //set up all the things that are controlled by theTractorBeam
        if (playerShip != null)
        {

        }
    }
}
