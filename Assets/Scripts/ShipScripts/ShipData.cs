using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Faction { get; set; }
    public string Prefab { get; set; }
    public int HullID { get; set; }
    public int Shield { get; set; }
    public int Engine { get; set; }
    public int Ecm { get; set; }
    public int Radar { get; set; }
    public int Rcs { get; set; }
    public int Tractorbeam { get; set; }
    public int Generator { get; set; }
    public string LootTable { get; set; }
    public int LootAmmount { get; set; }
    public bool PlayerShip { get; set; }
    public List<int> Weapons { get; set; }


    public ShipData()
    {

    }

    public ShipData(int id, string title, string faction, string prefab, int hullID,
                                                                       int shield,
                                                                       int engine,
                                                                       int ecm,
                                                                       int radar,
                                                                       int rcs,
                                                                       int tractorbeam,
                                                                       int generator,
                                                                       string lootTable,
                                                                       int lootAmmount,
                                                                       bool playership,
                                                                       List<int> weapons)
    {
        this.ID = id;
        this.Title = title;
        this.Faction = faction;
        this.Prefab = prefab;
        this.HullID = hullID;
        this.Shield = shield;
        this.Engine = engine;
        this.Ecm = ecm;
        this.Radar = radar;
        this.Rcs = rcs;
        this.Tractorbeam = tractorbeam;
        this.Generator = generator;
        this.LootTable = lootTable;
        this.LootAmmount = lootAmmount;
        this.PlayerShip = playership;
        this.Weapons = weapons;
        //todo add in reference to 3d model from slug see equipment sprit reference
    }
}
