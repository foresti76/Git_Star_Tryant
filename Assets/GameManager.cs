using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool isArenaMode;
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
        if(player == null & playerRespawnMessage.activeInHierarchy == false)
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
        arenaManager.arenaLevel = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        controls.Init();
        //hud.Init();
    }
}
