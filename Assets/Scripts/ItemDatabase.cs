using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    public List<Equipment> equipmentDatabase = new List<Equipment>();
    public List<Engine> engineDatabase = new List<Engine>();
    private JsonData equipementData;
    private JsonData engineData;

    void Start()
    {
        equipementData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Equipment.json"));
        ConstructEquipmentDatabase();

        engineData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Equipment.json"));
        ConstructEngineDatabase();

       // Debug.Log(engineDatabase.Count);
        //Debug.Log(equipmentDatabase[1].ID);
    }

    void ConstructEquipmentDatabase()
    {
        for (int i = 0; i < equipementData.Count; i++)
        {
            equipmentDatabase.Add(new Equipment((int)equipementData[i]["id"], equipementData[i]["type"].ToString(), equipementData[i]["title"].ToString(), equipementData[i]["description"].ToString(), (int)equipementData[i]["cost"], (bool)equipementData[i]["stackable"], equipementData[i]["slug"].ToString()));
        }
    }

    void ConstructEngineDatabase()
    {
        for (int i = 0; i < engineDatabase.Count; i++)
        {
            if (engineData[i]["type"].ToString() == "Engine")
            { 
                engineDatabase.Add(new Engine((int)engineData[i]["id"], engineData[i]["type"].ToString(), engineData[i]["title"].ToString(), (int)engineData[i]["cost"]));
            }
        }
    }

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

        public Engine (int ID, string Type, string Title, int Cost)
        {
            this.ID = ID;
            this.Type = Type;
            this.Title = Title;
            this.Cost = Cost;
        }
    }

    //Todo add in all other types of items and thier contrstuctors and databases

