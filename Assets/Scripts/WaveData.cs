using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData 
{
    [SerializeField]
    public int ShipID;
    [SerializeField]
    public int Count;
    
    public WaveData(int shipID, int count)
    {
        ShipID = shipID;
        Count = count;
    }

    public WaveData()
    {

    }
}
