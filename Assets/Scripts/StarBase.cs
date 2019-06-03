using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBase : MonoBehaviour {

    public string starBaseName;
    public ShipSpawner shipSpawner;
    public List<ShipData> arenaShips = new List<ShipData>();
    //todo add in station inventory.
    //todo add in station systems

    // Use this for initialization
    void Start () {
        starBaseName = "Deep Space Ten";
        arenaShips.Add(shipSpawner.FetchShipDataByID(3));
        arenaShips.Add(shipSpawner.FetchShipDataByID(4));
        arenaShips.Add(shipSpawner.FetchShipDataByID(5));
    }
	
	// Update is called once per frame
}
