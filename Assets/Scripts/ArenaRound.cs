using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaRound
{
    public int RoundID;
    public List<WaveData> Waves;
    public int RoundReward;

    public ArenaRound( int roundID, List<WaveData> waves, int roundReward)
    {
        RoundID = roundID;
        Waves = waves;
        RoundReward = roundReward;
    }
}
