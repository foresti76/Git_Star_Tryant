using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public int hullID;
    public int shield;
    public int engine;
    public int ecm;
    public int radar;
    public int rcs;
    public int tractorbeam;
    public int generator;
    public string lootTable;
    public int lootAmount;
    public List<int> weaponList;
    public List<int> subsystemList;

    public bool playerShip;
    //todo remove these as they are replaced with direct data

    public GameObject weaponsLayout;
    //public GameObject subsystemsLayout;
    public Inventory inv;
    public ItemDatabase itemDatabase;
    public HUD HUDscript;

    public List<WeaponController> weaponControllerList;
    public WeaponSlot[] weaponSlotArray;

    private void Start()
    {

        if (playerShip == true)
        {
            SaveData saveData = GameObject.Find("SaveLoad").GetComponent<SaveData>();
            saveData.Load();
            weaponsLayout = GameObject.Find("WeaponsLayout");
            HUDscript = GameObject.Find("HUD").GetComponent<HUD>();
        }
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        itemDatabase = inv.GetComponent<ItemDatabase>();

        weaponControllerList = new List<WeaponController>(GetComponentsInChildren<WeaponController>());

        if (itemDatabase != null)
        {
            BuildShip();
        } else
        {
            Debug.Log(this.name + " Can't find itemDatabase ");
        }
        
    }

    // todo I need a different way to build the ship info for AI ships that do not have the UI components.
    public void BuildShip()
    {

        
        UpdateGenerator(generator);
        UpdateEngine(engine);
        UpdateShield(shield);
        UpdateRCS(rcs);
        UpdateECM(ecm);
        UpdateRadar(radar);
        UpdateTractorBeam(tractorbeam);
        UpdateHull(hullID);
        UpdateWeaponContollers();
        if (playerShip)
        { 
            UpdateWeaponSlotList();
            HUDscript.CreateWeaponAmmoDisplayElements();
        }

        for (int i = 0; i < weaponControllerList.Count - 1; i++)
        {
            UpdateWeapon(weaponList[i], weaponControllerList[i]);
        }

        if (playerShip)
        {
            HUDscript.CreateWeaponAmmoDisplayElements();
        }

        /* todo remove this once I have implemented subsystems
        if (subsystemList.Count > 0)
        {

            for (int j = 0; j < subsystemList.Lenght - 1; j++)
            {
                UpdateSubsystem(subsystemList[j]);
                j++;
            }
        }
        */
    }

    public void UpdateWeaponSlotList()
    {
        weaponSlotArray = weaponsLayout.GetComponentsInChildren<WeaponSlot>();

        foreach (WeaponSlot weaponSlot in weaponSlotArray)
        {
            weaponList.Add(weaponSlot.GetComponentInChildren<EquipmentData>().equipment.ID);
        }
    }

    public void UpdateGenerator(int id)
    {

        Generator generatorScript = this.GetComponent<Generator>();
        GeneratorData generatorData = itemDatabase.FetchGeneratorByID(id);
        //set up all the things that are controlled by the radarData
        if (generatorData != null)
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
        HullData hullData = itemDatabase.FetchHullByID(id);
        if (hullData != null)
        {
            hull.maxHull = hullData.Hullpoints;
            hull.armor = hullData.Armor;
            foreach (WeaponController weaponControler in weaponControllerList)
            {
               weaponControler.turretRotationRate = hullData.Turrets[weaponControler.slotID].rotationRate;
               weaponControler.turretRotationLimit = hullData.Turrets[weaponControler.slotID].rotationLimit;
            }

            // todo change the ship model to match the current ship using slug.
        }
    }

    public void UpdateEngine(int id)
    {
        ShipMovement engineScript = this.GetComponent<ShipMovement>();
        EngineData engineData = itemDatabase.FetchEngineByID(id);

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
        Shield shieldScript = this.GetComponentInChildren<Shield>();
        ShieldData shieldData = itemDatabase.FetchShieldByID(id);

        //set up all the things that are controlled by the shieldData
        if (shieldScript != null)
        {
            shieldScript.maxShield = shieldData.Max_Shield;
            shieldScript.shieldRechageRate = shieldData.Regen_Rate;
            shieldScript.shieldRechargeDuration = shieldData.Regen_Delay;
            shieldScript.shieldRestoreDuration = shieldData.Restore_Delay;
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
        RCSData rcsData = itemDatabase.FetchRCSByID(id);

        if (rcsScript != null)
        {
            rcsScript.rotateThrust = rcsData.Rot;
            rcsScript.rcsEnergyCost = rcsData.Energy_Cost;
            //Todo create signature when using rcs
        }
    }

    public void UpdateECM(int id)
    {
        ECM ecmScript = GetComponent<ECM>();
        ECMData ecmData = itemDatabase.FetchECMByID(id);

        if (ecmScript != null)
        {
            ecmScript.lockDefense = ecmData.Lock_Defense;
            ecmScript.detectionDefense = ecmData.Detection_Defense;
            ecmScript.energyCost = ecmData.Energy_Cost;
        }
    }

    public void UpdateRadar(int id)
    {
        Radar radarScript = GetComponent<Radar>();
        RadarData radarData = itemDatabase.FetchRadarByID(id);

        if(radarScript != null && radarData != null)
        {
            radarScript.range = radarData.Range;
            radarScript.level = radarData.IFF_Level;
            radarScript.targetingRange = radarData.Targeting_Range;
            radarScript.targetingSpeed = radarData.Targeting_Speed;
            radarScript.energyCost = radarData.Energy_Cost;
            radarScript.signature = radarData.Signature;
            if (playerShip)
            { 
                radarScript.UpdateMinimap();
            }
        }
    }

    public void UpdateTractorBeam(int id)
    {
        TractorBeam tractorBeamScript = this.GetComponent<TractorBeam>();
        TractorBeamData tractorBeamData = itemDatabase.FetchTractorBeamByID(id);

        if(tractorBeamScript != null && tractorBeamData != null)
        {
            tractorBeamScript.maxPullMass = tractorBeamData.Max_Pull_Mass;
            tractorBeamScript.pullRange = tractorBeamData.Pull_Range;
            tractorBeamScript.pullRate = tractorBeamData.Pull_Rate;
            tractorBeamScript.energyCost = tractorBeamData.Energy_Cost;
        }
    }

    public void UpdateWeaponContollers()
    {
        weaponControllerList = new List<WeaponController>(GetComponentsInChildren<WeaponController>());
    }

    public void UpdateWeapon(int id, WeaponController myWeaponController)
    {

        WeaponData weaponData = itemDatabase.FetchWeaponByID(id);
        myWeaponController.weaponName = weaponData.Title;
        if(weaponData.Weapon_Type == "projectile")
        {
            ProjectileData projectileData = itemDatabase.FetchProjectileByID(id);
            myWeaponController.shotDamage = projectileData.Damage;
            myWeaponController.fireRate = projectileData.Fire_Rate;
            myWeaponController.weaponType = projectileData.Weapon_Type;
            myWeaponController.energyCost = projectileData.Energy_Cost;
            myWeaponController.projectilesPerShot = projectileData.Projectiles_per_Shot;
            myWeaponController.maxAmmo = projectileData.Ammo_Capacity;
        }
        else if (weaponData.Weapon_Type == "laser")
        {
            LaserData laserData = itemDatabase.FetchLaserByID(id);

            myWeaponController.shotDamage = laserData.Damage;
            myWeaponController.fireRate = laserData.Fire_Rate;
            myWeaponController.weaponType = laserData.Weapon_Type;
            myWeaponController.energyCost = laserData.Energy_Cost;
            myWeaponController.laserLength = laserData.Laser_Length;
            myWeaponController.maxAmmo = 1;
        }
        else if (weaponData.Weapon_Type == "missile")
        {
            MissileData missileData = itemDatabase.FetchMissileByID(id);

            myWeaponController.shotDamage = missileData.Damage;
            myWeaponController.fireRate = missileData.Fire_Rate;
            myWeaponController.weaponType = missileData.Weapon_Type;
            myWeaponController.energyCost = missileData.Energy_Cost;
            myWeaponController.speed = missileData.Speed;
            myWeaponController.seek_rate = missileData.Seek_Rate;
            myWeaponController.maxAmmo = missileData.Ammo_Capacity;
        }
        /*else if (weaponData.Weapon_Type == "Drone")
        {
            MineData mineData = itemDatabase.FetchMineByID(id);

            myWeaponController.shotDamage = mineData.Damage;
            myWeaponController.fireRate = mineData.Fire_Rate;
            myWeaponController.weaponType = mineData.Weapon_Type;
            myWeaponController.energyCost = mineData.Energy_Cost;
        }
        */

        myWeaponController.SetFiringType();
        //Todo Hook these up once strucutre is in place.


        //myWeaponController.ammoCapacity = weaponData.Ammo_Capacity;
        //myWeaponController.signature = weaponData.Singature;
        myWeaponController.Reload();
    }

    public void UpdateSubsystem(int id)
    {

    }

    public void ClearWeapon(WeaponController myWeaponController)
    {
        myWeaponController.shotDamage = 0;
        myWeaponController.fireRate = 0;
        myWeaponController.currentAmmo = 0;
        myWeaponController.maxAmmo = 0;
        //Todo Hook these up once strucutre is in place.  Also remove turret from ship.
        //myWeaponController.weaponType = weaponData.Weapon_Type;
        //myWeaponController.ammoCapacity = weaponData.Ammo_Capacity;
        //myWeaponController.energyCost = weaponData.Energy_Cost;
        //myWeaponController.signature = weaponData.Singature;
    }
}
