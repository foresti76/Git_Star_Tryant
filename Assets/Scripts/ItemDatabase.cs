﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    public List<Equipment> equipmentDatabase = new List<Equipment>();
    public List<Engine> engineDatabase = new List<Engine>();
    public List<Ship> shipDatabase = new List<Ship>();
    public List<Shield> shieldDatabase = new List<Shield>();
    public List<RCS> rcsDatabase = new List<RCS>();
    public List<Generator> generatorDatabase = new List<Generator>();
    public List<ECM> ecmDatabase = new List<ECM>();
    public List<TractorBeam> tractorbeamDatabase = new List<TractorBeam>();
    public List<Radar> radarDatabase = new List<Radar>();
    public List<Weapon> weaponDatabase = new List<Weapon>();

    private JsonData equipmentData;
     //todo subsystems, consumable, energy weapon, missile weapon, mine weapon and change weapon to projectile weapon

    void Start()
    {
        equipmentData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Equipment.json"));
        ConstructEquipmentDatabase();
        ConstructEngineDatabase();
        ConstructShipDatabase();
        ConstructShieldDatabase();
        ConstructRCSDatabase();
        ConstructGeneratorDatabase();
        ConstructECMDatabase();
        ConstructTractorBeamDatabase();
        ConstructRadarDatabase();
        ConstructWeaponDatabase();
        //todo  subsystems, comsumable
        // Debug.Log(engineDatabase.Count);
        //Debug.Log(equipmentDatabase[1].ID);
    }

    void ConstructEquipmentDatabase()
    {
        for (int i = 0; i < equipmentData.Count; i++)
        {
            equipmentDatabase.Add(new Equipment((int)equipmentData[i]["id"], equipmentData[i]["type"].ToString(), equipmentData[i]["title"].ToString(), equipmentData[i]["description"].ToString(), (int)equipmentData[i]["cost"], (bool)equipmentData[i]["stackable"], equipmentData[i]["slug"].ToString()));
        }
    }

    void ConstructEngineDatabase()
    {
        for (int i = 0; i < equipmentData.Count; i++)
        {
            if (equipmentData[i]["type"].ToString() == "Engine")
            { 
                engineDatabase.Add(new Engine((int)equipmentData[i]["id"], equipmentData[i]["type"].ToString(), equipmentData[i]["title"].ToString(), equipmentData[i]["description"].ToString(), (int)equipmentData[i]["cost"], (int)equipmentData[i]["combat_speed"], (int)equipmentData[i]["crusing_speed"], (int)equipmentData[i]["acceleration"], (int)equipmentData[i]["maneuver_bonus"], (int)equipmentData[i]["energy_cost"], (int)equipmentData[i]["signature"], equipmentData[i]["slug"].ToString()));
            }
        }
    }

    void ConstructShipDatabase()
    {
        for (int i = 0; i < equipmentData.Count; i++)
        {
            if (equipmentData[i]["type"].ToString() == "Ship")
            {
                shipDatabase.Add(new Ship((int)equipmentData[i]["id"], equipmentData[i]["type"].ToString(), equipmentData[i]["title"].ToString(), equipmentData[i]["description"].ToString(), (int)equipmentData[i]["cost"], equipmentData[i]["size"].ToString(), (int)equipmentData[i]["mass"], (int)equipmentData[i]["hullpoints"], (int)equipmentData[i]["cargospace"], (int)equipmentData[i]["armor"], (int)equipmentData[i]["subsystems"], (int)equipmentData[i]["sm_hardpoints"], (int)equipmentData[i]["med_hardpoints"], (int)equipmentData[i]["lg_hardpoints"], equipmentData[i]["slug"].ToString()));
            }
        }
    }

    void ConstructShieldDatabase()
    {
        for (int i = 0; i < equipmentData.Count; i++)
        {
            if (equipmentData[i]["type"].ToString() == "Shield")
            {
                shieldDatabase.Add(new Shield((int)equipmentData[i]["id"], equipmentData[i]["type"].ToString(), equipmentData[i]["title"].ToString(), equipmentData[i]["description"].ToString(), (int)equipmentData[i]["cost"], (int)equipmentData[i]["max_shield"], (int)equipmentData[i]["regen_rate"], (int)equipmentData[i]["regen_delay"], (int)equipmentData[i]["refresh_delay"], (int)equipmentData[i]["damage_mitigation"], (int)equipmentData[i]["maint_energy_cost"], (int)equipmentData[i]["recharge_energy_cost"], (int)equipmentData[i]["signature"], equipmentData[i]["slug"].ToString()));
            }
        }
    }

    void ConstructRCSDatabase()
    {
        for (int i = 0; i < equipmentData.Count; i++)
        {
            if (equipmentData[i]["type"].ToString() == "RCS")
            {
                rcsDatabase.Add(new RCS((int)equipmentData[i]["id"], equipmentData[i]["type"].ToString(), equipmentData[i]["title"].ToString(), equipmentData[i]["description"].ToString(), (int)equipmentData[i]["cost"], (int)equipmentData[i]["rot"], (int)equipmentData[i]["acc_bonus"], (int)equipmentData[i]["energy_cost"], equipmentData[i]["slug"].ToString()));
            }
        }
    }

    void ConstructGeneratorDatabase()
    {
        for (int i = 0; i < equipmentData.Count; i++)
        {
            if (equipmentData[i]["type"].ToString() == "Generator")
            {
                generatorDatabase.Add(new Generator((int)equipmentData[i]["id"], equipmentData[i]["type"].ToString(), equipmentData[i]["title"].ToString(), equipmentData[i]["description"].ToString(), (int)equipmentData[i]["cost"], (int)equipmentData[i]["energy_generation"], (int)equipmentData[i]["storage_capacity"], (int)equipmentData[i]["signature"], equipmentData[i]["slug"].ToString()));
            }
        }
    }

    void ConstructECMDatabase()
    {
        for (int i = 0; i < equipmentData.Count; i++)
        {
            if (equipmentData[i]["type"].ToString() == "ECM")
            {
                ecmDatabase.Add(new ECM((int)equipmentData[i]["id"], equipmentData[i]["type"].ToString(), equipmentData[i]["title"].ToString(), equipmentData[i]["description"].ToString(), (int)equipmentData[i]["cost"], equipmentData[i]["defense_type"].ToString(), (int)equipmentData[i]["defense_value"], (int)equipmentData[i]["energy_cost"], equipmentData[i]["slug"].ToString()));
            }
        }
    }

    void ConstructTractorBeamDatabase()
    {
        for (int i = 0; i < equipmentData.Count; i++)
        {
            if (equipmentData[i]["type"].ToString() == "TractorBeam")
            {
                tractorbeamDatabase.Add(new TractorBeam((int)equipmentData[i]["id"], equipmentData[i]["type"].ToString(), equipmentData[i]["title"].ToString(), equipmentData[i]["description"].ToString(), (int)equipmentData[i]["cost"], (int)equipmentData[i]["max_pull_mass"], (int)equipmentData[i]["pull_range"], (int)equipmentData[i]["pull_rate"], (int)equipmentData[i]["energy_cost"], equipmentData[i]["slug"].ToString()));
            }
        }
    }

    void ConstructRadarDatabase()
    {
        for (int i = 0; i < equipmentData.Count; i++)
        {
            if (equipmentData[i]["type"].ToString() == "Radar")
            {
                radarDatabase.Add(new Radar((int)equipmentData[i]["id"], equipmentData[i]["type"].ToString(), equipmentData[i]["title"].ToString(), equipmentData[i]["description"].ToString(), (int)equipmentData[i]["cost"], (int)equipmentData[i]["range"], (int)equipmentData[i]["iff_level"], (int)equipmentData[i]["targeting_range"], (int)equipmentData[i]["targeting_speed"], (int)equipmentData[i]["energy_cost"], (int)equipmentData[i]["signature"], equipmentData[i]["slug"].ToString()));
            }
        }
    }

    void ConstructWeaponDatabase()
    {
        for (int i = 0; i < equipmentData.Count; i++)
        {
            if (equipmentData[i]["type"].ToString() == "Weapon")
            {
                weaponDatabase.Add(new Weapon((int)equipmentData[i]["id"], equipmentData[i]["type"].ToString(), equipmentData[i]["title"].ToString(), equipmentData[i]["description"].ToString(), (int)equipmentData[i]["cost"], equipmentData[i]["mount_size"].ToString(), equipmentData[i]["weapon_type"].ToString(), (int)equipmentData[i]["damage"], (int)equipmentData[i]["projectiles_per_shot"], (int)equipmentData[i]["ammo_capacity"], (int)equipmentData[i]["fire_rate"], (int)equipmentData[i]["energy_cost"], (int)equipmentData[i]["signature"], equipmentData[i]["slug"].ToString()));
            }
        }
    }
    //todo  subsystems, consumables, energy weapon, missile weapon, mine weapon and change weapon to projectile weapon

    public Equipment FetchEquipmentByID(int id)
    {
        for (int i = 0; i < equipmentDatabase.Count; i++)
        {
            if (equipmentDatabase[i].ID == id)
            {
                return equipmentDatabase[i];
            }
        }
        return null;
    }

    public Engine FetchEngineByID(int id)
    {
        for (int i = 0; i < engineDatabase.Count; i++)
        {
            if (engineDatabase[i].ID == id)
            {
                return engineDatabase[i];
            }
        }
        return null;
    }

    public Ship FetchShipByID(int id)
    {
        for (int i = 0; i < shipDatabase.Count; i++)
        {
            if (shipDatabase[i].ID == id)
            {
                return shipDatabase[i];
            }
        }
        return null;
    }

    public Shield FetchShieldByID(int id)
    {
        for (int i = 0; i < shieldDatabase.Count; i++)
        {
            if (shieldDatabase[i].ID == id)
            {
                return shieldDatabase[i];
            }
        }
        return null;
    }

    public RCS FetchRCSByID(int id)
    {
        for (int i = 0; i < rcsDatabase.Count; i++)
        {
            if (rcsDatabase[i].ID == id)
            {
                return rcsDatabase[i];
            }
        }
        return null;
    }

    public Generator FetchGeneratorByID(int id)
    {
        for (int i = 0; i < generatorDatabase.Count; i++)
        {
            if (generatorDatabase[i].ID == id)
            {
                return generatorDatabase[i];
            }
        }
        return null;
    }
    
    public ECM FetchECMByID(int id)
    {
        for (int i = 0; i < ecmDatabase.Count; i++)
        {
            if (ecmDatabase[i].ID == id)
            {
                return ecmDatabase[i];
            }
        }
        return null;
    }

    public TractorBeam FetchTractoBeamByID(int id)
    {
        for (int i = 0; i < tractorbeamDatabase.Count; i++)
        {
            if (tractorbeamDatabase[i].ID == id)
            {
                return tractorbeamDatabase[i];
            }
        }
        return null;
    }

    public Radar FetchRadarByID(int id)
    {
        for (int i = 0; i < radarDatabase.Count; i++)
        {
            if (radarDatabase[i].ID == id)
            {
                return radarDatabase[i];
            }
        }
        return null;
    }

    public Weapon FetchWeaponByID(int id)
    {
        for (int i = 0; i < weaponDatabase.Count; i++)
        {
            if (weaponDatabase[i].ID == id)
            {
                return weaponDatabase[i];
            }
        }
        return null;
    }
    //todo subsystems, consumables, energy weapon, missile weapon, mine weapon and change weapon to projectile weapon
}

public class Equipment
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public bool Stackable { get; set;}
        public string Slug { get; set; }
        public Sprite Sprite { get; set; }

        public Equipment(int id, string type, string title, string description, int cost, bool stackable, string slug)
        {
            this.ID = id;
            this.Type = type;
            this.Title = title;
            this.Description = description;
            this.Cost = cost;
            this.Stackable = stackable;
            this.Slug = slug;
            this.Sprite = Resources.Load<Sprite>("Sprites/Equipment/" + Slug);
        }

    public Equipment()
    {
        this.ID = -1;
    }
}

public class Engine
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public int Combat_Speed { get; set; }
        public int Crusing_Speed { get; set; }
        public int Acceleration { get; set; }
        public int Manuver_Bonus { get; set; }
        public int Energy_Cost { get; set; }
        public int Signature { get; set; }
        public string Slug { get; set; }

    public Engine (int id, string type, string title, string description, int cost, int combat_speed, int crusing_speed, int acceleration, int maneuver_bonus, int energy_cost, int signature, string slug)
        {
            this.ID = id;
            this.Type = type;
            this.Title = title;
            this.Cost = cost;
            this.Description = description;
            this.Combat_Speed = combat_speed;
            this.Crusing_Speed = crusing_speed;
            this.Acceleration = acceleration;
            this.Manuver_Bonus = maneuver_bonus;
            this.Energy_Cost = energy_cost;
            this.Signature = signature;
            this.Slug = slug;
        //todo add in reference to 3d model from slug see equipment sprit reference
        }
    }

public class Ship
{
    public int ID { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public string Size { get; set; }
    public int Mass { get; set; }
    public int Hullpoints { get; set; }
    public int Cargospace { get; set; }
    public int Armor { get; set; }
    public int Subsystems { get; set; }
    public int Sm_Hardpoints { get; set; }
    public int Med_Hardpoints { get; set; }
    public int Lg_Hardpoints { get; set; }
    public string Slug { get; set; }

    public Ship(int id, string type, string title, string description, int cost, string size, int mass, int hullpoints, int cargospace, int armor, int subsystems, int sm_hardpoints, int med_hardpoints, int lg_hardpoints, string slug)
    {
        this.ID = id;
        this.Type = type;
        this.Title = title;
        this.Cost = cost;
        this.Description = description;
        this.Size = size;
        this.Mass = mass;
        this.Hullpoints = hullpoints;
        this.Cargospace = cargospace;
        this.Armor = armor;
        this.Subsystems = subsystems;
        this.Sm_Hardpoints = sm_hardpoints;
        this.Med_Hardpoints = med_hardpoints;
        this.Lg_Hardpoints = lg_hardpoints;
        this.Slug = slug;
        //todo add in reference to 3d model from slug see equipment sprit reference
    }
}

public class Shield
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int Max_Shield { get; set; }
        public int Regen_Rate { get; set; }
        public int Regen_Delay { get; set; }
        public int Refresh_Delay { get; set; }
        public int Damage_Mitigation { get; set; }
        public int Maint_Energy_Cost { get; set; }
        public int Recharge_Energy_Cost { get; set; }
        public int Signature { get; set; }
        public string Slug { get; set; }

        public Shield(int id, string type, string title, string description, int cost, int max_shield, int regen_rate, int regen_delay, int refresh_delay, int damage_mitigation, int maint_energy_cost, int recharge_energy_cost, int signature, string slug)
        {
            this.ID = id;
            this.Type = type;
            this.Title = title;
            this.Cost = cost;
            this.Description = description;
            this.Max_Shield = max_shield;
            this.Regen_Rate = regen_rate;
            this.Regen_Delay = regen_rate;
            this.Refresh_Delay = refresh_delay;
            this.Damage_Mitigation = damage_mitigation;
            this.Maint_Energy_Cost = maint_energy_cost;
            this.Recharge_Energy_Cost = recharge_energy_cost;
            this.Signature = signature;
            this.Slug = slug;
            //todo add in reference to 3d model from slug see equipment sprit reference
        }
    }

public class RCS
{
    public int ID { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public int Rot { get; set; }
    public int Acc_Bonus { get; set; }
    public int Energy_Cost { get; set; }
    public string Slug { get; set; }

    public RCS(int id, string type, string title, string description, int cost, int rot, int acc_bonus, int energy_cost, string slug)
    {
        this.ID = id;
        this.Type = type;
        this.Title = title;
        this.Cost = cost;
        this.Description = description;
        this.Rot = rot;
        this.Acc_Bonus = acc_bonus;
        this.Energy_Cost = energy_cost;
        this.Slug = slug;
        //todo add in reference to 3d model from slug see equipment sprit reference
    }
}

public class Generator
{
    public int ID { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public int Energy_Generation { get; set; }
    public int Storage_Capacity { get; set; }
    public int Signature { get; set; }
    public string Slug { get; set; }

    public Generator(int id, string type, string title, string description, int cost, int energy_generation, int storage_capacity, int signature, string slug)
    {
        this.ID = id;
        this.Type = type;
        this.Title = title;
        this.Cost = cost;
        this.Description = description;
        this.Energy_Generation = energy_generation;
        this.Storage_Capacity = storage_capacity;
        this.Signature = signature;
        this.Slug = slug;
        //todo add in reference to 3d model from slug see equipment sprit reference
    }
}

public class ECM
{
    public int ID { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public string Defense_Type { get; set; }
    public int Defense_Value { get; set; }
    public int Energy_Cost { get; set; }
    public string Slug { get; set; }

    public ECM(int id, string type, string title, string description, int cost, string defense_type, int defense_value, int energy_cost, string slug)
    {
        this.ID = id;
        this.Type = type;
        this.Title = title;
        this.Cost = cost;
        this.Description = description;
        this.Defense_Type = defense_type;
        this.Defense_Value = defense_value;
        this.Energy_Cost = energy_cost;
        this.Slug = slug;
        //todo add in reference to 3d model from slug see equipment sprit reference
    }
}

public class TractorBeam
{
    public int ID { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public int Max_Pull_Mass { get; set; }
    public int Pull_Range { get; set; }
    public int Pull_Rate { get; set; }
    public int Energy_Cost { get; set; }
    public string Slug { get; set; }

    public TractorBeam(int id, string type, string title, string description, int cost, int max_pull_mass, int pull_range, int pull_rate, int energy_cost, string slug)
    {
        this.ID = id;
        this.Type = type;
        this.Title = title;
        this.Cost = cost;
        this.Description = description;
        this.Max_Pull_Mass = max_pull_mass;
        this.Pull_Range = pull_range;
        this.Pull_Rate = pull_rate;
        this.Energy_Cost = energy_cost;
        this.Slug = slug;
        //todo add in reference to 3d model from slug see equipment sprite reference
    }
}

public class Radar
{
    public int ID { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public int Range { get; set; }
    public int IFF_Level { get; set; }
    public int Targeting_Range { get; set; }
    public int Targeting_Speed { get; set; }
    public int Energy_Cost { get; set; }
    public int Signature { get; set; }
    public string Slug { get; set; }

    public Radar(int id, string type, string title, string description, int cost, int range, int iff_level, int targeting_range, int targeting_speed, int energy_cost, int signature, string slug)
    {
        this.ID = id;
        this.Type = type;
        this.Title = title;
        this.Cost = cost;
        this.Description = description;
        this.Range = range;
        this.IFF_Level = iff_level;
        this.Targeting_Range = targeting_range;
        this.Targeting_Speed = targeting_speed;
        this.Energy_Cost = energy_cost;
        this.Signature = signature;
        this.Slug = slug;
        //todo add in reference to 3d model from slug see equipment sprite reference
    }
}

public class Weapon
{
    public int ID { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public string Mount_Size { get; set; }
    public string Weapon_Type { get; set; }
    public int Damage { get; set; }
    public int Projectiles_per_Shot { get; set; }
    public int Ammo_Capacity { get; set; }
    public int Fire_Rate { get; set; }
    public int Energy_Cost { get; set; }
    public int Signature { get; set; }
    public string Slug { get; set; }

    public Weapon(int id, string type, string title, string description, int cost, string mount_size, string weapon_type, int damage, int proectiles_per_shot, int ammo_capacity, int fire_rate, int energy_cost, int signature, string slug)
    {
        this.ID = id;
        this.Type = type;
        this.Title = title;
        this.Cost = cost;
        this.Description = description;
        this.Mount_Size = mount_size;
        this.Weapon_Type = weapon_type;
        this.Damage = damage;
        this.Projectiles_per_Shot = proectiles_per_shot;
        this.Ammo_Capacity = ammo_capacity;
        this.Fire_Rate = fire_rate;
        this.Energy_Cost = energy_cost;
        this.Signature = signature;
        this.Slug = slug;
        //todo add in reference to 3d model from slug see equipment sprite reference
    }
}
//todo subsystems, consumables, energy weapon, missile weapon, mine weapon and change weapon to projectile weapon 

