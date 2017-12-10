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

           // WeaponController currentWeaponController = weaponControllerList[id];
            
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

    //when a weapon is dropped on a slot do all the things to update the weapon perameters
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData);
        EquipmentData droppedEquipment = eventData.pointerDrag.GetComponent<EquipmentData>();
         if (droppedEquipment.equipment.Type == "Weapon")
         {
            weaponData = itemDatabase.FetchWeaponByID(droppedEquipment.equipment.ID);
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
                //Debug.Log("adding " + weaponData.Title);
                droppedEquipment.slotType = "Weapon";
                droppedEquipment.slot = id;
                UpdateData(droppedEquipment.equipment.ID);
            }
        }
        else
        {
            return;
        }
    }

    //update all the data associated with the weapon that was just dropped in the slot
    public void UpdateData(int id)
    {
        weaponData = itemDatabase.FetchWeaponByID(id);
        myWeaponController.shotDamage = weaponData.Damage;
        myWeaponController.fireRate = weaponData.Fire_Rate;
        childName = weaponData.Title;
        //Todo Hook these up once strucutre is in place.
        //myWeaponController.weaponType = weaponData.Weapon_Type;
        //myWeaponController.ammoCapacity = weaponData.Ammo_Capacity;
        //myWeaponController.energyCost = weaponData.Energy_Cost;
        //myWeaponController.signature = weaponData.Singature;
    }

    // clear out the data as there is no longer a weapon assigned to the slot
    public void ClearData()
    {
        //todo get rid of the weapon as there is nothing in the slot anymore
        myWeaponController.shotDamage = 0;
        myWeaponController.fireRate = 0;
        childName = "";

        //Todo Hook these up once strucutre is in place.
        //myWeaponController.weaponType = weaponData.Weapon_Type;
        //myWeaponController.ammoCapacity = weaponData.Ammo_Capacity;
        //myWeaponController.energyCost = weaponData.Energy_Cost;
        //myWeaponController.signature = weaponData.Singature;
    }
}
