﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool targetLock = false;
    public Text targetDisplayText;
    public string targetString;
    public int miniMapSizeScaler = 10;

    float timeToRadarLock;
    private Ship myShip;
    Transform miniMapSelectionBox;
    Transform mapSelectionBox;
    int angle = 0;
    GameObject miniMapSelectionParent;
    GameObject selectionParent;

    Camera minimapCamera;
	// Use this for initialization
	void Start () {
        miniMapSelectionParent = GameObject.Find("MiniMapSelectionCanvas");
        selectionParent = GameObject.Find("SelectionCanvas");
        minimapCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
        radarTrigger = transform.Find("RadarTrigger").GetComponent<SphereCollider>();
        myShip = GetComponent<Ship>();
        if (CheckIfPlayer())
        {
            targetDisplayText = GameObject.Find("PlayerTargetText").GetComponent<Text>();
            miniMapSelectionBox = miniMapSelectionParent.transform.Find("MiniMapSelectionBox").transform;
            mapSelectionBox = selectionParent.transform.Find("SelectionBox").transform;
        }
    }

    //Not sure if this is needed todo check what happens if this is removed.
    private void OnGUI()
    {
        if (target && targetLock)
        {
            targetDisplayText.text = target.name;
        }
    }

    public void UpdateMinimap()
    {
        if (minimapCamera)
        {
            minimapCamera.orthographicSize = range + miniMapSizeScaler;
        }
        else
        {
            minimapCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
            minimapCamera.orthographicSize = range + miniMapSizeScaler;
        }

        radarTrigger.radius = range;
    }

    private void LateUpdate()
    {
        //if (myShip.playerShip)
        //{
        //    UpdateMinimap();  //this is debug only.
        //}

        if (target == null)
        {
            targetLock = false;
            if(CheckIfPlayer())
            {
                targetDisplayText.text = "None";
            }
            return;
        }

        if(target && targetLock == false && CheckIfPlayer())
        {
            angle += 10;
            miniMapSelectionBox.localRotation = Quaternion.Euler(0, 0, angle);
            mapSelectionBox.localRotation = Quaternion.Euler(0, 0, angle);
        }

        if (timeToRadarLock <= Time.time && targetLock == false && CheckIfPlayer())
        {
            targetLock = true;
            angle = 0;
            miniMapSelectionBox.localRotation = Quaternion.Euler(0, 0, angle);
            mapSelectionBox.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
    // commence radar lock
    public void RadarLock()
    {
        targetLock = false;
        if (CheckIfPlayer())
        {
            targetString = target.ToString();
            targetDisplayText.text = targetString;
        }
        if (target.GetComponent<ECM>())
        { 
            timeToRadarLock = Time.time + target.GetComponent<ECM>().lockDefense;
        }
        else
        {
            targetLock = true;
        }
    }

    private bool CheckIfPlayer()
    {
        if(myShip != null && myShip.playerShip == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
