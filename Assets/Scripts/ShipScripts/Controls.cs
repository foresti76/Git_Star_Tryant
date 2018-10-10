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
    public GameObject starBasePanel;
    public GameObject dockingPrompt;
    public bool showInventory;
    public bool showShipCustomization;

    GameObject miniMap;
    SaveData saveData;
    public PlayerControls playerControls;

    void Start () {
        //find the objects with those scripts

        miniMap = GameObject.Find("Minimap");
        saveData = FindObjectOfType<SaveData>();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        HideShipCustomization();
        HideStarbaseScreen();
        HideDockingPrompt();
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

        if (Input.GetKey("escape"))
            Application.Quit();

        if (dockingPrompt.activeInHierarchy == true &&  Input.GetKeyDown(KeyCode.F))
        {
            ShowStarbaseScreen();
        }
    }

    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);
        showInventory = true;
        miniMap.SetActive(false);
        Pause();
    }

    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
        showInventory = false;
        miniMap.SetActive(true);
        UnPause();
    }

    public void ShowShipCustomization()
    {
        inventoryPanel.SetActive(true);
        shipCustomizationPanel.SetActive(true);
        showShipCustomization = true;
        Pause();
    }

    public void HideShipCustomization()
    {
        inventoryPanel.SetActive(false);
        shipCustomizationPanel.SetActive(false);
        showShipCustomization = false;
        UnPause();
    }

    private void Pause()
    {
        if (playerControls)
        {
            playerControls.uiOpen = true;
        }
        Time.timeScale = 0;
    }

    private void UnPause()
    {
        if (playerControls)
        {
            playerControls.uiOpen = false;
        }
        Time.timeScale = 1;
    }

    public void ShowStarbaseScreen()
    {
        HideDockingPrompt();
        starBasePanel.SetActive(true);
        miniMap.SetActive(false);
        Pause();
    }

    public void HideStarbaseScreen()
    {
        starBasePanel.SetActive(false);
        miniMap.SetActive(true);
        UnPause();
    }

    public void ShowDockingPrompt()
    {
        dockingPrompt.SetActive(true);
    }

    public void HideDockingPrompt()
    {
        dockingPrompt.SetActive(false);
    }
}
