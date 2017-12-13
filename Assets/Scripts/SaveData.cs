using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class SaveData : MonoBehaviour
{

    private GameObject playerShip;
    private ShipData shipData;
    private string filePath;

    void Start()
    {
        filePath = Path.Combine (Application.dataPath, "playerSave.txt");
        playerShip = GameObject.FindGameObjectWithTag("Player");
        shipData = playerShip.GetComponent<ShipData>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(shipData);
        File.WriteAllText(filePath, jsonString);
    }

    public void Load()
    {
        string jsonString = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(jsonString, shipData);
        shipData.BuildShip();
    }
}
