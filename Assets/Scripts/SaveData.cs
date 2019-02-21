using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class SaveData : MonoBehaviour
{

    private GameObject playerShip;
    private Ship ship;
    private string filePath;
    public ShipSpawner shipSpawner;
    public PlayerRecord playerRecord;
    public ShipData playerShipData;

    void Start()
    {

        filePath = Path.Combine (Application.dataPath, "playerSave.txt");
        playerShip = GameObject.FindGameObjectWithTag("Player");
        ship = playerShip.GetComponent<Ship>();
        shipSpawner = GameObject.FindObjectOfType<ShipSpawner>();
        playerRecord = GameObject.FindObjectOfType<PlayerRecord>();
        if (ship != null)
        {
            playerShipData = new ShipData();//todo save the ship data to the 0 slot on the shipSpawner database and move all the non-ship related data like currency to the save data file/
            playerShipData.ID = 0;
            playerShipData.Prefab = ship.gameObject.name;
            playerShipData.HullID = ship.hullID;
            playerShipData.Shield = ship.shield;
            playerShipData.Engine = ship.engine;
            playerShipData.Ecm = ship.ecm;
            playerShipData.Radar = ship.radar;
            playerShipData.Rcs = ship.rcs;
            playerShipData.Generator = ship.generator;
            playerShipData.LootTable = ship.lootTable;
            playerShipData.LootAmmount = ship.lootAmount;
            playerShipData.Weapons = ship.weaponList;
            playerShipData.PlayerShip = ship.playerShip;
        }
        else
        {
            Debug.Log("Save data Coulden't find the player ship");
        }
        //todo remove this once I have a real save point
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(playerRecord);
        File.WriteAllText(filePath, jsonString);

        if (ship != null)
        {
            playerShipData = new ShipData();//todo save the ship data to the 0 slot on the shipSpawner database and move all the non-ship related data like currency to the save data file/
            playerShipData.ID = 0;
            playerShipData.Prefab = ship.gameObject.name;
            playerShipData.HullID = ship.hullID;
            playerShipData.Shield = ship.shield ;
            playerShipData.Engine = ship.engine;
            playerShipData.Ecm = ship.ecm;
            playerShipData.Radar = ship.radar;
            playerShipData.Rcs = ship.rcs;
            playerShipData.Generator = ship.generator;
            playerShipData.LootTable = ship.lootTable;
            playerShipData.LootAmmount = ship.lootAmount;
            playerShipData.Weapons = ship.weaponList;
            playerShipData.PlayerShip = ship.playerShip;
        }

        shipSpawner.shipDatabase.RemoveAt(0);
        shipSpawner.shipDatabase.Insert(0, playerShipData);
    }

    public void Load()
    {
        if (ship != null)
        {
            //todo only read in the data that is needed for the ship. dont overwrite the common stuff.
            string jsonString = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(jsonString, playerRecord);
            shipSpawner.SpawnShip(0, playerRecord.playerLastPosition);
        }
    }
}
