using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShieldSlot : MonoBehaviour, IDropHandler{

    public string childName;

    private Inventory inv;
    private GameObject playerShip;
    private Shield shieldData;
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
        if (droppedEquipment.equipment.Type == "Shield")
        {
            // swap out the current shield object in this slot and send it back to the inventory
            if (childName != "")
            {
                Transform equipment = this.transform.Find(childName);
                EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                currentEquipment.slot = droppedEquipment.slot;
                currentEquipment.slotType = "Inv";
                equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
            }

            // set up thje current shield data based on the data from the object
            droppedEquipment.slotType = "Shield";
            UpdateShield(droppedEquipment.equipment.ID);

            // make sure the save data matches the current engine
            ShipData shipData = playerShip.GetComponent<ShipData>();
            shipData.shield = shieldData.ID;

        }
    }

    public void UpdateShield(int id)
    {
        shieldData = itemDatabase.FetchShieldByID(id);

        if (childName == "")
        {
            GameObject equipmentObject = Instantiate(inv.inventoryItem);
            equipmentObject.transform.SetParent(this.transform, false);
            equipmentObject.transform.localPosition = new Vector2(0, 0);
            equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + shieldData.Slug);
            equipmentObject.name = shieldData.Title;
            EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
            data.equipment = itemDatabase.FetchEquipmentByID(id);
            data.slotType = "Shield";
            data.ammount++;
        }

        childName = shieldData.Title;

        //set up all the things that are controlled by the shieldData
        if (playerShip != null)
        {
            ShieldBehavior shieldScript = playerShip.GetComponentInChildren<ShieldBehavior>();
            shieldScript.maxShield = shieldData.Max_Shield;
            shieldScript.shieldRechageRate = shieldData.Regen_Rate;
            shieldScript.shieldRechargeDuration = shieldData.Regen_Delay;
            shieldScript.shieldRefreshDuration = shieldData.Refresh_Delay;
            shieldScript.rechargeEnergyCost = shieldData.Recharge_Energy_Cost;
            shieldScript.maintEnergyCost = shieldData.Maint_Energy_Cost;

            //Todo create signature when using shield
        }
    }
}

