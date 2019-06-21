using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ArenaManager : MonoBehaviour
{
    public GameManager gameManager;
    public ShipSpawner shipSpawner;
    public int arenaRoundNumber = 0;
    public int finalRound = 10;
    public List<ArenaRound> arenaWaveDatabase = new List<ArenaRound>();
    public GameObject[] spawnpoints;
    public JsonData arenaWaveData;
    int currentSpawnPoint = 0;
    public List<GameObject> currentWave = new List<GameObject>();
    public bool arenaActive = false;
    public PlayerRecord playerRecord;
    public bool waveActive = false;
    public bool betweenWaves = false;
    public GameObject betweenWavesUI;
    public Transform areaStartLocation;
    public GameObject arenaWalls;
    public int arenaShipID;
    // Start is called before the first frame update
    void Start()
    {
        betweenWavesUI.SetActive(false);
        arenaWaveData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/ArenaData.json"));
        ConstructWaveList();
        shipSpawner = GameObject.FindObjectOfType<ShipSpawner>();
        finalRound = arenaWaveDatabase.Count;
        spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        playerRecord = GameObject.FindObjectOfType<PlayerRecord>();
        arenaWalls.SetActive(false);
        //Invoke("SpawnWave", 1.0f);
    }

    void LateUpdate()
    {
        if (arenaActive)
        {
           currentWave.RemoveAll(GameObject => GameObject == null);

            if (currentWave.Count == 0 && waveActive == true)
            {
                waveActive = false;
                playerRecord.GiveMoney(arenaWaveDatabase[arenaRoundNumber].RoundReward);

                //todo clear out the shop and add in the things from that arena round
                arenaRoundNumber++;
                if (arenaRoundNumber < finalRound)
                {
                    betweenWaves = true;
                }
                else
                {
                    arenaActive = false;
                    arenaRoundNumber = 0;
                }
                if (betweenWaves)
                {
                    betweenWavesUI.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }
    }

    public void StartArea()
    {
        gameManager.ArenaSpawnPlayer(arenaShipID);
        arenaWalls.SetActive(true);
        Invoke("SpawnWave", 3.0f);
    }

    public void SpawnWave()
    {
        betweenWaves = false;
        betweenWavesUI.SetActive(false);
        ArenaRound arenaRound = arenaWaveDatabase[arenaRoundNumber];

        foreach (WaveData wave in arenaRound.Waves)
        {
            for (int i = 0; i < wave.Count; i++)
            {
                shipSpawner.SpawnShip(wave.ShipID, spawnpoints[currentSpawnPoint].transform.position);
                currentSpawnPoint++;
                if (currentSpawnPoint == spawnpoints.Length - 1)
                {
                    currentSpawnPoint = 0;
                }
            }
        }
        foreach (GameObject enemy in currentWave)
        {
            if (gameManager.player != null)
            {
                enemy.GetComponent<AIBehavior>().UpdateTarget(gameManager.player);
            }
            else
            {
                Debug.Log(" Gamemanager.player not found");
            }

        }
        waveActive = true;
    }

    void ConstructWaveList()
    {
        for (int i = 0; i < arenaWaveData.Count; i++)
        {
            List<WaveData> waveDataList = new List<WaveData>(JsonMapper.ToObject<List<WaveData>>(arenaWaveData[i]["Waves"].ToJson()));

            int j = 0;
            foreach (WaveData waveData in waveDataList)
            {
                waveDataList[j] = new WaveData(int.Parse(arenaWaveData[i]["Waves"][j]["ShipID"].ToJson().ToString()), int.Parse(arenaWaveData[i]["Waves"][j]["Count"].ToJson().ToString()));
                j++;
            }

            arenaWaveDatabase.Add(new ArenaRound((int)arenaWaveData[i]["RoundID"], waveDataList, (int)arenaWaveData[i]["RoundReward"]));
        }
    }
}
