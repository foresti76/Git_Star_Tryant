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

    void Start()
    {
        filePath = Path.Combine (Application.dataPath, "playerSave.txt");
        playerShip = GameObject.FindGameObjectWithTag("Player");
        ship = playerShip.GetComponent<Ship>();
        Save();
    }

    public void Save()
    {


        if (ship != null)
        {
            string jsonString = JsonUtility.ToJson(ship);
            File.WriteAllText(filePath, jsonString);
        }
    }

    public void Load()
    {
        if (ship != null)
        {
            //todo only read in the data that is needed for the ship. dont overwrite the common stuff.
            string jsonString = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(jsonString, ship);
           // ship.BuildShip();
        }
    }
}
