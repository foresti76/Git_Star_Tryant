using System.Collections;
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
        controls.playerControls.DeactivateCombatMode();
    }

    public void ArenaSpawnPlayer(int id)
    {
        Destroy(player);
        player = null;
        player = shipSpawner.SpawnShip(id, arenaManager.areaStartLocation.position).gameObject;
        arenaManager.arenaRoundNumber = 0;
        //player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.Log("Can't find the player");
        }
     
        controls.playerControls = player.GetComponent<PlayerControls>();
        controls.playerControls.shipMovement = player.GetComponent<ShipMovement>();
        
        arenaManager.arenaActive = true;
        playerRespawnMessage.SetActive(false);
        StartCoroutine(StartCombatModeAfterTime(1.0f));
        // this isnt working probably need to set these things directly.
    }

    IEnumerator StartCombatModeAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        controls.playerControls.InitateCombatMode();
    }
}
