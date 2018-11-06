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
    public GameObject starBaseShopPanel;
    public bool inventoryOpen;
    public bool shipCustomizationOpen;

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
        if (Input.GetKeyDown(KeyCode.I) && starBasePanel.activeInHierarchy == false)
        {
            if (shipCustomizationOpen == true)
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

    public void SetInventoryPosition(float xpos, float ypos)
    {
        Debug.Log("setting the positoin of the inventory panel.");
        inventoryPanel.transform.localPosition = new Vector3(xpos, ypos, 0);
    }

    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);
        inventoryOpen = true;
        miniMap.SetActive(false);
        Pause();
    }

    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
        inventoryOpen = false;
        miniMap.SetActive(true);
        UnPause();
    }

    public void ShowShipCustomization()
    {
        ShowInventory();
        SetInventoryPosition(779.0f, 268.20f);
        shipCustomizationPanel.SetActive(true);
        shipCustomizationOpen = true;
        Pause();
    }

    public void HideShipCustomization()
    {
        inventoryPanel.SetActive(false);
        shipCustomizationPanel.SetActive(false);
        shipCustomizationOpen = false;
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
        HideStarbaseShopScreen();
        miniMap.SetActive(false);
        Pause();
    }

    public void HideStarbaseScreen()
    {
        starBasePanel.SetActive(false);
        miniMap.SetActive(true);
        UnPause();
    }

    public void ShowStarbaseShopScreen()
    {
        starBaseShopPanel.SetActive(true);
        SetInventoryPosition(-180.5f, 150.20f);
        ShowInventory();
    }

    public void HideStarbaseShopScreen()
    {
        starBaseShopPanel.SetActive(false);
        HideInventory();
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
