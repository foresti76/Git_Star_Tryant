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
    public GameObject arenaPanel;
    public Text starBaseTitleText;
    public bool inventoryOpen;
    public bool shipCustomizationOpen;

    ArenaManager arenaManager;
    GameObject miniMap;
    SaveData saveData;
    public PlayerControls playerControls;

    void Start () {
        //find the objects with those scripts
        arenaManager = FindObjectOfType<ArenaManager>();
        miniMap = GameObject.Find("Minimap");
        saveData = FindObjectOfType<SaveData>();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();

        HideShipCustomization();
        HideStarbaseScreen();
        HideDockingPrompt();
    }
	
    public void Init()
    {
        Debug.Log("initalizing player controls");
        playerControls = arenaManager.gameManager.player.GetComponent<PlayerControls>();
        if (playerControls == null)
        {
            Debug.Log("Failed to find player object to set controls for.");
        }
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

        if (Input.GetKeyDown(KeyCode.LeftControl & KeyCode.S))
        {
                saveData.Save();
        }

        if (Input.GetKeyDown(KeyCode.RightControl & KeyCode.L))
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
        if(!starBasePanel.activeInHierarchy)
            Pause();
    }

    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
        inventoryOpen = false;
        miniMap.SetActive(true);
        if (!starBasePanel.activeInHierarchy)
            UnPause();
    }

    public void ShowShipCustomization()
    {
        ShowInventory();
        SetInventoryPosition(604.0f, 22.51f);
        shipCustomizationPanel.SetActive(true);
        shipCustomizationOpen = true;
        if (!starBasePanel.activeInHierarchy)
           Pause();
    }

    public void HideShipCustomization()
    {
        inventoryPanel.SetActive(false);
        shipCustomizationPanel.SetActive(false);
        shipCustomizationOpen = false;
        if (!starBasePanel.activeInHierarchy)
            UnPause();
    }

    public void ShowStarbaseScreen()
    {
        HideDockingPrompt();
        starBasePanel.SetActive(true);
        //todo procedually enter the starbase name here
        starBaseTitleText.text = "Welcome to Starbase";
        if(arenaManager.arenaActive == true)
        {
            ShowStarbaseShopScreen();
            //todo hide the buttons that are not allowed in arena mode
        }
        else
        {
            ShowStarbaseNewsScreen();
        }
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
        if (shipCustomizationOpen)
        {
            HideShipCustomization();
        }
        ShowDockingPrompt();
        UnPause();
    }

    public void ShowStarbaseNewsScreen()
    {
        HideStarbaseShopScreen();
        HideStarbaseArenaScreen();
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
        HideStarbaseArenaScreen();
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
        HideStarbaseArenaScreen();
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
        HideStarbaseArenaScreen();
        HideStarbaseCommanderScreen();
        HideStarbaseShipyardScreen();
        starBaseShopPanel.SetActive(true);
        //todo procedually enter the shop name here
        starBaseTitleText.text = "Welcome to this Shop";
        SetInventoryPosition(-180.5f, 39.20f);
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
        HideStarbaseArenaScreen();
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

    public void ShowStarbaseArenaScreen()
    {
        HideStarbaseShopScreen();
        HideStarbaseNewsScreen();
        HideStarbaseCantinaScreen();
        HideStarbaseCommanderScreen();
        HideStarbaseShipyardScreen();
        HideStarbaseShopScreen();
        arenaPanel.SetActive(true);
        arenaPanel.GetComponent<ArenaScreenController>().InitalizeShipSelect();
    }

    public void HideStarbaseArenaScreen()
    {
        arenaPanel.SetActive(false);
    }

    public void ShowDockingPrompt()
    {
        if (arenaManager.arenaActive == false)
        {
            dockingPrompt.SetActive(true);
        }
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
