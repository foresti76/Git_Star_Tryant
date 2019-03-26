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
    public bool arenaActive = true;
    public PlayerRecord playerRecord;
    // Start is called before the first frame update
    void Start()
    {
        arenaWaveData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/ArenaData.json"));
        ConstructWaveList();
        shipSpawner = GameObject.FindObjectOfType<ShipSpawner>();
        finalRound = arenaWaveDatabase.Count - 1;
        spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        playerRecord = GameObject.FindObjectOfType<PlayerRecord>();
    }

    void LateUpdate()
    {
        foreach(GameObject enemy in currentWave)
        {
            if (enemy == null)
            {
                currentWave.Remove(enemy);
            }
        }
        if(currentWave.Count == 0)
        {
            Debug.Log("Round " + arenaRoundNumber + " complete giving rewards and starting the next round");
            playerRecord.GiveMoney(arenaWaveDatabase[arenaRoundNumber].RoundReward);
            arenaRoundNumber++;
            if (arenaRoundNumber >= finalRound)
            {
                arenaActive = false;
            }
            else
            {
                SpawnWave();
            }
            
        }
    }
    public void SpawnWave()
    {
        foreach (ArenaRound arenaRound in arenaWaveDatabase)
        {
            if (arenaRound.RoundID == arenaRoundNumber)
            {
                foreach(WaveData wave in arenaRound.Waves)
                {
                    for (int i = 0; i < wave.Count; i++)
                    {
                        shipSpawner.SpawnShip(wave.ShipID, spawnpoints[currentSpawnPoint].transform.position);
                        currentSpawnPoint++ ;
                        if(currentSpawnPoint == spawnpoints.Length - 1)
                        {
                            currentSpawnPoint = 0;
                        }
                    }
                }
            }
        }
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
