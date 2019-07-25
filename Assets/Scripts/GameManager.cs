﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public ArenaManager arenaManager;
    public GameObject player;
    public ShipSpawner shipSpawner;
    public Vector3 playerPosition;
    public GameObject playerRespawnMessage;
    public Controls controls;
    public HUD hud;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        arenaManager = FindObjectOfType<ArenaManager>();
        shipSpawner = FindObjectOfType<ShipSpawner>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRespawnMessage.SetActive(false);
        controls = FindObjectOfType<Controls>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null & playerRespawnMessage.activeInHierarchy == false & arenaManager.arenaActive == false)
        {
            ShowRespawnMessage();
        }
        else if(player != null & playerRespawnMessage.activeInHierarchy == true)
        {

            playerRespawnMessage.SetActive(false);
        }

        if(player == null)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                RespawnPlayer();
            }
        }
    }

    public void ShowRespawnMessage()
    {
        playerRespawnMessage.SetActive(true);
    }

    public void RespawnPlayer()
    {
        playerPosition = GameObject.FindObjectOfType<PlayerRecord>().playerLastPosition;
        shipSpawner.SpawnShip(0, playerPosition);
        arenaManager.arenaRoundNumber = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        controls.Init();
    }

    public void ArenaSpawnPlayer(int id)
    {
        Destroy(player);
        player = null;
        player = shipSpawner.SpawnShip(id, arenaManager.areaStartLocation.position).gameObject;
        arenaManager.arenaRoundNumber = 0;
        Debug.Log("finding the player again after they are created");
        //player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.Log("Can't find the player");
        }
        Debug.Log("setting up the player controls");
        controls.playerControls = player.GetComponent<PlayerControls>();
        controls.playerControls.shipMovement = player.GetComponent<ShipMovement>();
        controls.playerControls.combatModeActive = true;
        arenaManager.arenaActive = true;
        playerRespawnMessage.SetActive(false);
        // this isnt working probably need to set these things directly.
    }
}
