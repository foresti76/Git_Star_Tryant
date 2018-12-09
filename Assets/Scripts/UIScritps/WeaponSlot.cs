using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponSlot : MonoBehaviour, IDropHandler
{
    public int slotId;
    public int weaponSlotSize;  

    private Inventory inv;
    private ItemDatabase itemDatabase;
    public WeaponController myWeaponController;
    Ship shipData;

    public WeaponSlot[] weaponSlotList;
    public WeaponController[] weaponControllerList;
    // Use this for initialization
    void Start()
    {
        GameObject playerShip = GameObject.FindGameObjectWithTag("Player");
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        itemDatabase = inv.GetComponent<ItemDatabase>();
        GameObject weaponPanel = GameObject.Find("WeaponsLayout");
        weaponSlotList = weaponPanel.GetComponentsInChildren<WeaponSlot>();

        //find the weapon associated with this slot and put in the data
        if (playerShip != null)
        {
 

            shipData = playerShip.GetComponent<Ship>();

            weaponControllerList = playerShip.GetComponentsInChildren<WeaponController>();


            //WeaponController currentWeaponController = weaponControllerList[id];
            WeaponData weaponData = itemDatabase.FetchWeaponByID(shipData.weaponList[slotId]);

            if (this.transform.childCount == 1)
            {
                GameObject equipmentObject = Instantiate(inv.inventoryItem);
                equipmentObject.transform.SetParent(this.transform, false);
                equipmentObject.transform.localPosition = new Vector2(0, 0);
                equipmentObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + weaponData.Slug);
                equipmentObject.name = weaponData.Title;
                EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
                data.equipment = itemDatabase.FetchEquipmentByID(weaponData.ID);
                data.slotType = "Weapon";
                data.slot = slotId;
                data.ammount++;
            }
           myWeaponController = weaponControllerList[slotId].GetComponent<WeaponController>();   
        }
    }


    //when a weapon is dropped on a slot do all the things to update the weapon perameters
    public void OnDrop(PointerEventData eventData)
    {

        //Debug.Log(eventData);
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
         if (droppedEquipment.equipment.Type == "Weapon")
         {
            WeaponData weaponData = itemDatabase.FetchWeaponByID(droppedEquipment.equipment.ID);
            //Debug.Log(weaponData.Mount_Size);
            if (weaponData.Mount_Size <= weaponSlotSize)
            {
                // swap out the current ship object in this slot and send it back to the inventory
                if (this.transform.childCount > 1)
                {
                    //Debug.Log("adding new weapon to slot " + id + " that has a weapon in it");
                    //get the current weapon and send it back to the inventory in the slot that the dropped one was in
                    Transform equipment = this.transform.GetChild(1);
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
                        //sendingWeaponSlot.childName = currentEquipment.equipment.Title;
                        sendingWeaponSlot.shipData.weaponList[sendingWeaponSlot.slotId] = currentEquipment.equipment.ID;
                        sendingWeaponSlot.shipData.UpdateWeapon(currentEquipment.equipment.ID, sendingWeaponSlot.myWeaponController);
                    }
                }

                if(this.transform.childCount == 1 && droppedEquipment.slotType == "Weapon")
                {
                    WeaponSlot sendingWeaponSlot = weaponSlotList[droppedEquipment.slot];
                    sendingWeaponSlot.shipData.ClearWeapon(sendingWeaponSlot.myWeaponController);
                }
                // set up thje current ship data based on the data from the object
                droppedEquipment.slotType = "Weapon";
                droppedEquipment.slot = slotId;

                shipData.weaponList[slotId] = droppedEquipment.equipment.ID;
                shipData.UpdateWeapon(droppedEquipment.equipment.ID, myWeaponController);
            }
        }
    }
}
