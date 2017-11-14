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
    private WeaponController[] weaponControllerList;
    private WeaponController myWeaponController;

    private WeaponSlot[] weaponSlotList;
    // Use this for initialization
    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        itemDatabase = inv.GetComponent<ItemDatabase>();
        //find the weapon associated with this slot and put in the data
        if (playerShip != null)
        {
            weaponControllerList = playerShip.GetComponentsInChildren<WeaponController>();
            GameObject weaponPanel = GameObject.Find("WeaponsLayout");
            weaponSlotList = weaponPanel.GetComponentsInChildren<WeaponSlot>();
            for (int i = 0; i < weaponControllerList.Length; i++)
            {
                WeaponController currentWeaponController = weaponControllerList[i].GetComponent<WeaponController>();
                if (currentWeaponController.slotID == id)
                {
                    myWeaponController = currentWeaponController;
                    return;
                }

            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData);
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
         if (droppedEquipment.equipment.Type == "Weapon")
         {
            weaponData = itemDatabase.FetchWeaponByID(droppedEquipment.equipment.ID);
            Debug.Log(weaponData.Mount_Size);
            if (weaponData.Mount_Size == weaponSlotSize)
            {
                // swap out the current ship object in this slot and send it back to the inventory
                if (childName != "")
                {
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

                    if(droppedEquipment.slotType == "Weapon")
                    {
                        currentEquipment.slot = droppedEquipment.slot;
                        currentEquipment.transform.SetParent(droppedEquipment.transform.parent);
                        currentEquipment.transform.position = droppedEquipment.transform.parent.position;
                        
                        //todo figure out a way to update the weapon controller to make the weaponslot update its data when the weapons are switched
                        WeaponSlot sendingWeaponSlot = weaponSlotList[droppedEquipment.slot];
                        sendingWeaponSlot.UpdateData(currentEquipment.equipment.ID);

                    }
                }

                if(childName == "" && droppedEquipment.slotType == "Weapon")
                {
                    WeaponSlot sendingWeaponSlot = weaponSlotList[droppedEquipment.slot];
                    sendingWeaponSlot.ClearData();
                }
                // set up thje current ship data based on the data from the object
                Debug.Log("adding " + weaponData.Title);
                droppedEquipment.slotType = "Weapon";
                droppedEquipment.slot = id;
                childName = weaponData.Title;
                UpdateData(droppedEquipment.equipment.ID);
            }
        }
        else
        {
            return;
        }
    }

    public void UpdateData(int id)
    {
        weaponData = itemDatabase.FetchWeaponByID(id);
        myWeaponController.shotDamage = weaponData.Damage;
        myWeaponController.fireRate = weaponData.Fire_Rate;

        //Todo Hook these up once strucutre is in place.
        //myWeaponController.weaponType = weaponData.Weapon_Type;
        //myWeaponController.ammoCapacity = weaponData.Ammo_Capacity;
        //myWeaponController.energyCost = weaponData.Energy_Cost;
        //myWeaponController.signature = weaponData.Singature;
    }

    public void ClearData()
    {
        //todo get rid of the weapon as there is nothing in the slot anymore
        myWeaponController.shotDamage = 0;
        myWeaponController.fireRate = 0;

        //Todo Hook these up once strucutre is in place.
        //myWeaponController.weaponType = weaponData.Weapon_Type;
        //myWeaponController.ammoCapacity = weaponData.Ammo_Capacity;
        //myWeaponController.energyCost = weaponData.Energy_Cost;
        //myWeaponController.signature = weaponData.Singature;
    }
}
