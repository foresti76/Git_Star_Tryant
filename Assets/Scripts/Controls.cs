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

    void Start () {
        //find the objects with those scripts
        HideShipCustomization();
	}
	
	// Update is called once per frame
	void Update () {
        //call scripts based on keyboard input
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I key press registered");
            if (showShipCustomization == true)
            {
                Debug.Log("hiding stuff");
                HideShipCustomization();
            } else
            {
                Debug.Log("showing stuff");
                ShowShipCustomization();
            }
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
