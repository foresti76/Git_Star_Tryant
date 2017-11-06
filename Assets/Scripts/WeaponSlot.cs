using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlot : MonoBehaviour, IDropHandler
{
    public string childName;
    public int id;
    public string weaponSlotSize;

    private Inventory inv;
    private GameObject playerShip;
    private Weapon weaponData;
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
         if (droppedEquipment.equipment.Type == "Weapon")
        {
            weaponData = itemDatabase.FetchWeaponByID(droppedEquipment.equipment.ID);

            if (weaponData.Mount_Size == weaponSlotSize)
            {
                // swap out the current ship object in this slot and send it back to the inventory
                if (childName != "")
                {
                    Transform equipment = this.transform.Find(childName);
                    EquipmentData currentEquipment = equipment.GetComponent<EquipmentData>();
                    currentEquipment.slot = droppedEquipment.slot;
                    currentEquipment.slotType = "Inv";
                    equipment.transform.SetParent(inv.slots[currentEquipment.slot].transform);
                    equipment.transform.position = inv.slots[currentEquipment.slot].transform.position;
                }

                // set up thje current ship data based on the data from the object
                droppedEquipment.slotType = "Weapon";
                droppedEquipment.slot = id;
                childName = weaponData.Title;

                //set up all the things that are controlled by the weaponData
                if (playerShip != null)
                {
                    //todo find the weapon associated with this slot and put in the data
                }
            }
        }
    }
}