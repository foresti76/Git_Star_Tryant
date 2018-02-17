using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour {

    public int hullID;
    public int shield;
    public int engine;
    public int ecm;
    public int radar;
    public int rcs;
    public int tractorbeam;
    public int generator;
    public List<int> weaponList;
    public List<int> subsystemList;

    public bool PlayerShip;
    //todo remove these as they are replaced with direct data
    RCSSlot rcsSlot;
    RadarSlot radarSlot;
    ECMSlot ecmSlot;
    TractorBeamSlot tractorBeamSlot;
    GameObject weaponsLayout;
    GameObject subsystemsLayout;
    Inventory inv;
    ItemDatabase itemDatabase;

    WeaponController[] weaponControllerList;


    private void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        itemDatabase = inv.GetComponent<ItemDatabase>();
        weaponsLayout = GameObject.Find("WeaponsLayout");
        if(PlayerShip == true)
        {
            SaveData saveData = GameObject.Find("SaveLoad").GetComponent<SaveData>();
            saveData.Load();
        }

        weaponControllerList = this.GetComponentsInChildren<WeaponController>();
        BuildShip();
    }

    // todo I need a different way to build the ship info for AI ships that do not have the UI components.
    public void BuildShip()
    {

        UpdateHull(hullID);
        UpdateGenerator(generator);
        UpdateEngine(engine);
        UpdateShield(shield);
        UpdateRCS(rcs);
        UpdateECM(ecm);
        UpdateRadar(radar);
        UpdateTractorBeam(tractorbeam);

        int i = 0;
        foreach (WeaponController weaponController in weaponControllerList)
        {
             UpdateWeapon(weaponList[i], weaponController);
             i++;
        }

        /* todo remove this once I have implemented subsystems
        if (subsystemList.Count > 0)
        {
            int j = 0;
            foreach (SubSystem subSystem in subsystemList)
            {
                UpdateSubsystem(subsystemList[j]);
                j++;
            }
        }
        */
    }

    public void UpdateWeaponList()
    {
        WeaponSlot[] weaponSlots = weaponsLayout.GetComponentsInChildren<WeaponSlot>();

        weaponList.Clear();

        foreach (WeaponSlot weaponSlot in weaponSlots)
        {
            weaponList.Add(weaponSlot.GetComponentInChildren<EquipmentData>().equipment.ID);
        }
    }

    public void UpdateGenerator(int id)
    {

        ShipGenerator generatorScript = this.GetComponent<ShipGenerator>();
        Generator generatorData = itemDatabase.FetchGeneratorByID(id);
        //set up all the things that are controlled by the radarData
        if (generatorScript != null)
        {
            generatorScript.maxPower = generatorData.Storage_Capacity;
            generatorScript.regenRate = generatorData.Energy_Generation;
            generatorScript.currentPower = generatorScript.maxPower;
            //Todo create signature when using generator
        }
    }
    
    public void UpdateHull(int id)
    {
        Hull hull = this.GetComponent<Hull>();
        Ship hullData = itemDatabase.FetchShipByID(id);

        if (hull != null)
        {
            hull.maxHull = hullData.Hullpoints;
            hull.armor = hullData.Armor;
            // todo change the ship model to match the current ship using slug.
        }
    }

    public void UpdateEngine(int id)
    {
        ShipMovement engineScript = this.GetComponent<ShipMovement>();
        Engine engineData = itemDatabase.FetchEngineByID(id);

        if (engineScript != null)
        {

            //Set up the engine stuff
            engineScript.engineThrust = engineData.Acceleration;
            engineScript.reverseThrust = engineData.Acceleration / 2;
            engineScript.maxSpeed = engineData.Combat_Speed;
            engineScript.engineEnergyCost = engineData.Energy_Cost;
            // todo add cruising speed engineScript.cruiseSpeed = engineData.Crusing_Speed;
            // Todo use power from the generator when using the engine
            //Todo create signature when using engine
        }
    }

    public void UpdateShield(int id)
    {
        ShieldBehavior shieldScript = this.GetComponentInChildren<ShieldBehavior>();
        Shield shieldData = itemDatabase.FetchShieldByID(id);

        //set up all the things that are controlled by the shieldData
        if (shieldScript != null)
        {
            shieldScript.maxShield = shieldData.Max_Shield;
            shieldScript.shieldRechageRate = shieldData.Regen_Rate;
            shieldScript.shieldRechargeDuration = shieldData.Regen_Delay;
            shieldScript.shieldRefreshDuration = shieldData.Refresh_Delay;
            shieldScript.rechargeEnergyCost = shieldData.Recharge_Energy_Cost;
            shieldScript.maintEnergyCost = shieldData.Maint_Energy_Cost;
            shieldScript.currentShield = shieldScript.maxShield;
            shieldScript.shieldDisplayDuration = 0.5f;
            //Todo create signature when using shield
        }
    }

    public void UpdateRCS(int id)
    {
        ShipMovement rcsScript = this.GetComponent<ShipMovement>();
        RCS rcsData = itemDatabase.FetchRCSByID(id);

        if (rcsScript != null)
        {
            rcsScript.rotateThrust = rcsData.Rot;
            rcsScript.rcsEnergyCost = rcsData.Energy_Cost;
            //Todo create signature when using rcs
        }
    }

    public void UpdateECM(int id)
    {

    }

    public void UpdateRadar(int id)
    {

    }

    public void UpdateTractorBeam(int id)
    {

    }

    public void UpdateWeapon(int id, WeaponController myWeaponController)
    {

        Weapon weaponData = itemDatabase.FetchWeaponByID(id);

        myWeaponController.shotDamage = weaponData.Damage;
        myWeaponController.fireRate = weaponData.Fire_Rate;
        //todo put this data into the weapon object
        myWeaponController.turretRotationRate = 50;
        myWeaponController.turretRotationLimit = 30;
    //Todo Hook these up once strucutre is in place.
    //myWeaponController.weaponType = weaponData.Weapon_Type;
    //myWeaponController.ammoCapacity = weaponData.Ammo_Capacity;
    //myWeaponController.energyCost = weaponData.Energy_Cost;
    //myWeaponController.signature = weaponData.Singature;
}

    public void UpdateSubsystem(int id)
    {

    }

    public void ClearWeapon(WeaponController myWeaponController)
    {
        myWeaponController.shotDamage = 0;
        myWeaponController.fireRate = 0;
        //Todo Hook these up once strucutre is in place.  Also remove turret from ship.
        //myWeaponController.weaponType = weaponData.Weapon_Type;
        //myWeaponController.ammoCapacity = weaponData.Ammo_Capacity;
        //myWeaponController.energyCost = weaponData.Energy_Cost;
        //myWeaponController.signature = weaponData.Singature;
    }
}
