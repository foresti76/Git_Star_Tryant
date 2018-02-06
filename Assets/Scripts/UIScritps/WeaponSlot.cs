using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponSlot : MonoBehaviour, IDropHandler
{
    public string childName;
    public int slotId;
    public string weaponSlotSize;  

    private Inventory inv;
    private ItemDatabase itemDatabase;
    public WeaponController myWeaponController;
    ShipData shipData;

    private WeaponSlot[] weaponSlotList;
    WeaponController[] weaponControllerList;
    // Use this for initialization
    void Awake()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        GameObject playerShip = GameObject.FindGameObjectWithTag("Player");
        itemDatabase = inv.GetComponent<ItemDatabase>();
        shipData = playerShip.GetComponent<ShipData>();

        //find the weapon associated with this slot and put in the data
        if (playerShip != null)
        {
            weaponControllerList = playerShip.GetComponentsInChildren<WeaponController>();
            GameObject weaponPanel = GameObject.Find("WeaponsLayout");
            weaponSlotList = weaponPanel.GetComponentsInChildren<WeaponSlot>();

            // WeaponController currentWeaponController = weaponControllerList[id];
            Weapon weaponData = itemDatabase.FetchWeaponByID(shipData.weaponList[slotId]);

            if (childName == "")
            {
                GameObject equipmentObject = Instantiate(inv.inventoryItem);
                equipmentObject.transform.SetParent(this.transform, false);
                equipmentObject.transform.localPosition = new Vector2(0, 0);
                equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + weaponData.Slug);
                equipmentObject.name = weaponData.Title;
                EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
                data.equipment = itemDatabase.FetchEquipmentByID(weaponData.ID);
                data.slotType = "Weapon";
                data.ammount++;
                childName = weaponData.Title;
            }

            for (int i = 0; i < weaponControllerList.Length; i++)
            {
                WeaponController currentWeaponController = weaponControllerList[i].GetComponent<WeaponController>();
                if (currentWeaponController.slotID == slotId)
                {
                    myWeaponController = currentWeaponController;
                    return;
                }

            }
        }
    }

    //when a weapon is dropped on a slot do all the things to update the weapon perameters
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData);
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
         if (droppedEquipment.equipment.Type == "Weapon")
         {
            Weapon weaponData = itemDatabase.FetchWeaponByID(droppedEquipment.equipment.ID);
            //Debug.Log(weaponData.Mount_Size);
            if (weaponData.Mount_Size == weaponSlotSize)
            {
                // swap out the current ship object in this slot and send it back to the inventory
                if (childName != "")
                {
                    //Debug.Log("adding new weapon to slot " + id + " that has a weapon in it");
                    //get the current weapon and send it back to the inventory in the slot that the dropped one was in
                    Transform equipment = this.transform.Find(childName);
                    EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                    if (droppedEquipment.slotType == "Inv")
                    { 
                        currentEquipment.slot = droppedEquipment.slot;
                        currentEquipment.slotType = "Inv";

                        //make the dropped equipment parent to this slot
                        equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                        equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
                    }

                    //if you are swapping weapons between slots then just switch them
                    if(droppedEquipment.slotType == "Weapon")
                    {
                        currentEquipment.slot = droppedEquipment.slot;
                        currentEquipment.transform.SetParent(droppedEquipment.transform.parent);
                        currentEquipment.transform.position = droppedEquipment.transform.parent.position;
                        
                        //todo figure out a way to update the weapon controller to make the weaponslot update its data when the weapons are switched
                        //todo I see a potential exploit here where you can switch between different size slots.  Need to check to see if the recieveing slot is ok for the current weapon.
                        WeaponSlot sendingWeaponSlot = weaponSlotList[droppedEquipment.slot];
                        sendingWeaponSlot.childName = currentEquipment.equipment.Title;
                        sendingWeaponSlot.shipData.weaponList[sendingWeaponSlot.slotId] = currentEquipment.equipment.ID;
                        sendingWeaponSlot.shipData.UpdateWeapon(currentEquipment.equipment.ID, sendingWeaponSlot.myWeaponController);
                    }
                }

                if(childName == "" && droppedEquipment.slotType == "Weapon")
                {
                    WeaponSlot sendingWeaponSlot = weaponSlotList[droppedEquipment.slot];
                    sendingWeaponSlot.shipData.ClearWeapon(sendingWeaponSlot.myWeaponController);
                }
                // set up thje current ship data based on the data from the object
                //Debug.Log("adding " + weaponData.Title);
                droppedEquipment.slotType = "Weapon";
                droppedEquipment.slot = slotId;
                childName = droppedEquipment.equipment.Title;

                shipData.weaponList[slotId] = droppedEquipment.equipment.ID;
                shipData.UpdateWeapon(droppedEquipment.equipment.ID, myWeaponController);
            }
        }
    }
}
