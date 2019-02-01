using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using System;

public class ShipSpawner : MonoBehaviour
{

    public List<ShipData> shipDatabase = new List<ShipData>();
    public List<GameObject> shipPrefabs;

    public JsonData shipData;
    // Start is called before the first frame update
    void Start()
    {
        shipData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/ShipData.json"));
        ConstructShipDatabase();
    }

    void ConstructShipDatabase()
    {
        for (int i = 0; i < shipData.Count; i++)
        {
            List<int> weapons = new List<int>(JsonMapper.ToObject<List<int>>(shipData[i]["Weapons"].ToJson()));

            int j = 0;
            foreach (int weapon in weapons)
            {
                weapons[j] = int.Parse(shipData[i]["Weapons"][j]["WeaponID"].ToJson().ToString());
                j++;
            }

            shipDatabase.Add(new ShipData((int)shipData[i]["id"], shipData[i]["title"].ToString(),
                                                                                 shipData[i]["faction"].ToString(),
                                                                                 shipData[i]["prefab"].ToString(),
                                                                                 (int)shipData[i]["HullID"],
                                                                                 (int)shipData[i]["Shield"],
                                                                                 (int)shipData[i]["Engine"],
                                                                                 (int)shipData[i]["Ecm"],
                                                                                 (int)shipData[i]["Radar"],
                                                                                 (int)shipData[i]["Rcs"],
                                                                                 (int)shipData[i]["Tractorbeam"],
                                                                                 (int)shipData[i]["Generator"],
                                                                                 shipData[i]["LootTable"].ToString(),
                                                                                 (int)shipData[i]["LootAmmount"],
                                                                                 weapons
                                                                                 ));

        }
    }

    [Serializable]
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
        [SerializeField]
        public List<int> Weapons { get; set; }

        [SerializeField]
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
            this.Weapons = weapons;
            //todo add in reference to 3d model from slug see equipment sprit reference
        }
    }
}
