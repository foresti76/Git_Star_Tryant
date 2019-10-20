using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaRound
{
    [SerializeField]
    public int RoundID;
    [SerializeField]
    public List<WaveData> Waves;
    [SerializeField]
    public int RoundReward;

    public ArenaRound( int roundID, List<WaveData> waves, int roundReward)
    {
        RoundID = roundID;
        Waves = waves;
        RoundReward = roundReward;
    }
}
