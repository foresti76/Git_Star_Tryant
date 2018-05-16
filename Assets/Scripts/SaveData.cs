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
        string jsonString = JsonUtility.ToJson(ship);
        File.WriteAllText(filePath, jsonString);
    }

    public void Load()
    {
        string jsonString = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(jsonString, ship);
        ship.BuildShip();
    }
}
