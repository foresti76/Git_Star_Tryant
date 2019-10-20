using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ShipData
{
    [SerializeField]
    public int ID { get; set; }
    [SerializeField]
    public string Title { get; set; }
    [SerializeField]
    public string Faction { get; set; }
    [SerializeField]
    public string Prefab { get; set; }
    [SerializeField]
    public int HullID { get; set; }
    [SerializeField]
    public int Shield { get; set; }
    [SerializeField]
    public int Engine { get; set; }
    [SerializeField]
    public int Ecm { get; set; }
    [SerializeField]
    public int Radar { get; set; }
    [SerializeField]
    public int Rcs { get; set; }
    [SerializeField]
    public int Tractorbeam { get; set; }
    [SerializeField]
    public int Generator { get; set; }
    [SerializeField]
    public string LootTable { get; set; }
    [SerializeField]
    public int LootAmmount { get; set; }
    [SerializeField]
    public bool PlayerShip { get; set; }
    [SerializeField]
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
        ID = id;
        Title = title;
        Faction = faction;
        Prefab = prefab;
        HullID = hullID;
        Shield = shield;
        Engine = engine;
        Ecm = ecm;
        Radar = radar;
        Rcs = rcs;
        Tractorbeam = tractorbeam;
        Generator = generator;
        LootTable = lootTable;
        LootAmmount = lootAmmount;
        PlayerShip = playership;
        Weapons = weapons;
        //todo add in reference to 3d model from slug see equipment sprit reference
    }
}
