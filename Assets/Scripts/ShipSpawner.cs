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
    public ArenaManager arenaManager;
    public JsonData shipData;

    // Start is called before the first frame update
    void Start()
    {
        shipData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/ShipData.json"));
        ConstructShipDatabase();
        arenaManager = GameObject.FindObjectOfType<ArenaManager>();
    }

    private void Update()
    {
        //debug code to test this function
        /*
        if (Input.GetKeyDown(KeyCode.G)){
            Vector3 location = new Vector3(-2f, -1.75f , 2f);

            SpawnShip(1, location);
        }
        */
    }

    public GameObject SpawnShip(int shipID, Vector3 location)
    {
        //get the data for the ship you want to spawn from the database
        ShipData newShipData = FetchShipDataByID(shipID);
        
        //create the ship and put in the place you want it to be.
        GameObject shipToSpawn = Instantiate(shipPrefabs.Find(obj => obj.name == newShipData.Prefab));
        shipToSpawn.transform.position = location;

        //setup all the data values for the new ship
        Ship shipScript = shipToSpawn.GetComponent<Ship>();
        shipScript.inv = FindObjectOfType<Inventory>();
        shipScript.itemDatabase = FindObjectOfType<ItemDatabase>();
        shipScript.hullID = newShipData.HullID;
        shipScript.shield = newShipData.Shield;
        shipScript.engine = newShipData.Engine;
        shipScript.ecm = newShipData.Ecm;
        shipScript.radar = newShipData.Radar;
        shipScript.rcs = newShipData.Rcs;
        shipScript.generator = newShipData.Generator;
        shipScript.lootTable = newShipData.LootTable;
        shipScript.lootAmount = newShipData.LootAmmount;
        shipScript.weaponList = newShipData.Weapons;
        if(newShipData.PlayerShip == true)
        {
            shipScript.HUDscript = GameObject.Find("HUD").GetComponent<HUD>();
            shipScript.saveData = GameObject.Find("SaveLoad").GetComponent<SaveData>();
        }
        shipScript.BuildShip();

        if (arenaManager.arenaActive == true && shipScript.playerShip == false)
        {
            arenaManager.currentWave.Add(shipToSpawn);
        }
        return shipToSpawn;
    }

    public ShipData FetchShipDataByID(int id)
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
                                                                                 (bool)shipData[i]["PlayerShip"],
                                                                                 weapons
                                                                                 ));

        }
    }
}
