using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    //Todo Move all keyboard and other controls into here
    // Use this for initialization

    //Make references for all the scrips that have controls in them
    //mainly ship movement/shields/inventory/shipcustomization
    public GameObject inventoryPanel;
    public GameObject shipCustomizationPanel;

    public bool showInventory;
    public bool showShipCustomization;
    GameObject miniMap;
    SaveData saveData;

    void Start () {
        //find the objects with those scripts
        HideShipCustomization();
        miniMap = GameObject.Find("Minimap");
        saveData = GameObject.FindObjectOfType<SaveData>();
    }
	
	// Update is called once per frame
	void Update () {
        //call scripts based on keyboard input
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (showShipCustomization == true)
            {
                HideShipCustomization();
                miniMap.SetActive(true);
            } else
            {
                ShowShipCustomization();
                miniMap.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
                saveData.Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            saveData.Load();
        }

    }

    void ShowInventory()
    {
        inventoryPanel.SetActive(true);
        showInventory = true;

    }

    void HideInventory()
    {
        inventoryPanel.SetActive(false);
        showInventory = false;
    }

    void ShowShipCustomization()
    {
        inventoryPanel.SetActive(true);
        shipCustomizationPanel.SetActive(true);
        showShipCustomization = true;
    }

    void HideShipCustomization()
    {
        inventoryPanel.SetActive(false);
        shipCustomizationPanel.SetActive(false);
        showShipCustomization = false;
    }
}
