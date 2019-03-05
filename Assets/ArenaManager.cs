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
    public List<WaveData> arenaWaveDatabase = new List<WaveData>();
    public List<WaveData> round0;
    public List<WaveData> round1;
    public List<WaveData> round2;
    public List<WaveData> round3;
    public List<WaveData> round4;
    public List<WaveData> round5;
    public List<WaveData> round6;
    public List<WaveData> round7;
    public List<WaveData> round8;
    public List<WaveData> round9;

    public JsonData arenaWaveData;
    // Start is called before the first frame update
    void Start()
    {
        arenaWaveData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/ShipData.json"));
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

    }
}
