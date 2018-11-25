using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour {
    //Todo Move all keyboard and other controls into here
    // Use this for initialization

    //Make references for all the scrips that have controls in them
    //mainly ship movement/shields/inventory/shipcustomization
    public GameObject inventoryPanel;
    public GameObject shipCustomizationPanel;
    public GameObject starBasePanel;
    public GameObject starBaseNewsPanel;
    public GameObject starBaseShipyardPanel;
    public GameObject starBaseCantinaPanel;
    public GameObject starBaseCommanderPanel;
    public GameObject dockingPrompt;
    public GameObject starBaseShopPanel;
    public Text starBaseTitleText;
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
        SetInventoryPosition(604.0f, 103.0f);
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

    public void ShowStarbaseScreen()
    {
        HideDockingPrompt();
        starBasePanel.SetActive(true);
        //todo procedually enter the starbase name here
        starBaseTitleText.text = "Welcome to Starbase";
        ShowStarbaseNewsScreen();
        miniMap.SetActive(false);
        Pause();
    }

    public void HideStarbaseScreen()
    {
        starBasePanel.SetActive(false);
        miniMap.SetActive(true);
        if (inventoryOpen)
        {
            HideInventory();
        }
        ShowDockingPrompt();
        UnPause();
    }

    public void ShowStarbaseNewsScreen()
    {
        HideStarbaseShopScreen();
        HideStarbaseCantinaScreen();
        HideStarbaseCommanderScreen();
        HideStarbaseShipyardScreen();
        starBaseNewsPanel.SetActive(true);
        HideStarbaseShopScreen();
        //todo procedually enter the staerbase name here
        starBaseTitleText.text = "Welcome to Starbase Name";
    }

    public void HideStarbaseNewsScreen()
    {
        starBaseNewsPanel.SetActive(false);
    }

    public void ShowStarbaseCantinaScreen()
    {
        HideStarbaseNewsScreen();
        HideStarbaseShopScreen();
        HideStarbaseCommanderScreen();
        HideStarbaseShipyardScreen();
        starBaseCantinaPanel.SetActive(true);
        HideStarbaseShopScreen();
        //todo procedually enter the staerbase name here
        starBaseTitleText.text = "Welcome to Cantina Name";
    }

    public void HideStarbaseCantinaScreen()
    {
        starBaseCantinaPanel.SetActive(false);
    }

    public void ShowStarbaseCommanderScreen()
    {
        HideStarbaseNewsScreen();
        HideStarbaseCantinaScreen();
        HideStarbaseShopScreen();
        HideStarbaseShipyardScreen();
        starBaseCommanderPanel.SetActive(true);
        HideStarbaseShopScreen();
        //todo procedually enter the name here
        starBaseTitleText.text = "Commander Soandso";
    }

    public void HideStarbaseCommanderScreen()
    {
        starBaseCommanderPanel.SetActive(false);
    }

    public void ShowStarbaseShopScreen()
    {
        HideStarbaseNewsScreen();
        HideStarbaseCantinaScreen();
        HideStarbaseCommanderScreen();
        HideStarbaseShipyardScreen();
        starBaseShopPanel.SetActive(true);
        //todo procedually enter the shop name here
        starBaseTitleText.text = "Welcome to this Shop";
        SetInventoryPosition(-180.5f, 150.20f);
        ShowInventory();
    }

    public void HideStarbaseShopScreen()
    {
        starBaseShopPanel.SetActive(false);
        HideInventory();
    }

    public void ShowStarbaseShipyardScreen()
    {
        HideStarbaseNewsScreen();
        HideStarbaseShopScreen();
        HideStarbaseCantinaScreen();
        HideStarbaseCommanderScreen();
        starBaseShipyardPanel.SetActive(true);
        //todo procedually enter the shhipyard name here
        starBaseTitleText.text = "Welcome the Shipyard";
        ShowShipCustomization();
    }

    public void HideStarbaseShipyardScreen()
    {
        starBaseShipyardPanel.SetActive(false);
        HideShipCustomization();
    }

    public void ShowDockingPrompt()
    {
        dockingPrompt.SetActive(true);
    }

    public void HideDockingPrompt()
    {
        dockingPrompt.SetActive(false);
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
}
