using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ArenaManager : MonoBehaviour
{
    public GameManager gameManager;
    public int arenaRound = 0;
    public int finalRound = 10;
    public List<ArenaRound> arenaWaveDatabase = new List<ArenaRound>();

    public JsonData arenaWaveData;
    // Start is called before the first frame update
    void Start()
    {
        arenaWaveData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/ArenaData.json"));
        ConstructWaveList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWave()
    {

    }

    void ConstructWaveList()
    {
        for (int i = 0; i < arenaWaveData.Count; i++)
        {
            List<WaveData> waveDataList = new List<WaveData>(JsonMapper.ToObject<List<WaveData>>(arenaWaveData[i]["Waves"].ToJson()));

            int j = 0;
            foreach (WaveData waveData in waveDataList)
            {
                waveDataList[j] = new WaveData(int.Parse(arenaWaveData[i]["Waves"][j]["ShipID"].ToString()), int.Parse(arenaWaveData[i]["Waves"][j]["Count"].ToString()));
                j++;
            }

            arenaWaveDatabase.Add(new ArenaRound((int)arenaWaveData[i]["RoundID"], waveDataList, (int)arenaWaveData[i]["RoundReward"]));
        }


    }
}
