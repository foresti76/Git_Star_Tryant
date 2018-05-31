﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {

    public int range;
    public int level;
    public int targetingRange;
    public int targetingSpeed;
    public int energyCost;
    public int signature;
    public SphereCollider radarTrigger;
    public GameObject miniMapIcon;
    public List<GameObject> detections;
    public GameObject target;


    Camera minimapCamera;
	// Use this for initialization
	void Start () {
        minimapCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
        radarTrigger = transform.Find("RadarTrigger").GetComponent<SphereCollider>();
	}
	
    public void UpdateMinimap()
    {
        minimapCamera.orthographicSize = range;
        radarTrigger.radius = range;
    }
}
