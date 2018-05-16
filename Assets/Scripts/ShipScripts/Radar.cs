using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {

    public int range;
    public int level;
    public int targetingRange;
    public int targetingSpeed;
    public int energyCost;
    public int signature;


    Camera minimapCamera;
	// Use this for initialization
	void Start () {
        minimapCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
	}
	
    public void UpdateMinimap()
    {
        minimapCamera.orthographicSize = range;
    }
}
